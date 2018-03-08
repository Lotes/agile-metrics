using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Instance
{
    [Flags]
    public enum TagRelevanceFlags
    {
        None = 0,
        SelfTagged = 1,
        ChildrenTagged = 2,
        All = SelfTagged | ChildrenTagged
    }
}
