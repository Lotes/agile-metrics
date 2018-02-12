using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Instance
{
    public interface IGraph
    {
        IMetaGraph MetaGraph { get; }
        ITagExpression TagExpression { get; }
        IValueStorage Storage { get; }

        IEnumerable<INode> Nodes { get; }
        INode GetNode(IMetaNode metaNode, IArtifact artifact);
        INode AddNode(IMetaNode metaNode, IArtifact artifact);

        IEnumerable<IDependency> Dependencies { get; }
        IEnumerable<IDependency> GetInputsOf(INode node);
        IEnumerable<IDependency> GetOutputsOf(INode node);

        IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn(TypedKey metricKey,
            IEnumerable<IArtifact> artifacts);
    }
}