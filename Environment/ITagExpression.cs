using ClassLibrary1.E03_Tags;
using System.Collections.Generic;

namespace ClassLibrary1.E01_Artifacts
{
    public interface ITagExpression { bool Evaluate(IEnumerable<ITag> tags); }
}