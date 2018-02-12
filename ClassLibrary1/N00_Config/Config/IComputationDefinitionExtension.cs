using System.Collections.Generic;
using ClassLibrary1.E00_Addons;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Config
{
    [AddonInterface]
    public interface IComputationDefinitionExtension
    {
        string ElementName { get; }
        IReadOnlyDictionary<TypedKey, UsageAttribute> Keys { get; }
        IMetaNode CreateDefinition(ITypedKeyDictionary properties);
    }
}