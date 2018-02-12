using ClassLibrary1.E01_Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.N00_Config.Meta
{
    public interface IMetaRawNode : IMetaNode
    {
        ImporterType Source { get; }
    }
}
