using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Facade;
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
        TagRelevanceFlags this[IArtifact artifact] { get; }
        event EventHandler<IEnumerable<TagRelevanceChangedArgs>> TagRelevanceChanged;
    }

    public class TagRelevanceChangedArgs: EventArgs
    {
        public TagRelevanceChangedArgs(IArtifact artifact, TagRelevanceFlags old, TagRelevanceFlags @new)
        {
            Artifact = artifact;
            TagRelevance = new OldNewPair<TagRelevanceFlags>(old, @new);
        }

        public IArtifact Artifact { get; private set; }
        public OldNewPair<TagRelevanceFlags> TagRelevance { get; private set; }
    }
}
