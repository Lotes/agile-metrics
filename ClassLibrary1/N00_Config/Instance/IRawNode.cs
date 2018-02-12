using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.N00_Config.Instance
{
    public interface IRawNode : INode
    {
        new object Value { get; set; }
    }
}
