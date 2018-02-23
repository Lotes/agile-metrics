using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueStorage
    {
        IEnumerable<IMetaNode> Keys { get; }
        IValueCell GetValue(IMetaNode key, IArtifact artifact);
        IValueCell SetValue(IMetaNode key, IArtifact artifact, object value);
        void Allocate(IMetaNode key, IEnumerable<ArtifactType> artifactTypes);
        void Free(IMetaNode key);
    }
}
