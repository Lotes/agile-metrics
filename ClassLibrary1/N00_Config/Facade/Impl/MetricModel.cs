using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class MetricModel : IMetricModel
    {
        private readonly IMetaGraph metaGraph;
        private readonly IValueStorageFactory storageFactory;

        public MetricModel(IMetaGraphFactory factory, IValueStorageFactory storageFactory)
        {
            this.metaGraph = factory.CreateMetaGraph(storageFactory);
            this.storageFactory = storageFactory;
        }

        public IMetaBuilder<TResult> BeginDefinition<TResult>(TypedKey<TResult> key, params ArtifactType[] allowedTypes)
        {
            return new MetaBuilder<TResult>(metaGraph, key, allowedTypes);
        }

        public void DeclareRaw(TypedKey key, ImporterType source, params ArtifactType[] allowedTypes)
        {
            metaGraph.CreateRawNode(key, source, allowedTypes);
        }

        public void SetRawValue(TypedKey key, IArtifact artifact, object value)
        {
            var metaNode = metaGraph.GetNode(key);
            if (!(metaNode is IMetaRawNode))
                throw new InvalidOperationException();
            metaGraph.Storage.SetValue(metaGraph.GetNode(key), artifact, value);
        }

        public IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn<TResult>(ITagExpression tagExpression, TypedKey<TResult> key, IEnumerable<IArtifact> artifacts)
        {
            if (!metaGraph.Instances.ContainsKey(tagExpression))
                metaGraph.CreateInstanceFor(tagExpression);
            return metaGraph.Instances[tagExpression].SubscribeOn(key, artifacts);
        }
    }
}
