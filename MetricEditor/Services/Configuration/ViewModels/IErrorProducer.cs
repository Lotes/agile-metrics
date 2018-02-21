using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public interface IErrorProducer
    {
        ObservableCollection<IError> Errors { get; }
    }
}
