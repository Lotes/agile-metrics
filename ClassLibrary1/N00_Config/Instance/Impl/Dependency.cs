using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Instance.Impl
{
    public class Dependency: IDependency
    {
        public Dependency(IMetaDependency metaDependency, INode source, INode target)
        {
            MetaDependency = metaDependency;
            Source = source;
            Target = target;
        }

        public IMetaDependency MetaDependency { get; }
        public INode Source { get; }
        public INode Target { get; }
        public void Invalidate()
        {
            Target.Invalidate();
        }
    }
}
