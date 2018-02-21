using ClassLibrary1.N00_Config.Meta;
using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VInput : ErrorTrackingViewModel
    {
        public VInput(XInput xInput)
        {
            Name = new VTemplateString<string>(xInput.Name);
            ReferenceId = new VTemplateString<VId>(xInput.ReferencedId);
            Locality = new VTemplateString<DependencyLocality>(xInput.ReferencedId);
        }
        public VTemplateString<DependencyLocality> Locality { get; private set; }
        public VTemplateString<string> Name { get; private set; }
        public VTemplateString<VId> ReferenceId { get; private set; }
    }
}
