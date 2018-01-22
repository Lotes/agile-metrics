using Domo.Core;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo.Ui
{
    public class TreeViewModel : ViewModelBase
    {
        private IArtifact artifact;
        private bool isExpanded = true;

        public TreeViewModel(IArtifact artifact)
        {
            this.artifact = artifact;
            this.Children = artifact.Children.Select(c => new TreeViewModel(c)).ToArray();
        }

        public string Name { get { return artifact.Name; } }
        public bool IsExpanded { get { return isExpanded; } set { isExpanded = value; RaisePropertyChanged(() => IsExpanded); } }
        public TreeViewModel[] Children { get; private set; }
    }
}
