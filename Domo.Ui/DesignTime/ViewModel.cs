using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domo.Core;
using Domo.Core.Impl;

namespace Domo.Ui.DesignTime
{
    public class ViewModel : Domo.Ui.ViewModel
    {
        public ViewModel() : base(new FileSystemLoader(new ArtifactFactory()))
        {
            Load(@"..");
        }
    }
}
