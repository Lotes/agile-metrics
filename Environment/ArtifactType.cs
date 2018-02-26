using ClassLibrary1.E00_Addons;
using System.Collections.Generic;

namespace ClassLibrary1.E01_Artifacts
{
    public sealed class ArtifactType
    {
        private static Dictionary<string, ArtifactType> instances = new Dictionary<string, ArtifactType>();
        public static ArtifactType Create(string name)
        {
            name = name.ToUpper();
            return instances.GetOrLazyInsert(name, () => new ArtifactType(name));
        }

        private ArtifactType(string name)
        {
            this.Name = name;
        }
        public string Name { get; }

        public static implicit operator ArtifactType(string str)
        {
            return ArtifactType.Create(str);
        }
    }
}