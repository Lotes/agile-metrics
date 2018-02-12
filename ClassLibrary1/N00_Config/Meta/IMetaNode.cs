using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;

namespace ClassLibrary1.N00_Config.Meta
{
    public interface IMetaNode
    {
        TypedKey Key { get; }
        IEnumerable<ArtifactType> TargetArtifactTypes {get;}
        object InvalidValue { get; }
    }
}