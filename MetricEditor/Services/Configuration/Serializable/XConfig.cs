using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassLibrary1.N00_Config.Meta;

namespace MetricEditor.Services.Configuration.Serializable
{
    [XmlRoot(ElementName = "ComputationModel")]
    public class XConfig
    {
        [XmlArray("Definitions")]
        [XmlArrayItem(ElementName = "DeclareRaw", Type = typeof(XDeclareRaw))]
        [XmlArrayItem(ElementName = "DefineComputed", Type = typeof(XDefineComputed))]
        [XmlArrayItem(ElementName = "DefineTemplate", Type = typeof(XDefineTemplate))]
        [XmlArrayItem(ElementName = "ApplyTemplate", Type = typeof(XApplyTemplate))]
        public List<XAbstractDefinition> Definitions { get; set; }
        [XmlAttribute("Documentation")]
        public string Documentation { get; set; }
    }

    public abstract class XAbstractDefinition
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }
        [XmlAttribute("Documentation")]
        public string Documentation { get; set; }
    }

    public abstract class XAbstractDataDefinition: XAbstractDefinition
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }
        [XmlArray("TargetArtifactTypes")]
        [XmlArrayItem("ArtifactType", typeof(XATSelectorDirect))]
        [XmlArrayItem("CopyOf", typeof(XATSelectorCopy))]
        [XmlArrayItem("AncestorsOf", typeof(XATSelectorAncestors))]
        public List<XArtifactTypeSelector> TargetArtifactTypes { get; set; }
    }

    public class XDeclareRaw: XAbstractDataDefinition
    {
        [XmlAttribute("Source")]
        public string Source { get; set; }
    }

    public class XDefineComputed: XAbstractDataDefinition
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

    public class XDefineTemplate: XAbstractDefinition
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

    public class XParameter
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Type")]
        public string ParameterType { get; set; }
    }

    public class XApplyTemplate: XAbstractDefinition
    {
        [XmlArray("Bindings")]
        [XmlArrayItem("Binding")]
        public List<XTemplateBinding> Bindings { get; set; }
    }

    public class XTemplateBinding
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Value")]
        public string Value { get; set; }
    }

    public class XVariable
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Value")]
        public string Value { get; set; }
    }

    public class XSourceCodeSection
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlText]
        public string Code { get; set; }
    }

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

    public abstract class XArtifactTypeSelector
    {
        [XmlText]
        public string Value { get; set; }
        [XmlAttribute("Documentation")]
        public string Documentation { get; set; }
    }

    public class XATSelectorCopy: XArtifactTypeSelector { }
    public class XATSelectorAncestors: XArtifactTypeSelector { }
    public class XATSelectorDirect: XArtifactTypeSelector { }
}
