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
        IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn(TypedKey metricKey,
            IEnumerable<IArtifact> artifacts);
    }
}