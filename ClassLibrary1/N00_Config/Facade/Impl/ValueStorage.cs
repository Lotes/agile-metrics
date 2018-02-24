using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.E00_Addons;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueStorage: IValueStorage
    {
        private readonly Dictionary<IMetaNode, Dictionary<ArtifactType, IValueSet>> values =
            new Dictionary<IMetaNode, Dictionary<ArtifactType, IValueSet>>();
        private readonly IValueStorageFactory factory;

        public ValueStorage(IValueStorageFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<IMetaNode> Keys { get { return values.Keys; } }
        public IValueCell GetValue(IMetaNode key, IArtifact artifact)
        {
            if(CheckIfAllocated(key, artifact))
                return values[key][artifact.ArtifactType].GetValue(artifact);
            return null;
        }
        public IValueCell SetValue(IMetaNode key, IArtifact artifact, object value)
        {
            if(CheckIfAllocated(key, artifact))
                return values[key][artifact.ArtifactType].SetValue(artifact, value);
            return null;
        }

        private bool CheckIfAllocated(IMetaNode key, IArtifact artifact)
        {
            if (!values.ContainsKey(key))
                return false;
            if (!values[key].ContainsKey(artifact.ArtifactType))
                return false;
            return true;
        }

        public void Allocate(IMetaNode key, IEnumerable<ArtifactType> artifactTypes)
        {
            var byType = artifactTypes.ToDictionary(a => a, a => factory.CreateValueSet(key, a));
            values.Add(key, byType);
        }

        public void Free(IMetaNode key)
        {
            values.Remove(key);
        }

        public void ClearValue(IMetaNode key, IArtifact artifact)
        {
            if (values.ContainsKey(key) && values[key].ContainsKey(artifact.ArtifactType))
                values[key][artifact.ArtifactType].ClearValue(artifact);
        }
    }
}
