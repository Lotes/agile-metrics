using System;
using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Instance;
using Metrics.Meta;

namespace ClassLibrary1.N00_Config.Meta
{
    public interface IMetaGraph
    {
        IValueStorage Storage { get; }

        IMetaRawNode CreateRawNode(TypedKey key, ImporterType source, ArtifactType[] targetArtifactTypes);

        IMetaSelfNode CreateSelfNode(TypedKey key, ArtifactType[] targetArtifactTypes,
            IEnumerable<InputConfiguration> inputs, string sourceCode);
        void RemoveNode(IMetaNode node);
        IEnumerable<IMetaNode> MetaNodes { get; }
        IMetaNode GetNode(TypedKey metriKey);

        IEnumerable<IMetaDependency> MetaDependencies { get; }
        IEnumerable<IMetaDependency> GetInputsOf(IMetaNode node);
        IEnumerable<IMetaDependency> GetOutputsOf(IMetaNode node);

        IReadOnlyDictionary<ITagExpression, IGraph> Instances { get; }
        IGraph CreateInstanceFor(ITagExpression tagExpression);
        /// <summary>
        /// If node is NULL, the entire graph is affected
        /// </summary>
        /// <param name="node">can be null!</param>
        /// <param name="artifacts"></param>
        void Invalidate(IMetaNode node, IExecutionQueue queue, params IArtifact[] artifacts);
    }
}