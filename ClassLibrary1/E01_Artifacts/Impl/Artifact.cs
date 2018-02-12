using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClassLibrary1.E03_Tags;

namespace ClassLibrary1.E01_Artifacts.Impl
{
    public class Artifact : IArtifact
    {
        private LinkedList<IArtifact> children = new LinkedList<IArtifact>();
        public Artifact(string name, ArtifactType artifactType, Artifact parent = null)
        {
            Name = name;
            ArtifactType = artifactType;
            Tags = new ObservableCollection<ITag>();
            parent?.children.AddLast(this);
            Parent = parent;
        }

        public string Name { get; }
        public ArtifactType ArtifactType { get; }
        public IEnumerable<IArtifact> Children { get { return children; } }
        public ObservableCollection<ITag> Tags { get; }
        public IArtifact Parent { get; }
    }
}
