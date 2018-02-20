using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetricEditor.Services.Configuration.Serializable
{
    public class XApplyTemplate : XAbstractDefinition
    {
        [XmlArray("Bindings")]
        [XmlArrayItem("Binding")]
        public List<XTemplateBinding> Bindings { get; set; }
    }
}
