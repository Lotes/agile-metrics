using System.Collections;
using ClassLibrary1.E03_Tags;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ClassLibrary1.E01_Artifacts
{
    public interface IArtifact
    {
        string Name { get; }
        ArtifactType ArtifactType { get; }   
        IReadOnlyList<ITag> Tags { get; }
        ICollection<IArtifact> Children { get; }
        IArtifact Parent { get; set; }
    }
}