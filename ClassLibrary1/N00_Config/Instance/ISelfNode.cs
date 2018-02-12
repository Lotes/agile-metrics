using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Instance
{
    public interface ISelfNode : INode
    {
        IReadOnlyDictionary<IMetaDependency, IEnumerable<IDependency>> Inputs { get; }
    }
}
