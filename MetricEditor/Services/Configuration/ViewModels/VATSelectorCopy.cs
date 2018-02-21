using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VATSelectorCopy : VArtifactTypeSelector
    {
        public VATSelectorCopy(XATSelectorCopy xSelector)
            : base(xSelector)
        {
            CopyOf = new VTemplateString<VId>(xSelector.Value);
        }
        public VTemplateString<VId> CopyOf { get; private set; }
    }
}
