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
        ValueCellState State { get; }
        int ReferenceCount { get; }
        void IncReferenceCount();
        void DecReferenceCount();
        event EventHandler<OldNewPair<ValueCellState>> StateChanged;
    }
}
