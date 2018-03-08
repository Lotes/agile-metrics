using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Meta;
using System.ComponentModel;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueSubscription: IValueSubscription
    {
        private Action dispose;
        private Action compute;
        private IValueCell cell;

        public event PropertyChangedEventHandler PropertyChanged;

        public object ValueAsync
        {
            get
            {
                return cell.Value;
            }
        }

        public ValueCellState State
        {
            get
            {
                return cell.State;
            }
        }

        public object ValueSync
        {
            get
            {
                if (State != ValueCellState.Valid)
                    compute();
                return ValueAsync;
            }
        }

        public ValueSubscription(IValueCell cell, Action dispose, Action compute)
        {
            this.cell = cell;
            this.dispose = dispose;
            this.compute = compute;
            Attach(cell);
        }

        public void Dispose()
        {
            Detach(cell);
            dispose?.Invoke();
            dispose = null;
            cell = null;
        }

        private void Attach(IValueCell cell)
        {
            cell.StateChanged += Cell_StateChanged;
        }

        private void Detach(IValueCell cell)
        {
            cell.StateChanged -= Cell_StateChanged;
        }

        private void Cell_StateChanged(object sender, OldNewPair<ValueCellState> e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
            if(State == ValueCellState.Valid || State == ValueCellState.Invalid)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueAsync)));
        }
    }
}
