using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Core
{
    public interface IArtifact
    {
        ArtifactType ArtifactType { get; }
        string Name { get; }
        IArtifact Parent { get; }
        IEnumerable<IArtifact> Children { get; }
    }
}
