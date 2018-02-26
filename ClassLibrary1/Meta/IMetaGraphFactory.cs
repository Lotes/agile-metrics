using System;
using System.Collections.Generic;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;

namespace ClassLibrary1.N00_Config.Meta
{
    public interface IMetaGraphFactory
    {
        IMetaGraph CreateMetaGraph(IValueStorageFactory storageFactory);
        IMetaRawNode CreateMetaRawNode(IMetaGraph metaGraph, TypedKey key, ImporterType source, ArtifactType[] targetArtifactTypes);
        IMetaSelfNode CreateMetaSelfNode(IMetaGraph metaGraph, TypedKey key, ArtifactType[] targetArtifactTypes, IEnumerable<InputConfiguration> inputs, string sourceCode);
        IMetaDependency CreateMetaDependency(IMetaNode source, IMetaNode target, string name, DependencyLocality locality);
    }
}