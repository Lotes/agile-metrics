using ClassLibrary1.E02_TypedKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.E01_Artifacts;

namespace ClassLibrary1.N00_Config.Meta
{
    public interface IMetaSelfNode : IMetaNode
    {
        IReadOnlyDictionary<string, IMetaDependency> Inputs { get; }
        object Compute(IGraph graph, IArtifact artifact);
    }
}
