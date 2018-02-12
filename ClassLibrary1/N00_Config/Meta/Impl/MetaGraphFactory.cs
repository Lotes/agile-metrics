using ClassLibrary1.N00_Config.Facade.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Instance.Impl;

namespace ClassLibrary1.N00_Config.Meta.Impl
{
    public class MetaGraphFactory : IMetaGraphFactory, IGraphFactory
    {
        public IMetaDependency CreateMetaDependency(IMetaNode source, IMetaNode target, string name, DependencyLocality locality)
        {
            return new MetaDependency(source, target, name, locality);
        }

        public IMetaGraph CreateMetaGraph(IValueStorageFactory storageFactory)
        {
            return new MetaGraph(storageFactory, this, this);
        }

        public IMetaRawNode CreateMetaRawNode(TypedKey key, ImporterType source, ArtifactType[] targetArtifactTypes)
        {
            return new MetaRawNode(key, source, targetArtifactTypes);
        }

        public IMetaSelfNode CreateMetaSelfNode(IMetaGraph metaGraph, TypedKey key, ArtifactType[] targetArtifactTypes, IEnumerable<InputConfiguration> inputs, string sourceCode)
        {
            return new MetaSelfNode(metaGraph, key, targetArtifactTypes, inputs, sourceCode);
        }

        public IGraph CreateGraph(IMetaGraph metaGraph, ITagExpression tagExpression, IValueStorageFactory storageFactory)
        {
            return new Graph(this, metaGraph, tagExpression, storageFactory);
        }

        public IDependency CreateDependency(IMetaDependency metaDependency, INode source, INode target)
        {
            return new Dependency(metaDependency, source, target);
        }

        public ISelfNode CreateSelfNode(IGraph graph, IMetaNode metaNode, IArtifact artifact)
        {
            return new SelfNode(metaNode, artifact, graph);
        }

        public IRawNode CreateRawNode(IMetaGraph graph, IMetaNode metaNode, IArtifact artifact)
        {
            return new RawNode(metaNode, graph, artifact);
        }
    }
}
