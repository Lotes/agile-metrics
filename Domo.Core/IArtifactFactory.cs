using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Core
{
    public interface IArtifactFactory
    {
        IArtifact CreateRoot();
        IArtifact CreateDirectory(string name, IArtifact parent);
        IArtifact CreateFile(string name, IArtifact parent);
    }
}
