using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Meta;
using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts.Impl;
using ClassLibrary1.N00_Config.Instance;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IMetricModel
    {
        void SetRawValue(TypedKey key, IArtifact artifact, object value);
        void DeclareRaw(TypedKey key, ImporterType source, params ArtifactType[] allowedTypes);
        IMetaBuilder<TResult> BeginDefinition<TResult>(TypedKey<TResult> key, params ArtifactType[] allowedTypes);
        IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn<TResult>(ITagExpression tagExpression, TypedKey<TResult> key, IEnumerable<IArtifact> artifacts);
    }
}
