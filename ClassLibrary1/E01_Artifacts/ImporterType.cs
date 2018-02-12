using ClassLibrary1.E00_Addons;
using System.Collections.Generic;

namespace ClassLibrary1.E01_Artifacts
{
    public sealed class ImporterType
    {
        private static Dictionary<string, ImporterType> instances = new Dictionary<string, ImporterType>();
        public static ImporterType Create(string name)
        {
            name = name.ToUpper();
            return instances.GetOrLazyInsert(name, () => new ImporterType(name));
        }

        private ImporterType(string name)
        {
            this.Name = name;
        }
        public string Name { get; }

        public static implicit operator ImporterType(string str)
        {
            return ImporterType.Create(str);
        }
    }
}