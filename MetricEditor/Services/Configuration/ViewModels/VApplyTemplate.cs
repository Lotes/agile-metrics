using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VApplyTemplate : VDefinition
    {
        public VApplyTemplate(XApplyTemplate xDef) : base(xDef)
        {
            xDef.Bindings;
        }
    }
}
