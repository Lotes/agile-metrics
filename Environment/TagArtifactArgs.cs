using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E03_Tags;
using ClassLibrary1.N00_Config.Facade;
using System.Collections.Generic;

namespace Metrics.E01_Artifacts
{
    public class TagArtifactArgs
    {
        public readonly IArtifact Artifact;
        public readonly ITag Tag;

        public TagArtifactArgs(IArtifact artifact, ITag tag)
        {
            Artifact = artifact;
            Tag = tag;
        }
    }
}