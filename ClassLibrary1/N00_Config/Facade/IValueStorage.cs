using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Instance;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueStorage
    {
        IEnumerable<TypedKey> Keys { get; }
        object GetValue(TypedKey key, IArtifact artifact);
        void SetValue(TypedKey key, IArtifact artifact, object value);
        void Allocate(TypedKey key, IEnumerable<ArtifactType> artifactTypes);
        void Free(TypedKey key);
    }
}
