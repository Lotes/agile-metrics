using Metrics.E01_Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E03_Tags;
using ClassLibrary1.E01_Artifacts.Impl;

namespace Environment.Impl
{
    public class ArtifactCatalog : IArtifactCatalog
    {
        private List<IArtifact> roots;

        public IEnumerable<IArtifact> Roots { get { return roots; } }

        public event EventHandler<IArtifact> Added;
        public event EventHandler<MoveArtifactArgs> Moved;
        public event EventHandler<TagArtifactArgs> Tagged;

        public ArtifactCatalog()
        {
            roots = new List<IArtifact>();
        }

        public IArtifact Add(string name, ArtifactType artifactType, IArtifact parent = null)
        {
            var result = new Artifact(name, artifactType, parent);
            if (parent == null)
                roots.Add(result);
            Added?.Invoke(this, result);
            return result;
        }

        public void Move(IEnumerable<IArtifact> artifacts, IArtifact newParent)
        {
            foreach(var artifact in artifacts)
            {
                var old = artifact.Parent;
                if(old != newParent)
                {
                    artifact.Parent = newParent;
                    if (old == null)
                        roots.Remove(artifact);
                    if (newParent == null)
                        roots.Add(artifact);
                    Moved?.Invoke(this, new MoveArtifactArgs(artifact, new ClassLibrary1.N00_Config.Facade.OldNewPair<IArtifact>(old, newParent)));
                }
            }
        }

        public void Tag(IEnumerable<IArtifact> artifacts, ITag tag, SetterMode mode)
        {
            foreach (var artifact in artifacts)
            {
                Tagged?.Invoke(this, new TagArtifactArgs(artifact, tag, new ClassLibrary1.N00_Config.Facade.OldNewPair<bool>(true, true))); // TODO
            }
        }
    }
}
