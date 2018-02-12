using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Instance
{
    public interface IDependency
    {
        INode Source { get; }
        INode Target { get; }
        IMetaDependency MetaDependency { get; }
        void Invalidate();
    }
}