using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetricEditor.Services.Configuration.Serializable
{
    public abstract class XAbstractDataDefinition : XAbstractDefinition
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }
        [XmlArray("TargetArtifactTypes")]
        [XmlArrayItem("ArtifactType", typeof(XATSelectorDirect))]
        [XmlArrayItem("CopyOf", typeof(XATSelectorCopy))]
        [XmlArrayItem("AncestorsOf", typeof(XATSelectorAncestors))]
        public List<XArtifactTypeSelector> TargetArtifactTypes { get; set; }
    }
}
