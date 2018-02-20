using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetricEditor.Services.Configuration.Serializable
{
    public abstract class XArtifactTypeSelector
    {
        [XmlText]
        public string Value { get; set; }
        [XmlAttribute("Documentation")]
        public string Documentation { get; set; }
    }

    public class XATSelectorCopy : XArtifactTypeSelector { }
    public class XATSelectorAncestors : XArtifactTypeSelector { }
    public class XATSelectorDirect : XArtifactTypeSelector { }
}
