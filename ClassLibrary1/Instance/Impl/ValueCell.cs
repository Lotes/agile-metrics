using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueCell : IValueCell
    {
        private object value;
        private ValueCellState state;
        private int referenceCount;
        public ValueCell()
        {
            state = ValueCellState.Invalid;
            referenceCount = 0;
            value = null;
        }
        public int ReferenceCount { get { return referenceCount; } }
        public ValueCellState State
        {
            get { return state; }
            set
            {
                var old = state;
                state = value;
                StateChanged?.Invoke(this, new OldNewPair<ValueCellState>(old, value));
            }
        }
        public object Value
        {
            get
            {
                switch(State)
                {
                    case ValueCellState.Invalid:
                        return null;
                    case ValueCellState.Valid:
                        return value;
                }
                throw new InvalidOperationException("Unknown state!");
            }
            set
            {
                this.value = value;
                State = ValueCellState.Valid;
            }
        }
        public event EventHandler<OldNewPair<ValueCellState>> StateChanged;

        public void IncReferenceCount()
        {
            referenceCount++;
        }

        public void DecReferenceCount()
        {
            referenceCount--;
        }
    }
}
