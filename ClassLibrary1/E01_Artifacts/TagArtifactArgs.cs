using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E03_Tags;
using ClassLibrary1.N00_Config.Facade;

namespace Metrics.E01_Artifacts
{
    public class TagArtifactArgs
    {
        public readonly IArtifact Artifact;
        public readonly ITag Tag;
        public readonly OldNewPair<bool> Set;

        public TagArtifactArgs(IArtifact artifact, ITag tag, OldNewPair<bool> set)
        {
            Artifact = artifact;
            Tag = tag;
            Set = set;
        }
    }
}