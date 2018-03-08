using System;
using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using System.Linq;

namespace ClassLibrary1.N00_Config.Meta.Impl
{
    public class MetaRawNode : IMetaRawNode
    {
        private IMetaGraph metaGraph;
        public MetaRawNode(IMetaGraph metaGraph, TypedKey key, ImporterType source, IEnumerable<ArtifactType> targetArtifactTypes)
        {
            this.metaGraph = metaGraph;
            Key = key;
            Source = source;
            TargetArtifactTypes = targetArtifactTypes;
        }

        public TypedKey Key { get; private set; }
        public ImporterType Source { get; private set; }
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; private set; }

        public IReadOnlyList<IMetaDependency> Outputs
        {
            get
            {
                return metaGraph.GetOutputsOf(this).ToList();
            }
        }

        public override string ToString()
        {
            return "{Raw} "+Key.ToString();
        }
    }
}