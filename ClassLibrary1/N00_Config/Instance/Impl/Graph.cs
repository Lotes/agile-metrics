using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E00_Addons;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Meta;
using ClassLibrary1.N00_Config.Facade.Impl;

namespace ClassLibrary1.N00_Config.Instance.Impl
{
    public class Graph: IGraph
    {
        private static readonly Dictionary<IArtifact, INode> EmptyNodes = new Dictionary<IArtifact, INode>();

        private readonly ITagExpression expression;
        private readonly Dictionary<TypedKey, Dictionary<IArtifact, INode>> nodes = new Dictionary<TypedKey, Dictionary<IArtifact, INode>>();
        private readonly HashSet<IDependency> dependencies = new HashSet<IDependency>();
        private readonly Dictionary<INode, List<IDependency>> dependenciesBySource = new Dictionary<INode, List<IDependency>>();
        private readonly Dictionary<INode, List<IDependency>> dependenciesByTarget = new Dictionary<INode, List<IDependency>>();
        private readonly IGraphFactory factory;
        private readonly IValueStorage storage;
        
        public Graph(IGraphFactory factory, IMetaGraph metaGraph, ITagExpression expression, IValueStorageFactory storageFactory)
        {
            this.storage = storageFactory.CreateStorage();
            this.MetaGraph = metaGraph;
            this.factory = factory;
            this.MetaGraph = metaGraph;
            this.expression = expression;
            foreach (var node in metaGraph.MetaNodes.OfType<IMetaSelfNode>())
                storage.Allocate(node.Key, node.TargetArtifactTypes);
        }

        public IMetaGraph MetaGraph { get; }
        public IEnumerable<INode> Nodes { get { return nodes.Values.SelectMany(v => v.Values); } }
        public IEnumerable<IDependency> Dependencies { get { return dependencies; } }

        public ITagExpression TagExpression
        {
            get
            {
                return expression;
            }
        }

        public IValueStorage Storage
        {
            get
            {
                return storage;
            }
        }

        public INode GetNode(IMetaNode metaNode, IArtifact artifact)
        {
            //if(metaNode is IMetaRawNode)
                //return MetaGraph.
            return nodes.GetOrDefault(metaNode.Key, EmptyNodes).GetOrDefault(artifact, null);
        }

        private void AddDependency(IMetaDependency metaDependency, INode source, INode target)
        {
            if (source == null || target == null)
                return;
            //Console.WriteLine(source.Artifact.Name + " --> " + target.Artifact.Name+" : "+metaDependency.Name);
            var dependency = factory.CreateDependency(metaDependency, source, target);
            dependencies.Add(dependency);
            dependenciesBySource.GetOrLazyInsert(source, () => new List<IDependency>())
                .Add(dependency);
            dependenciesByTarget.GetOrLazyInsert(target, () => new List<IDependency>())
                .Add(dependency);
        }

        public INode AddNode(IMetaNode metaNode, IArtifact artifact)
        {
            if (!metaNode.TargetArtifactTypes.Contains(artifact.ArtifactType))
                return null;

            if (metaNode is IMetaRawNode)
                return MetaGraph.AddOrGetRawInstanceNode((IMetaRawNode)metaNode, artifact);

            var node = GetNode(metaNode, artifact);
            if (node == null)
            {
                node = factory.CreateSelfNode(this, metaNode, artifact);
                metaNode.ForEachInput((metaDependency, metaSource) =>
                {
                    if (metaDependency.Locality == DependencyLocality.Self)
                    {
                        var source = AddNode(metaSource, artifact);
                        AddDependency(metaDependency, source, node);
                    }
                    else
                    {
                        foreach (var child in artifact.Children)
                        {
                            var source = AddNode(metaSource, child);
                            AddDependency(metaDependency, source, node);
                        }
                    }
                });
            }
            nodes.GetOrLazyInsert(metaNode.Key, () => new Dictionary<IArtifact, INode>())
                [artifact] = node;
            return node;
        }

        private static readonly List<IDependency> EmptyList = new List<IDependency>();
        public IEnumerable<IDependency> GetInputsOf(INode node)
        {
            return dependenciesByTarget.GetOrDefault(node, EmptyList);
        }

        public IEnumerable<IDependency> GetOutputsOf(INode node)
        {
            return dependenciesBySource.GetOrDefault(node, EmptyList);
        }

        public IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn(TypedKey key, IEnumerable<IArtifact> artifacts)
        {
            var metaNode = MetaGraph.GetNode(key);
            return artifacts.ToDictionary(a => a, a => AddNode(metaNode, a)?.Subscribe());
        }
    }
}
