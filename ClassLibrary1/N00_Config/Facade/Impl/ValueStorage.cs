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
            CheckIfAllocated(key, artifact);
            return values[key][artifact.ArtifactType].GetValue(artifact);
        }
        public IValueCell SetValue(IMetaNode key, IArtifact artifact, object value)
        {
            CheckIfAllocated(key, artifact);
            return values[key][artifact.ArtifactType].SetValue(artifact, value);
        }

        private void CheckIfAllocated(IMetaNode key, IArtifact artifact)
        {
            if (!values.ContainsKey(key))
                throw new InvalidOperationException(); //TODO
            if (!values[key].ContainsKey(artifact.ArtifactType))
                throw new InvalidOperationException(); //TODO
        }

        public void Allocate(IMetaNode key, IEnumerable<ArtifactType> artifactTypes)
        {
            var byType = artifactTypes.ToDictionary(a => a, factory.CreateValueSet);
            values.Add(key, byType);
        }

        public void Free(IMetaNode key)
        {
            values.Remove(key);
        }
    }
}
