using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Meta;
using Metrics.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Instance
{
    public static class GraphInvalidator
    {
        /// <summary>
        /// Helper function for invalidating values.
        /// Graph is NULL when the affected node is RAW.
        /// Affected node is NULL, when all nodes have to be invalidated.
        /// </summary>
        /// <param name="metaGraph"></param>
        /// <param name="graph">can be null</param>
        /// <param name="affectedNode">can be null</param>
        /// <param name="queue"></param>
        /// <param name="artifacts"></param>
        public static void Invalidate(IMetaGraph metaGraph, IGraph currentGraph,
            IMetaNode affectedNode, IExecutionQueue queue, params IArtifact[] artifacts)
        {
            var nodes = affectedNode == null
                ? metaGraph.MetaNodes.OfType<IMetaSelfNode>().Cast<IMetaNode>()
                : new[] { affectedNode };
            var graphs = currentGraph == null
                ? metaGraph.Instances.Values
                : new[] { currentGraph };
            foreach(var graph in graphs)
            foreach(var node in nodes)
            {
                var storage = node is IMetaRawNode ? metaGraph.Storage : graph.Storage;
                foreach (var artifact in artifacts)
                {
                    var valueCell = storage.GetValue(node, artifact);
                    if (valueCell != null)
                    {
                        valueCell.State = ValueCellState.Invalid;
                        if (graph != null && node is IMetaSelfNode)
                            queue.Enqueue(graph, (IMetaSelfNode)node, artifact);
                        node.ForEachOutput((dependency, target) =>
                        {
                            if (dependency.Locality == DependencyLocality.Self)
                            {
                                if(!(target is IMetaRawNode))
                                    Invalidate(metaGraph, graph, target, queue, artifact);
                            }
                            else if (artifact.Parent != null)
                                Invalidate(metaGraph, graph, target, queue, artifact.Parent);
                        });
                    }
                }
            }
        }
    }
}
