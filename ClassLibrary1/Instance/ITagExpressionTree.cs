using ClassLibrary1.E01_Artifacts;
using Metrics.E01_Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Instance
{
    public interface ITagExpressionTree
    {
        ITagExpression Expression { get; }
        void Attach(IArtifactCatalog catalog);
        void Detach();
        ITETreeNode Root { get; }
        IReadOnlyDictionary<IArtifact, ITETreeNode> Nodes { get; }
        event EventHandler<IEnumerable<ITETreeNode>> TagRelevanceChanged;
    }

    public interface ITETreeNode
    {
        ITagExpressionTree Tree { get; }
        ITETreeNode Parent { get; }
        IEnumerable<ITETreeNode> Children { get; }
        IArtifact Artifact { get; }
        TagRelevanceFlags TagRelevance { get; }
    }
}
