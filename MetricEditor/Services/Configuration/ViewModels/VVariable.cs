using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VVariable : ErrorTrackingViewModel
    {
        public VVariable(XVariable xV)
        {
            Name = new VTemplateString<string>(xV.Name);
            Value = new VTemplateString<object>(xV.Value);
        }
        public VTemplateString<string> Name { get; private set; }
        public VTemplateString<object> Value { get; private set; }
    }
}
