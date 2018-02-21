using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public abstract class VDataDefinition : VDefinition
    {
        public VDataDefinition(XAbstractDataDefinition xDataDef)
            : base(xDataDef)
        {
            Type = new VTemplateString<System.Type>(xDataDef.Type);
            TargetArtifactTypes = new ObservableCollection<VArtifactTypeSelector>(xDataDef.TargetArtifactTypes.Select<XArtifactTypeSelector, VArtifactTypeSelector>(xD =>
            {
                if (xD is XATSelectorAncestors)
                    return new VATSelectorAncestors(xD as XATSelectorAncestors);
                if (xD is XATSelectorCopy)
                    return new VATSelectorCopy(xD as XATSelectorCopy);
                if (xD is XATSelectorDirect)
                    return new VATSelectorDirect(xD as XATSelectorDirect);
                else
                    throw new InvalidOperationException("Unknown selector!");
            }));
        }
        public VTemplateString<Type> Type { get; private set; }
        public ObservableCollection<VArtifactTypeSelector> TargetArtifactTypes { get; set; }
    }
}
