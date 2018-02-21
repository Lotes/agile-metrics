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
        private readonly ITagExpression expression;
        private readonly IValueStorage storage;
        
        public Graph(IMetaGraph metaGraph, ITagExpression expression, IValueStorageFactory storageFactory)
        {
            this.storage = storageFactory.CreateStorage();
            this.MetaGraph = metaGraph;
            this.MetaGraph = metaGraph;
            this.expression = expression;
            foreach (var node in metaGraph.MetaNodes.OfType<IMetaSelfNode>())
                storage.Allocate(node.Key, node.TargetArtifactTypes);
        }

        public IMetaGraph MetaGraph { get; }

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

        public IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn(TypedKey key, IEnumerable<IArtifact> artifacts)
        {
            var metaNode = MetaGraph.GetNode(key);
            return artifacts.ToDictionary(a => a, a => Storage.SubscribeOn(key, a));
        }
    }
}
