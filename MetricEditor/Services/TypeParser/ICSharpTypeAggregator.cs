using System.Collections.Generic;

namespace MetricEditor.Services.TypeParser
{
    public interface ICSharpTypeAggregator<TResult>
    {
        TResult MakeType(Namespace ns, string name);
        TResult MakeGeneric(TResult type, IEnumerable<TResult> typeArguments);
        TResult MakeArray(TResult elementType);
    }
}