using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.N00_Config.Instance
{
    public static class NodeExtensions
    {
        public static IEnumerable<ArtifactType> GetTargetArtifactTypes(this INode node) { return node.MetaNode.TargetArtifactTypes; }
    }
}
