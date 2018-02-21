using ClassLibrary1.E01_Artifacts;
using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VDeclareRaw : VDataDefinition
    {
        public VDeclareRaw(XDeclareRaw xDef)
            : base(xDef)
        {
            Source = new VTemplateString<ImporterType>(xDef.Source);
        }
        public VTemplateString<ImporterType> Source { get; }
    }
}
