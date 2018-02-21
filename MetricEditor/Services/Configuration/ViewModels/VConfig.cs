using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using GalaSoft.MvvmLight;
using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VConfig: ErrorTrackingViewModel 
    {
        public VConfig(XConfig xConfig)
        {
            Definitions = new ObservableCollection<VDefinition>(xConfig.Definitions.Select<XAbstractDefinition, VDefinition>(xD => 
            {
                if (xD is XApplyTemplate)
                    return new VApplyTemplate(xD as XApplyTemplate);
                if (xD is XDeclareRaw)
                    return new VDeclareRaw(xD as XDeclareRaw);
                if (xD is XDefineComputed)
                    return new VDefineComputed(xD as XDefineComputed);
                if (xD is XDefineTemplate)
                    return new VDefineTemplate(xD as XDefineTemplate);
                throw new InvalidOperationException("Unknown definition!");
            }));
            Documentation = new VTemplateString<string>(xConfig.Documentation);
        }
        public ObservableCollection<VDefinition> Definitions { get; private set; }
        public VTemplateString<string> Documentation { get; private set; }
    }
}
