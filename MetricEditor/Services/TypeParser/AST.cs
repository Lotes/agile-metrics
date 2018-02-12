using System.Collections.Generic;
using System.Linq;

namespace MetricEditor.Services.TypeParser
{
    public abstract class CSharpType
    {
        public abstract TResult Aggregate<TResult>(ICSharpTypeAggregator<TResult> aggregator);
    }

    public class ArrayType : CSharpType
    {
        public ArrayType(CSharpType elementType)
        {
            ElementType = elementType;
        }
        public CSharpType ElementType { get; }
        public override TResult Aggregate<TResult>(ICSharpTypeAggregator<TResult> aggregator)
        {
            return aggregator.MakeArray(ElementType.Aggregate(aggregator));
        }
    }

    public class GenericType : CSharpType
    {
        public GenericType(Namespace @namespace, string name, IEnumerable<CSharpType> typeArguments)
        {
            Namespace = @namespace;
            Name = name;
            TypeArguments = typeArguments.ToArray();
        }
        public Namespace Namespace { get; }
        public string Name { get; }
        public IEnumerable<CSharpType> TypeArguments { get; }
        public override TResult Aggregate<TResult>(ICSharpTypeAggregator<TResult> aggregator)
        {
            var type = aggregator.MakeType(Namespace, Name);
            if (TypeArguments.Any())
                type = aggregator.MakeGeneric(type, TypeArguments.Select(ta => ta.Aggregate(aggregator)));
            return type;
        }
    }

    public class Namespace
    {
        public Namespace(string name, Namespace parent = null)
        {
            Name = name;
            Parent = parent;
        }
        public string Name { get; }
        public Namespace Parent { get; }
        public string FullName { get { return (Parent != null ? Parent.FullName + "." : "") + Name; } }
    }
}
