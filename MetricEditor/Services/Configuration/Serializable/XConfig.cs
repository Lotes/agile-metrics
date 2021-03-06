﻿using System;
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
}
