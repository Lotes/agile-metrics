using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetricEditor.Services.Configuration.Serializable
{
    public abstract class XAbstractDefinition
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }
        [XmlAttribute("Documentation")]
        public string Documentation { get; set; }
    }
}
