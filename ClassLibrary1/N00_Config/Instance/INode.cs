using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Instance
{
    public interface INode
    {
        object Value { get; }
        bool Invalid { get; }
        void Validate();
        void Invalidate();
        IArtifact Artifact { get; }
        IMetaNode MetaNode { get; }
        IEnumerable<IDependency> Outputs { get; }
        IValueSubscription Subscribe();
    }
}