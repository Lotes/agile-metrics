using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Instance;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueStorageFactory
    {
        IValueStorage CreateStorage();
        IValueSet CreateValueSet(ArtifactType type);
    }
}
