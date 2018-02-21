using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration.ViewModels
{
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
}
