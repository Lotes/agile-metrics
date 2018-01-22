using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Core
{
    public static class ArtifactExtensions
    {
        public static string GetPath(this IArtifact @this)
        {
            return (@this.Parent != null ? @this.Parent.GetPath() + "/" : "") + @this.Name;
        }
    }
}
