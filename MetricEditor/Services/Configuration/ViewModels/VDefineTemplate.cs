using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VDefineTemplate : VDefinition
    {
        public VDefineTemplate(XDefineTemplate xDef) : base(xDef)
        {
            xDef.Definitions;
            xDef.Parameters;
            xDef.Variables;
        }
    }
}
