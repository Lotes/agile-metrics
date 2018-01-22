using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Core.Impl
{
    public class ArtifactFactory : IArtifactFactory
    {
        public IArtifact CreateDirectory(string name, IArtifact parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            return new Artifact(name, ArtifactType.Directory, (Artifact)parent);
        }

        public IArtifact CreateFile(string name, IArtifact parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            return new Artifact(name, ArtifactType.File, (Artifact)parent);
        }

        public IArtifact CreateRoot()
        {
            return new Artifact("", ArtifactType.Root);
        }
    }
}
