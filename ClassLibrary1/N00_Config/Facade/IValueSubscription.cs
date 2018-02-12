using System;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueSubscription: IDisposable
    {
        object Value { get; }
        event EventHandler ValueChanged;
        void NotifyChanged();
    }
}
