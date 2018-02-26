using ClassLibrary1.N00_Config.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Environment
{
    public interface IFindingCatalog
    {
        event EventHandler<IFinding> Added;
        event EventHandler<IFinding> Removed;
        event EventHandler<FindingMovedArgs> Moved;
        event EventHandler<FindingClassificationChangedArgs> ClassificationChanged;
    }

    public class FindingClassificationChangedArgs
    {
        public readonly IFinding AffectedFinding;
        public readonly OldNewPair<Classification> Clasification;
    }

    public enum Classification
    {
        New,
        Ok,
        Bad
    }

    public class FindingMovedArgs
    {
        public readonly IFinding AffectedFinding;
        public readonly OldNewPair<IFinding> Parent;

        public FindingMovedArgs(IFinding affectedFinding, OldNewPair<IFinding> parent)
        {
            AffectedFinding = affectedFinding;
            Parent = parent;
        }
    }
}
