using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VSourceCodeSection : ErrorTrackingViewModel
    {
        public VSourceCodeSection(XSourceCodeSection xCode)
        {
            Name = new VTemplateString<string>(xCode.Name);
            Code = new VTemplateString<string>(xCode.Code);
        }

        public VTemplateString<string> Code { get; private set; }
        public VTemplateString<string> Name { get; private set; }
    }
}
