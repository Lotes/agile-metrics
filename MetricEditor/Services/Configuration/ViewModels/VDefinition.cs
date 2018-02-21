using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public abstract class VDefinition : ErrorTrackingViewModel
    {
        public VDefinition(XAbstractDefinition xDef)
        {
            Id = new VTemplateString<VId>(xDef.Id);
            Documentation = new VTemplateString<string>(xDef.Documentation);
            Children.Add(Id);
            Children.Add(Documentation);
        }
        public VTemplateString<string> Documentation { get; private set; }
        public VTemplateString<VId> Id { get; private set; }
    }
}
