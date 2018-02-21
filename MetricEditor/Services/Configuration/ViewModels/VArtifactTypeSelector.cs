using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public abstract class VArtifactTypeSelector : ErrorTrackingViewModel
    {
        public VArtifactTypeSelector(XArtifactTypeSelector xSelector)
        {
            Documentation = new VTemplateString<string>(xSelector.Documentation);
        }

        public VTemplateString<string> Documentation { get; private set; }
    }
}
