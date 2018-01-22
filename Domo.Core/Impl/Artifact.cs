using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Core.Impl
{
    public class Artifact : IArtifact
    {
        private Dictionary<string, IArtifact> children = new Dictionary<string, IArtifact>();

        public Artifact(string name, ArtifactType type, Artifact parent = null)
        {
            if (parent != null && parent.children.ContainsKey(name))
                throw new ArgumentException("This name is already assigned: "+name, nameof(name));
            this.Name = name;
            this.ArtifactType = type;
            this.Parent = parent;
            if (parent != null)
                parent.children.Add(name, this);
        }

        public ArtifactType ArtifactType { get; private set; }
        public IEnumerable<IArtifact> Children { get { return children.Values; } }
        public string Name { get; private set; }
        public IArtifact Parent { get; private set; }
    }
}
