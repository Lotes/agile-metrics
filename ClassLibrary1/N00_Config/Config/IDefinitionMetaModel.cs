using System.Collections.Generic;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Config
{
    public interface IDefinitionMetaModel
    {
        string ElementName { get; }
        IDefinitionMetaModel ParentMetaModel { get; }
        IReadOnlyDictionary<TypedKey, UsageAttribute> PropertyKeys { get; }
        IMetaNode Create(ITypedKeyDictionary properties);
    }
}