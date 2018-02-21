using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueSubscription: IValueSubscription
    {
        private Action dispose;
        private IArtifact artifact;
        private IValueSet set;
        public ValueSubscription(IArtifact artifact, IValueSet set, Action dispose)
        {
            this.artifact = artifact;
            this.set = set;
            this.dispose = dispose;
        }
        public void Dispose()
        {
            dispose?.Invoke();
            dispose = null;
            set = null;
            artifact = null;
        }


        public object Value { get { return set?.GetValue(artifact); } }
        public event EventHandler ValueChanged;
        public void NotifyChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
