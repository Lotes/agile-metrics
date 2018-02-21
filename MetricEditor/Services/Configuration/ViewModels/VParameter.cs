using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VParameter : ErrorTrackingViewModel
    {
        public VParameter(XParameter xP)
            : base()
        {
            Name = new VTemplateString<string>(xP.Name);
            ParameterType = new VTemplateString<Type>(xP.ParameterType);
        }
        public VTemplateString<string> Name { get; private set; }
        public VTemplateString<Type> ParameterType { get; private set; }
    }
}
