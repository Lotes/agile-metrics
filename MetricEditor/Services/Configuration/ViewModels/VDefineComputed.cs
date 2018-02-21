using ClassLibrary1.N00_Config.Meta;
using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public class VDefineComputed : VDataDefinition
    {
        public VDefineComputed(XDefineComputed xDef)
            : base(xDef)
        {
            Inputs = new ObservableCollection<VInput>(xDef.Inputs.Select(xI => new VInput(xI)));
            SourceCodeSections = new ObservableCollection<VSourceCodeSection>(xDef.SourceCodeSections.Select(xS => new VSourceCodeSection(xS)));
        }

        public ObservableCollection<VInput> Inputs { get; private set; }
        public ObservableCollection<VSourceCodeSection> SourceCodeSections { get; private set; }
    }
}
