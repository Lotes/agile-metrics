using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VATSelectorAncestors : VArtifactTypeSelector
    {
        public VATSelectorAncestors(XATSelectorAncestors xSelector)
            : base(xSelector)
        {
            AncestorsOf = new VTemplateString<VId>(xSelector.Value);
        }
        public VTemplateString<VId> AncestorsOf { get; private set; }
    }
}
