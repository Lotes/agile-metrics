using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Meta
{
    public interface IExecutionQueue
    {
        void Enqueue(IGraph graph, IMetaSelfNode node, IArtifact artifact);
        void Execute();
    }
}
