using ClassLibrary1.N00_Config.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetricEditor.Services.Configuration.Serializable
{
    public class XInput
    {
        [XmlAttribute("Locality")]
        public DependencyLocality Locality { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("RefId")]
        public string ReferencedId { get; set; }
        [XmlAttribute("Documentation")]
        public string Documentation { get; set; }
    }
}
