using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VApplyTemplate : VDefinition
    {
        public VApplyTemplate(XApplyTemplate xDef) 
            : base(xDef)
        {
            Bindings = new ObservableCollection<VBoundParameter>(xDef.Bindings.Select(xB => new VBoundParameter(xB)));
        }

        public ObservableCollection<VBoundParameter> Bindings { get; private set; }
    }

    public class VBoundParameter: ErrorTrackingViewModel
    {
        public VBoundParameter(XTemplateBinding xBinding)
        {
            Name = new VTemplateString<string>(xBinding.Name);
            Value = new VTemplateString<object>(xBinding.Value);
        }
        public VTemplateString<string> Name { get; private set; }
        public VTemplateString<object> Value { get; private set; }
    }
}
