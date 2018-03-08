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
using Metrics.Meta;

namespace ClassLibrary1.N00_Config.Instance.Impl
{
    public class Graph: IGraph
    {
        private readonly ITagExpression expression;
        private readonly IValueStorage storage;
        private readonly List<IValueSubscription> subscriptions = new List<IValueSubscription>();
        private readonly Func<bool> compute;
        public Graph(IMetaGraph metaGraph, ITagExpression expression, IValueStorageFactory storageFactory, Func<bool> compute)
        {
            this.compute = compute;
            this.storage = storageFactory.CreateStorage();
            this.MetaGraph = metaGraph;
            this.MetaGraph = metaGraph;
            this.expression = expression;
            foreach (var node in metaGraph.MetaNodes.OfType<IMetaSelfNode>())
                storage.Allocate(node, node.TargetArtifactTypes);
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

        public IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn(TypedKey key, IEnumerable<IArtifact> artifacts, IExecutionQueue queue)
        {
            var metaNode = MetaGraph.GetNode(key);
            return artifacts.ToDictionary(a => a, a => SubscribeOnCell(RequestCells(metaNode, a, queue), metaNode, a));
        }

        private IValueCell RequestCells(IMetaNode metaNode, IArtifact artifact, IExecutionQueue queue)
        {
            metaNode.ForEachInput((dependency, source) => 
            {
                if (dependency.Locality == DependencyLocality.Self)
                {
                    RequestCells(source, artifact, queue);
                }
                else
                {
                    foreach (var child in artifact.Children)
                        RequestCells(source, child, queue);
                }
            });
            var storage = metaNode is IMetaRawNode ? MetaGraph.Storage : this.storage;
            var cell = storage.GetValue(metaNode, artifact);
            cell?.IncReferenceCount();
            if (cell != null && metaNode is IMetaSelfNode && cell.State == ValueCellState.Invalid)
                queue.Enqueue(this, (IMetaSelfNode)metaNode, artifact); //depth first search
            return cell;
        }

        private void ReleaseCell(IMetaNode metaNode, IArtifact artifact)
        {
            metaNode.ForEachInput((dependency, source) =>
            {
                if (dependency.Locality == DependencyLocality.Self)
                {
                    ReleaseCell(source, artifact);
                }
                else
                {
                    foreach (var child in artifact.Children)
                        ReleaseCell(source, child);
                }
            });
            var storage = metaNode is IMetaRawNode ? MetaGraph.Storage : this.storage;
            var cell = storage.GetValue(metaNode, artifact);
            if(cell != null)
            {
                cell.DecReferenceCount();
                if (cell.ReferenceCount == 0)
                    storage.ClearValue(metaNode, artifact);
            }
        }

        private IValueSubscription SubscribeOnCell(IValueCell cell, IMetaNode metaNode, IArtifact artifact)
        {
            if(cell == null)
            {
                ReleaseCell(metaNode, artifact);
                return null;
            }
            else
            {
                ValueSubscription subscription = null;
                subscription = new ValueSubscription(cell, () =>
                {
                    ReleaseCell(metaNode, artifact);
                    subscriptions.Remove(subscription);
                }, () => 
                {
                    while(compute());
                });
                subscriptions.Add(subscription);
                return subscription;
            }
        }
    }
}
