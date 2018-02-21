using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VDefineTemplate : VDefinition
    {
        public VDefineTemplate(XDefineTemplate xDef) : base(xDef)
        {
            Definitions = new ObservableCollection<VDefinition>(xDef.Definitions.Select(VConfig.ConvertXDefs2VDefs));
            Parameters = new ObservableCollection<VParameter>(xDef.Parameters.Select(xP => new VParameter(xP)));
            Variables = new ObservableCollection<VVariable>(xDef.Variables.Select(xV => new VVariable(xV)));
        }
        public ObservableCollection<VDefinition> Definitions { get; private set; }
        public ObservableCollection<VParameter> Parameters { get; private set; }
        public ObservableCollection<VVariable> Variables { get; private set; }
    }
}
