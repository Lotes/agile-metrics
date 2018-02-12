using System;

namespace ClassLibrary1.N00_Config.Meta
{
    public interface IMetaDependency
    {
        IMetaNode Source { get; }
        IMetaNode Target { get; }
        string Name { get; }
        DependencyLocality Locality { get; }
    }
}