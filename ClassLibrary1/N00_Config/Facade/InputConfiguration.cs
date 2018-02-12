using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Facade
{
    public class InputConfiguration
    {
        public InputConfiguration(TypedKey key, DependencyLocality locality, string parameterName)
        {
            Key = key;
            Locality = locality;
            ParameterName = parameterName;
        }

        public string ParameterName { get; private set; }
        public TypedKey Key { get; private set; }
        public DependencyLocality Locality { get; private set; }
    }
}