using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetricEditor.Services.Configuration.Serializable
{
    public class XSourceCodeSection
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlText]
        public string Code { get; set; }
    }
}
