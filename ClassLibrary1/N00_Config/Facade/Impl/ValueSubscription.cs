using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Instance;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueSubscription: IValueSubscription
    {
        private Action dispose;
        private INode node;
        public ValueSubscription(INode node, Action dispose)
        {
            this.node = node;
            this.dispose = dispose;
        }
        public void Dispose()
        {
            dispose?.Invoke();
            dispose = null;
            node = null;
        }


        public object Value { get { return node?.Value; } }
        public event EventHandler ValueChanged;
        public void NotifyChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
