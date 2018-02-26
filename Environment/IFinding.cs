using ClassLibrary1.E01_Artifacts;
using System.Collections.Generic;

namespace Environment
{
    public interface IFinding
    {
        IFinding Parent { get; }
        IEnumerable<IFinding> Children { get; }
        FindingType FindingType { get; }
        IEnumerable<IArtifact> AffectedFiles { get; }
    }
}