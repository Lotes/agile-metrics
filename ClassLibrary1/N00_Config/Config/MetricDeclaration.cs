using System;
using System.Collections.Generic;
using ClassLibrary1.E00_Addons;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Config
{
    [Addon]
    public class MetricDeclaration : IMetricDefinitionExtension
    {
        public TypedKey<ImporterType> Key_Source = new TypedKey<ImporterType>("Source");
        public TypedKey<IEnumerable<IDependency>> Key_Dependencies = new TypedKey<IEnumerable<IDependency>>("Dependencies");

        public string ElementName
        {
            get
            {
                return "DeclareMetric";
            }
        }

        public IReadOnlyDictionary<TypedKey, UsageAttribute> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IMetaNode CreateDefinition(ITypedKeyDictionary properties)
        {
            throw new NotImplementedException();
        }
    }
}