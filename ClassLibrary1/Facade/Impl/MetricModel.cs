using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Meta;
using Environment;
using Metrics.E01_Artifacts;
using Metrics.Meta;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class MetricModel : IMetricModel
    {
        private readonly IMetaGraph metaGraph;
        private readonly IValueStorageFactory storageFactory;
        private IArtifactCatalog artifacts;
        private IFindingCatalog findings;
        private IExecutionQueue queue;
        public MetricModel(IMetaGraphFactory factory, IValueStorageFactory storageFactory)
        {
            this.metaGraph = factory.CreateMetaGraph(storageFactory);
            this.storageFactory = storageFactory;
            this.queue = new ExecutionQueue();
        }

        public void Attach(IArtifactCatalog artifacts, IFindingCatalog findings)
        {
            Detach();
            this.artifacts = artifacts;
            this.findings = findings;
            artifacts.Added += Artifacts_Added;
            artifacts.Moved += Artifacts_Moved;
            artifacts.Tagged += Artifacts_Tagged;
            findings.Added += Findings_Added;
            findings.Removed += Findings_Removed;
            findings.Moved += Findings_Moved;
            findings.ClassificationChanged += Findings_ClassificationChanged;
        }

        public void Detach()
        {
            artifacts.Added -= Artifacts_Added;
            artifacts.Moved -= Artifacts_Moved;
            artifacts.Tagged -= Artifacts_Tagged;
            findings.Added -= Findings_Added;
            findings.Removed -= Findings_Removed;
            findings.Moved -= Findings_Moved;
            findings.ClassificationChanged -= Findings_ClassificationChanged;
            this.artifacts = null;
            this.findings = null;
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
            metaGraph.Invalidate(metaNode, queue, artifact); //invalidate first, because SetValue makes the cell valid again
            metaGraph.Storage.SetValue(metaGraph.GetNode(key), artifact, value);
        }

        public IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn<TResult>(ITagExpression tagExpression, TypedKey<TResult> key, IEnumerable<IArtifact> artifacts)
        {
            if (!metaGraph.Instances.ContainsKey(tagExpression))
                metaGraph.CreateInstanceFor(tagExpression);
            return metaGraph.Instances[tagExpression].SubscribeOn(key, artifacts, queue);
        }

        public void BeginUpdate()
        {

        }

        public void EndUpdate()
        {

        }

        #region Events
        private void Artifacts_Tagged(object sender, TagArtifactArgs e)
        {
            metaGraph.Invalidate(null, queue, e.Artifact);
        }

        private void Artifacts_Moved(object sender, MoveArtifactArgs e)
        {
            metaGraph.Invalidate(null, queue, e.Parent.Old);
            metaGraph.Invalidate(null, queue, e.Parent.New);
        }

        private void Artifacts_Added(object sender, IArtifact e)
        {
            metaGraph.Invalidate(null, queue, e);
        }

        private void Findings_ClassificationChanged(object sender, FindingClassificationChangedArgs e)
        {
            metaGraph.Invalidate(null, queue, e.AffectedFinding.AffectedFiles.ToArray());
        }

        private void Findings_Moved(object sender, FindingMovedArgs e)
        {
            metaGraph.Invalidate(null, queue, e.Parent.Old.AffectedFiles.ToArray());
            metaGraph.Invalidate(null, queue, e.AffectedFinding.AffectedFiles.ToArray());
        }

        private void Findings_Removed(object sender, IFinding e)
        {
            if(e.Parent != null)
                metaGraph.Invalidate(null, queue, e.Parent.AffectedFiles.ToArray());
        }

        private void Findings_Added(object sender, IFinding e)
        {
            metaGraph.Invalidate(null, queue, e.AffectedFiles.ToArray());
        }
        #endregion
    }
}
