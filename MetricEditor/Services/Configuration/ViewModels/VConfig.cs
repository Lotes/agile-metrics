using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using GalaSoft.MvvmLight;
using MetricEditor.Services.Configuration.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
    public interface IError
    {
        string Message { get; }
        Uri Location { get; }
    }
    public interface IErrorProducer
    {
        ObservableCollection<IError> Errors { get; }
    }

    public abstract class ErrorTrackingViewModel : ViewModelBase, IErrorProducer
    {
        public ObservableCollection<IError> Errors { get; private set; }
    }

    public class VConfig: ErrorTrackingViewModel 
    {
        public VConfig(XConfig xConfig)
        {
            Definitions = new ObservableCollection<VDefinition>(xConfig.Definitions.Select<XAbstractDefinition, VDefinition>(xD => 
            {
                if (xD is XApplyTemplate)
                    return new VApplyTemplate(xD as XApplyTemplate);
                if (xD is XDeclareRaw)
                    return new VDeclareRaw(xD as XDeclareRaw);
                if (xD is XDefineComputed)
                    return new VDefineComputed(xD as XDefineComputed);
                if (xD is XDefineTemplate)
                    return new VDefineTemplate(xD as XDefineTemplate);
                throw new InvalidOperationException("Unknown definition!");
            }));
            Documentation = new VTemplateString<string>(xConfig.Documentation);
        }
        public ObservableCollection<VDefinition> Definitions { get; private set; }
        public VTemplateString<string> Documentation { get; private set; }
    }

    public abstract class VDefinition : ErrorTrackingViewModel
    {
        public VDefinition(XAbstractDefinition xDef)
        {
            Id = new VTemplateString<VId>(xDef.Id);
            Documentation = new VTemplateString<string>(xDef.Documentation);
        }
        public VTemplateString<string> Documentation { get; private set; }
        public VTemplateString<VId> Id { get; private set; }
    }

    public abstract class VDataDefinition : VDefinition
    {
        public VDataDefinition(XAbstractDataDefinition xDataDef)
            : base(xDataDef)
        {
            Type = new VTemplateString<System.Type>(xDataDef.Type);
            TargetArtifactTypes = new ObservableCollection<VArtifactTypeSelector>(xDataDef.TargetArtifactTypes.Select<XArtifactTypeSelector, VArtifactTypeSelector>(xD =>
            {
                if (xD is XATSelectorAncestors)
                    return new VATSelectorAncestors(xD as XATSelectorAncestors);
                if (xD is XATSelectorCopy)
                    return new VATSelectorCopy(xD as XATSelectorCopy);
                if (xD is XATSelectorDirect)
                    return new VATSelectorDirect(xD as XATSelectorDirect);
                else
                    throw new InvalidOperationException("Unknown selector!");
            }));
        }
        public VTemplateString<Type> Type { get; private set; }
        public ObservableCollection<VArtifactTypeSelector> TargetArtifactTypes { get; set; }
    }

    public abstract class VArtifactTypeSelector: ErrorTrackingViewModel
    {
        public VArtifactTypeSelector(XArtifactTypeSelector xSelector)
        {
            Documentation = new VTemplateString<string>(xSelector.Documentation);
        }

        public VTemplateString<string> Documentation { get; private set; }
    }

    public class VATSelectorCopy: VArtifactTypeSelector
    {
        public VATSelectorCopy(XATSelectorCopy xSelector)
            : base(xSelector)
        {
            CopyOf = new VTemplateString<VId>(xSelector.Value);
        }
        public VTemplateString<VId> CopyOf { get; private set; }
    }

    public class VATSelectorAncestors : VArtifactTypeSelector
    {
        public VATSelectorAncestors(XATSelectorAncestors xSelector)
            : base(xSelector)
        {
            AncestorsOf = new VTemplateString<VId>(xSelector.Value);
        }
        public VTemplateString<VId> AncestorsOf { get; private set; }
    }

    public class VATSelectorDirect : VArtifactTypeSelector
    {
        public VATSelectorDirect(XATSelectorDirect xSelector)
            : base(xSelector)
        {
            Direct = new VTemplateString<VId>(xSelector.Value);
        }
        public VTemplateString<VId> Direct { get; private set; }
    }

    public class VId
    {
        public string Name { get; private set; }
    }

    public class VTemplateString<TDataType> : ErrorTrackingViewModel
    {
        private string raw;
        private TDataType value;
        public VTemplateString(string raw)
        {
            this.Raw = raw;
            value = default(TDataType);
        }
        public void Evaluate()
        {

        }
        public string Raw { get { return raw; } set { raw = value; RaisePropertyChanged(() => Raw); } }
        public TDataType Value { get { return value; } }
    }

    public class VDeclareRaw: VDataDefinition
    {
        public VDeclareRaw(XDeclareRaw xDef)
            : base(xDef)
        {
            Source = new VTemplateString<ImporterType>(xDef.Source);
        }
        public VTemplateString<ImporterType> Source { get; }
    }

    public class VDefineComputed : VDataDefinition
    {
        public VDefineComputed(XDefineComputed xDef)
            : base(xDef)
        {
            xDef.Inputs;xDef.SourceCodeSections
        }

    }

    public class VApplyTemplate : VDefinition
    {
        public VApplyTemplate(XApplyTemplate xDef) : base(xDef)
        {
            xDef.Bindings;
        }
    }

    public class VDefineTemplate : VDefinition
    {
        public VDefineTemplate(XDefineTemplate xDef) : base(xDef)
        {
            xDef.Definitions;
            xDef.Parameters;
            xDef.Variables;
        }
    }
}
