using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.N00_Config.Meta.Impl
{
    public class MetaDependency : IMetaDependency
    {
        public MetaDependency(IMetaNode source, IMetaNode target, string name, DependencyLocality locality)
        {
            Source = source;
            Target = target;
            Name = name;
            Locality = locality;
        }

        public IMetaNode Source { get; private set; }
        public IMetaNode Target { get; private set; }
        public string Name { get; private set; }
        public DependencyLocality Locality { get; private set; }
    }
}
