using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.E00_Addons;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Facade.Impl;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Instance.Impl;

namespace ClassLibrary1.N00_Config.Meta.Impl
{
    public class MetaGraph : IMetaGraph, IGraphFactory
    {
        private readonly IMetaGraphFactory metaFactory;

        private readonly Dictionary<TypedKey, IMetaNode> nodes = new Dictionary<TypedKey, IMetaNode>();
        private readonly List<IMetaDependency> dependencies = new List<IMetaDependency>();
        private readonly Dictionary<IMetaNode, List<IMetaDependency>> dependenciesBySource = new Dictionary<IMetaNode, List<IMetaDependency>>();
        private readonly Dictionary<IMetaNode, List<IMetaDependency>> dependenciesByTarget = new Dictionary<IMetaNode, List<IMetaDependency>>();
        private readonly Dictionary<ITagExpression, IGraph> instances = new Dictionary<ITagExpression, IGraph>();
        private readonly IValueStorageFactory storageFactory;
        public MetaGraph(IValueStorageFactory storageFactory, IMetaGraphFactory metaFactory)
        {
            this.storageFactory = storageFactory;
            this.Storage = storageFactory.CreateStorage();
            this.metaFactory = metaFactory;
        }

        public IEnumerable<IMetaDependency> MetaDependencies { get { return dependencies; } }
        public IReadOnlyDictionary<ITagExpression, IGraph> Instances { get { return instances; } }
        public IEnumerable<IMetaNode> MetaNodes { get { return nodes.Values; } }

        public IGraph CreateInstanceFor(ITagExpression tagExpression)
        {
            return instances.GetOrLazyInsert<ITagExpression, IGraph>(tagExpression, () => CreateGraph(this, tagExpression, storageFactory));
        }

        public IValueStorage Storage { get; }

        public IMetaRawNode CreateRawNode(TypedKey key, ImporterType source, ArtifactType[] targetArtifactTypes)
        {
            if (GetNode(key) != null)
                throw new InvalidOperationException();
            var node = metaFactory.CreateMetaRawNode(key, source, targetArtifactTypes);
            nodes.Add(key, node);
            Storage.Allocate(node, targetArtifactTypes);
            return node;
        }
        public IMetaSelfNode CreateSelfNode(TypedKey key, ArtifactType[] targetArtifactTypes, IEnumerable<InputConfiguration> inputs, string sourceCode)
        {
            if (GetNode(key) != null)
                throw new InvalidOperationException(); //TODO
            var node = metaFactory.CreateMetaSelfNode(this, key, targetArtifactTypes, inputs, sourceCode);
            var deps = inputs.Select(input =>
            {
                var inputKey = input.Key;
                var locality = input.Locality;
                var inputNode = GetNode(inputKey);
                if (inputNode == null)
                {
                    if (inputKey == key && locality == DependencyLocality.Children)
                        inputNode = node;
                    else
                        throw new InvalidOperationException(); //TODO
                }
                return metaFactory.CreateMetaDependency(inputNode, node, input.ParameterName, locality);
            }).ToArray();

            this.nodes.Add(key, node);
            foreach(var dependency in deps)
            {
                this.dependencies.Add(dependency);
                this.dependenciesBySource.GetOrLazyInsert(dependency.Source, () => new List<IMetaDependency>())
                    .Add(dependency);
                this.dependenciesByTarget.GetOrLazyInsert(dependency.Target, () => new List<IMetaDependency>())
                    .Add(dependency);
            }

            foreach (var graph in Instances.Values)
                graph.Storage.Allocate(node, targetArtifactTypes);

            return node;
        }

        public IEnumerable<IMetaDependency> GetInputsOf(IMetaNode node)
        {
            List<IMetaDependency> result;
            if (dependenciesByTarget.TryGetValue(node, out result))
                return result;
            return Enumerable.Empty<IMetaDependency>();
        }

        public IMetaNode GetNode(TypedKey key)
        {
            IMetaNode result;
            if (nodes.TryGetValue(key, out result))
                return result;
            return null;
        }

        public IEnumerable<IMetaDependency> GetOutputsOf(IMetaNode node)
        {
            List<IMetaDependency> result;
            if (dependenciesBySource.TryGetValue(node, out result))
                return result;
            return Enumerable.Empty<IMetaDependency>();
        }

        public void RemoveNode(IMetaNode node)
        {
            if (!nodes.ContainsKey(node.Key))
                throw new InvalidOperationException(); //TODO
            var outs = GetOutputsOf(node);
            var ins = GetInputsOf(node);
            foreach (var @out in outs.ToArray())
            {
                dependencies.Remove(@out);
                RemoveNode(@out.Target);
            }
            foreach(var @in in ins) dependencies.Remove(@in);
            dependenciesBySource.Remove(node);
            dependenciesByTarget.Remove(node);
            nodes.Remove(node.Key);
            if (node is IMetaRawNode)
                Storage.Free(node);
            else
                foreach (var graph in Instances.Values)
                    graph.Storage.Free(node);
        }

        public IGraph CreateGraph(IMetaGraph metaComputationGraph, ITagExpression tagExpression, IValueStorageFactory storageFactory)
        {
            return new Graph(metaComputationGraph, tagExpression, storageFactory);
        }

        public void Compute()
        {
            
        }
    }
}
