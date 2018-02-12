using System;
using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Instance;

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
        IRawNode AddOrGetRawInstanceNode(IMetaRawNode node, IArtifact artifact);

        IEnumerable<IMetaDependency> MetaDependencies { get; }
        IEnumerable<IMetaDependency> GetInputsOf(IMetaNode node);
        IEnumerable<IMetaDependency> GetOutputsOf(IMetaNode node);

        IReadOnlyDictionary<ITagExpression, IGraph> Instances { get; }
        IGraph CreateInstanceFor(ITagExpression tagExpression);
    }
}