using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueCell : IValueCell
    {
        public bool IsValid { get; set; }
        public int ReferenceCount { get; set; }
        public object Value { get; set; }
    }
}
