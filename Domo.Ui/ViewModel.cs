using Domo.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Ui
{
    public class ViewModel: ViewModelBase
    {
        private IArtifactLoader loader;

        public ViewModel(IArtifactLoader loader)
        {
            this.loader = loader;
        }

        public TreeViewModel[] Roots { get; private set; }

        public void Load(string folderPath)
        {
            Roots = new[] { new TreeViewModel(loader.Load(folderPath)) };
            RaisePropertyChanged(() => Roots);
        }
    }
}
