using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MetricEditor.Services.Configuration
{
    public class MetaModel<TModel>
    {
        public string ElementName { get; }
        public IReadOnlyDictionary<TypedKey, UsageAttribute> Children { get; }
        public TModel Read(XElement element)
        {

        }
    }
}
