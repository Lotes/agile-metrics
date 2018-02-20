using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetricEditor.Services.Configuration.Serializable
{
    public class XDefineTemplate : XAbstractDefinition
    {
        [XmlArray("Parameters")]
        [XmlArrayItem("Parameter")]
        public List<XParameter> Parameters { get; set; }
        [XmlArray("Variables")]
        [XmlArrayItem("Var")]
        public List<XVariable> Variables { get; set; }
        [XmlArray("Definitions")]
        [XmlArrayItem(ElementName = "DeclareRaw", Type = typeof(XDeclareRaw))]
        [XmlArrayItem(ElementName = "DefineComputed", Type = typeof(XDefineComputed))]
        [XmlArrayItem(ElementName = "DefineTemplate", Type = typeof(XDefineTemplate))]
        [XmlArrayItem(ElementName = "ApplyTemplate", Type = typeof(XApplyTemplate))]
        public List<XAbstractDefinition> Definitions { get; set; }
    }
}
