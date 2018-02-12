using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.E01_Artifacts.Impl
{
    public class TagExpression: ITagExpression
    {
        private readonly Func<IArtifact, bool> evaluate;

        public TagExpression(Func<IArtifact, bool> evaluate = null)
        {
            this.evaluate = evaluate ?? (a => true);
        }
        public bool Evaluate(IArtifact artifact)
        {
            return evaluate(artifact);
        }
    }
}
