using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueSubscription: IValueSubscription
    {
        private Action dispose;
        private IValueCell cell;
        
        public ValueSubscription(IValueCell cell, Action dispose)
        {
            this.cell = cell;
            this.dispose = dispose;
        }

        public void Dispose()
        {
            dispose?.Invoke();
            dispose = null;
            cell = null;
        }


        public object Value { get { return cell?.Value; } }
        public event EventHandler ValueChanged;
        public void NotifyChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
