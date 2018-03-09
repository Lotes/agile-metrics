using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E03_Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.E01_Artifacts
{
    public interface IArtifactCatalog: IEnumerable<IArtifact>
    {
        IArtifact Add(string name, ArtifactType artifactType, IArtifact parent = null);
        void Move(IEnumerable<IArtifact> artifact, IArtifact newParent);
        void Tag(IEnumerable<IArtifact> artifacts, ITag tag, SetterMode mode);
        IEnumerable<IArtifact> Roots { get; }
        event EventHandler<IArtifact> Added;
        event EventHandler<MoveArtifactArgs> Moved;
        event EventHandler<TagArtifactArgs> Tagged;
    }

    public enum TraversionType
    {
        Prefix,
        Postfix
    }

    public static class ArtifactCatalogExtensions
    {
        private static Dictionary<TraversionType, Action<IArtifact, Action<IArtifact>>> lookup
            = new Dictionary<TraversionType, Action<IArtifact, Action<IArtifact>>>()
            {
                { TraversionType.Prefix, ForEachPrefix },
                { TraversionType.Postfix, ForEachPostfix }
            };
        public static void ForEach(this IArtifactCatalog @this, TraversionType type, Action<IArtifact> action)
        {
            var algorithm = lookup[type];
            foreach(var root in @this.Roots)
                algorithm(root, action);
        }
        public static void ForEachPrefix(IArtifact @this, Action<IArtifact> action)
        {
            action(@this);
            foreach (var child in @this.Children)
                ForEachPrefix(child, action);
        }
        public static void ForEachPostfix(IArtifact @this, Action<IArtifact> action)
        {
            foreach (var child in @this.Children)
                ForEachPrefix(child, action);
            action(@this);
        }
    }
}
