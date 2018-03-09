using Metrics.E01_Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E03_Tags;
using ClassLibrary1.E01_Artifacts.Impl;
using System.Collections;

namespace Environment.Impl
{
    public class ArtifactCatalog : IArtifactCatalog
    {
        private List<IArtifact> roots;
        private List<IArtifact> all;

        public IEnumerable<IArtifact> Roots { get { return roots; } }

        public event EventHandler<IArtifact> Added;
        public event EventHandler<MoveArtifactArgs> Moved;
        public event EventHandler<TagArtifactArgs> Tagged;

        public ArtifactCatalog()
        {
            roots = new List<IArtifact>();
            all = new List<IArtifact>();
        }

        public IArtifact Add(string name, ArtifactType artifactType, IArtifact parent = null)
        {
            var result = new Artifact(name, artifactType, parent);
            if (parent == null)
                roots.Add(result);
            all.Add(result);
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
            var downs = new HashSet<IArtifact>();
            foreach (var artifact in artifacts)
                ArtifactCatalogExtensions.ForEachPostfix(artifact, a => downs.Add(a));

            var ups = new List<IArtifact>();
            foreach (var artifact in artifacts)
            {
                var node = artifact.Parent;
                while(node != null)
                {
                    ups.Add(node);
                    node = node.Parent;
                }
            }

            if (mode == SetterMode.Toggle)
            {
                if (artifacts.Any(a => a.Tags.Contains(tag)))
                    mode = SetterMode.Unset;
                else
                    mode = SetterMode.Set; 
            }
            foreach (var artifact in downs)
            {
                var tags = ((Artifact)artifact).MutableTags;
                if (mode == SetterMode.Set && !tags.Contains(tag))
                {
                    tags.Add(tag);
                    Tagged?.Invoke(this, new TagArtifactArgs(artifact, tag));
                }
                else if (mode == SetterMode.Unset && tags.Contains(tag))
                {
                    tags.Remove(tag);
                    Tagged?.Invoke(this, new TagArtifactArgs(artifact, tag));
                }
            }
            foreach(var artifact in ups)
            {
                var tags = ((Artifact)artifact).MutableTags;
                if (artifact.Children.All(c => c.Tags.Contains(tag)))
                    tags.Add(tag);
                else
                    tags.Remove(tag);
                Tagged?.Invoke(this, new TagArtifactArgs(artifact, tag));
            }
        }

        public IEnumerator<IArtifact> GetEnumerator()
        {
            return all.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
