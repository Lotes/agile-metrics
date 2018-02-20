using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetricEditor.Services.Configuration.Serializable
{
    public class XDefineComputed : XAbstractDataDefinition
    {
        [XmlArray("Inputs")]
        [XmlArrayItem("In")]
        public List<XInput> Inputs { get; set; }
        [XmlArray("Variables")]
        [XmlArrayItem("Var")]
        public List<XVariable> Variables { get; set; }
        [XmlArray("SourceCode")]
        [XmlArrayItem("Section")]
        public List<XSourceCodeSection> SourceCodeSections { get; set; }
    }
}
