using ClassLibrary1.N00_Config.Facade.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;

namespace ClassLibrary1.N00_Config.Meta.Impl
{
    public class MetaGraphFactory : IMetaGraphFactory
    {
        public IMetaDependency CreateMetaDependency(IMetaNode source, IMetaNode target, string name, DependencyLocality locality)
        {
            return new MetaDependency(source, target, name, locality);
        }

        public IMetaGraph CreateMetaGraph(IValueStorageFactory storageFactory)
        {
            return new MetaGraph(storageFactory, this);
        }

        public IMetaRawNode CreateMetaRawNode(TypedKey key, ImporterType source, ArtifactType[] targetArtifactTypes)
        {
            return new MetaRawNode(key, source, targetArtifactTypes);
        }

        public IMetaSelfNode CreateMetaSelfNode(IMetaGraph metaGraph, TypedKey key, ArtifactType[] targetArtifactTypes, IEnumerable<InputConfiguration> inputs, string sourceCode)
        {
            return new MetaSelfNode(metaGraph, key, targetArtifactTypes, inputs, sourceCode);
        }
    }
}
