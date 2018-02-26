using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Facade;

namespace Metrics.E01_Artifacts
{
    public class MoveArtifactArgs
    {
        public readonly IArtifact MovedArtifact;
        public readonly OldNewPair<IArtifact> Parent;

        public MoveArtifactArgs(IArtifact movedArtifact, OldNewPair<IArtifact> parent)
        {
            MovedArtifact = movedArtifact;
            Parent = parent;
        }
    }
}