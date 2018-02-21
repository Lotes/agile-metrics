using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public interface IError
    {
        string Message { get; }
        Uri Location { get; }
    }
}
