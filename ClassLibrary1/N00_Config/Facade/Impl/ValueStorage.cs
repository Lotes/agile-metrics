using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.E00_Addons;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueStorage: IValueStorage
    {
        private readonly Dictionary<TypedKey, Dictionary<ArtifactType, IValueSet>> values =
            new Dictionary<TypedKey, Dictionary<ArtifactType, IValueSet>>();
        private readonly IValueStorageFactory factory;

        public ValueStorage(IValueStorageFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<TypedKey> Keys { get { return values.Keys; } }
        public object GetValue(TypedKey key, IArtifact artifact)
        {
            CheckIfAllocated(key, artifact);
            return values[key][artifact.ArtifactType].GetValue(artifact);
        }
        public void SetValue(TypedKey key, IArtifact artifact, object value)
        {
            CheckIfAllocated(key, artifact);
            values[key][artifact.ArtifactType].SetValue(artifact, value);
        }

        private void CheckIfAllocated(TypedKey key, IArtifact artifact)
        {
            if (!values.ContainsKey(key))
                throw new InvalidOperationException(); //TODO
            if (!values[key].ContainsKey(artifact.ArtifactType))
                throw new InvalidOperationException(); //TODO
        }

        public void Allocate(TypedKey key, IEnumerable<ArtifactType> artifactTypes)
        {
            var byType = artifactTypes.ToDictionary(a => a, factory.CreateValueSet);
            values.Add(key, byType);
        }

        public void Free(TypedKey key)
        {
            values.Remove(key);
        }
    }
}
