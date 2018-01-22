using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Core
{
    public interface IArtifactLoader
    {
        IArtifact Load(string url);
    }
}
