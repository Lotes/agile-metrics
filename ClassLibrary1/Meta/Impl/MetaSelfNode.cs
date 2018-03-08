using System;
using System.CodeDom;
using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Instance;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Common.DataStructures;
using Metrics.Meta.Impl;

namespace ClassLibrary1.N00_Config.Meta.Impl
{
    public class MetaSelfNode : IMetaSelfNode
    {
        private static int ComputationCounter = 0;
        private readonly IMetaGraph metaGraph;
        private readonly Func<IReadOnlyDictionary<string, object>, object> compute;

        public MetaSelfNode(IMetaGraph metaGraph, TypedKey key, IEnumerable<ArtifactType> targetArtifactTypes, IEnumerable<InputConfiguration> inputs, string sourceCode)
        {
            this.metaGraph = metaGraph;
            Key = key;
            TargetArtifactTypes = targetArtifactTypes;
            compute = CreateComputeDelegate(inputs, key.Type, sourceCode);
        }

        private Func<IReadOnlyDictionary<string, object>, object> CreateComputeDelegate(IEnumerable<InputConfiguration> inputs, Type returnType, string sourceCode)
        {
            var unitId = ComputationCounter++;
            var outputFile = new FileInfo("ComputationUnit" + unitId + ".dll").FullName;
            var compilerParams = new CompilerParameters()
            {
                GenerateInMemory = false,
                TreatWarningsAsErrors = false,
                GenerateExecutable = false,
                CompilerOptions = "/optimize",
                OutputAssembly = outputFile
            };
            var namespaceTypes = new[]
            {
                typeof(AverageData),
                typeof(char), //System
                typeof(IList), //System.Collections
                typeof(IList<>), //System.Collections.Generic
                typeof(Enumerable), //System.Linq
                typeof(XmlDocument), //System.Xml
                typeof(XDocument) //System.Xml.Linq
            };
            compilerParams.ReferencedAssemblies.AddRange(namespaceTypes.Select(t => t.Assembly.Location).ToArray());

            var usings = namespaceTypes.Select(t => t.Namespace.ToString()).ToArray();
            var variables = inputs.ToDictionary(
                i => i.ParameterName, 
                i => i.Locality == DependencyLocality.Self ? i.Key.Type : i.Key.Type.MakeArrayType()
            );
            var @namespace = "Computations";
            var className = "Unit" + unitId;
            var computeMethodName = "Compute";

            var template = new ComputationUnit();
            template.Session = new Dictionary<string, object>()
            {
                { "Usings", usings },
                { "Namespace", @namespace },
                { "ClassName", className },
                { "ComputeMethodName", computeMethodName },
                { "ReturnType", returnType },
                { "Code", sourceCode },
                { "Parameters", variables }
            };
            template.Initialize();
            var wholeText = template.TransformText();
            var unit = new CSharpCodeProvider().CompileAssemblyFromSource(compilerParams, wholeText);
            if (unit.Errors.HasErrors)
                throw new InvalidOperationException(unit.Errors[0].ErrorText);
            var assembly = Assembly.LoadFile(outputFile);
            var type = assembly.GetType(@namespace + "." + className);
            var method = type.GetMethod(computeMethodName, BindingFlags.Static | BindingFlags.Public);
            Func<IReadOnlyDictionary<string, object>, object> wrappingMethod = dictionary =>
            {
                var actuals = new List<object>();
                foreach (var input in variables)
                {
                    var inputType = input.Value;
                    if (dictionary.ContainsKey(input.Key) && dictionary[input.Key] != null)
                        actuals.Add(dictionary[input.Key]);
                    else
                        actuals.Add(inputType.IsValueType 
                            ? CreateValue(inputType)
                            : (inputType.IsArray 
                                ? Activator.CreateInstance(inputType, 0) 
                                : null));
                }
                return method.Invoke(null, actuals.ToArray());
            };
            return wrappingMethod;
        }

        private object CreateValue(Type type)
        {
            if (type == typeof(double))
                return double.NaN;
            if (type == typeof(float))
                return float.NaN;
            return Activator.CreateInstance(type);
        }

        public TypedKey Key { get; private set; }
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; private set; }
        public IReadOnlyDictionary<string, IMetaDependency> Inputs { get { return metaGraph.GetInputsOf(this).ToDictionary(a=>a.Name, a=>a); } }

        public IReadOnlyList<IMetaDependency> Outputs
        {
            get
            {
                return metaGraph.GetOutputsOf(this).ToList();
            }
        }

        public object Compute(IGraph graph, IArtifact artifact)
        {
            var parameters = this.Inputs.ToDictionary(i => i.Key, i => Aggregate(graph, i.Value.Locality, i.Value.Source, artifact));
            return compute(parameters);
        }

        private object Aggregate(IGraph graph, DependencyLocality locality, IMetaNode node, IArtifact artifact)
        {
            var storage = node is IMetaRawNode ? metaGraph.Storage : graph.Storage;
            if (locality == DependencyLocality.Self)
                return storage.GetValue(node, artifact)?.Value;
            var values = artifact.Children.Select(a => storage.GetValue(node, a)?.Value).ToArray();
            var array = Array.CreateInstance(node.Key.Type, values.Length);
            var index = 0;
            foreach(var element in values)
                array.SetValue(element, index++);
            return array;
        }

        public object ComputeDelta(IGraph graph, DeltaMethod method, IArtifact artifact, ITypedKeyDictionary oldValues)
        {
            return null;
        }

        public override string ToString()
        {
            return "{Self} "+Key.ToString();
        }
    }
}