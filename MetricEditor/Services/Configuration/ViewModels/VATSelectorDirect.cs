using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VATSelectorDirect : VArtifactTypeSelector
    {
        public VATSelectorDirect(XATSelectorDirect xSelector)
            : base(xSelector)
        {
            Direct = new VTemplateString<VId>(xSelector.Value);
        }
        public VTemplateString<VId> Direct { get; private set; }
    }
}
