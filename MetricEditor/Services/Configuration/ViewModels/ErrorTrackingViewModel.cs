using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public abstract class ErrorTrackingViewModel : ViewModelBase, IErrorProducer
    {
        public ErrorTrackingViewModel()
        {
            Errors = new ObservableCollection<IError>();
            Children = new ObservableCollection<IErrorProducer>();
        }
        public ObservableCollection<IError> Errors { get; private set; }
        public ObservableCollection<IErrorProducer> Children { get; private set; }
    }
}
