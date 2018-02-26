using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueStorageFactory
    {
        IValueStorage CreateStorage();
        IValueSet CreateValueSet(IMetaNode node, ArtifactType type);
    }
}
