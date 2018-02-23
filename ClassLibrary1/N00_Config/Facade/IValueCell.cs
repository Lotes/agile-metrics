using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueCell
    {
        object Value { get; set; }
        bool IsValid { get; set; }
        int ReferenceCount { get; set; }
    }
}
