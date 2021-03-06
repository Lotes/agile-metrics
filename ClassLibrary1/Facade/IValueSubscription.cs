﻿using System;
using System.ComponentModel;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueSubscription: IDisposable, INotifyPropertyChanged
    {
        object ValueAsync { get; }
        object ValueSync { get; }
        ValueCellState State { get; }
    }
}
