using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Instance;
using ClassLibrary1.N00_Config.Meta;

namespace Metrics.Meta
{
    public class ExecutionQueue: IExecutionQueue
    {
        private Queue<Tuple<IGraph, IMetaSelfNode, IArtifact>> queue = new Queue<Tuple<IGraph, IMetaSelfNode, IArtifact>>();

        public void Enqueue(IGraph graph, IMetaSelfNode node, IArtifact artifact)
        {
            queue.Enqueue(new Tuple<IGraph, IMetaSelfNode, IArtifact>(graph, node, artifact));
        }

        public void Execute()
        {
            var local = queue;
            queue = new Queue<Tuple<IGraph, IMetaSelfNode, IArtifact>>();
            while(queue.Any())
            {
                var item = queue.Dequeue();
                var graph = item.Item1;
                var node = item.Item2;
                var artifact = item.Item3;
                graph.Storage.SetValue(node, artifact, node.Compute(graph, artifact));
            }
        }
    }
}
