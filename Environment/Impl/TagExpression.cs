using ClassLibrary1.E03_Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.E01_Artifacts.Impl
{
    public class TagExpression: ITagExpression
    {
        private readonly Func<IEnumerable<ITag>, bool> evaluate;

        public TagExpression(Func<IEnumerable<ITag>, bool> evaluate = null)
        {
            this.evaluate = evaluate ?? (a => true);
        }
        public bool Evaluate(IEnumerable<ITag> artifact)
        {
            return evaluate(artifact);
        }
    }
}
