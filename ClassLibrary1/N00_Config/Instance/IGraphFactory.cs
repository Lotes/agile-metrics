using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Instance
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(IMetaGraph metaComputationGraph, ITagExpression tagExpression, IValueStorageFactory storageFactory);
        ISelfNode CreateSelfNode(IGraph graph, IMetaNode metaNode, IArtifact artifact);
        IRawNode CreateRawNode(IMetaGraph graph, IMetaNode metaNode, IArtifact artifact);
        IDependency CreateDependency(IMetaDependency metaDependency, INode source, INode target);
    }
}