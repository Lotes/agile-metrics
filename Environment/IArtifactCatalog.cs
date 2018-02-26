using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E03_Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.E01_Artifacts
{
    public interface IArtifactCatalog
    {
        IArtifact Add(string name, ArtifactType artifactType, IArtifact parent = null);
        void Move(IEnumerable<IArtifact> artifact, IArtifact newParent);
        void Tag(IEnumerable<IArtifact> artifacts, ITag tag, SetterMode mode);
        IEnumerable<IArtifact> Roots { get; }
        event EventHandler<IArtifact> Added;
        event EventHandler<MoveArtifactArgs> Moved;
        event EventHandler<TagArtifactArgs> Tagged;
    }
}
