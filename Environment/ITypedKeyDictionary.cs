using System.Collections.Generic;

namespace ClassLibrary1.E02_TypedKeys
{
    public interface ITypedKeyDictionary: IEnumerable<KeyValuePair<TypedKey, object>>
    {
        TType GetValue<TType>(TypedKey<TType> key);
    }
}