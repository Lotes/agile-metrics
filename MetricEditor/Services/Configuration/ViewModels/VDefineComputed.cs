using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VDefineComputed : VDataDefinition
    {
        public VDefineComputed(XDefineComputed xDef)
            : base(xDef)
        {
            xDef.Inputs; xDef.SourceCodeSections
        }
    }
}
