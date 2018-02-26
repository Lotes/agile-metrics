using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Instance
{
    public static class GraphInvalidator
    {
        public static void Invalidate(IMetaGraph metaGraph, IValueStorage storage, IMetaNode affectedNode, params IArtifact[] artifacts)
        {
            var nodes = affectedNode == null 
                ? metaGraph.MetaNodes.OfType<IMetaSelfNode>().Cast<IMetaNode>() 
                : new[] { affectedNode };
            foreach(var node in nodes)
            {
                foreach (var artifact in artifacts)
                {
                    var valueCell = storage.GetValue(node, artifact);
                    if (valueCell != null)
                    {
                        valueCell.State = ValueCellState.Invalid;
                        node.ForEachOutput((dependency, target) =>
                        {
                            if (dependency.Locality == DependencyLocality.Self)
                                Invalidate(metaGraph, storage, (IMetaSelfNode)target, artifact);
                            else if (artifact.Parent != null)
                                Invalidate(metaGraph, storage, (IMetaSelfNode)target, artifact.Parent);
                        });
                    }
                }
            }
        }
    }
}
