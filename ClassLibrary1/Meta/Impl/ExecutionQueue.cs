using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Meta
{
    public class ExecutionQueue: IExecutionQueue
    {
        private Queue<Action> queue = new Queue<Action>();

        public void Enqueue(Action action)
        {

        }

        public void ExecuteValidation()
        {

        }
    }
}
