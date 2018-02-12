using System.Collections.Generic;
using System.Linq;

namespace MetricEditor.Services.TypeParser
{
    public class CSharpTypeVisitor: CSharpTypeBaseVisitor<CSharpType>
    {
        private readonly NamespaceVisitor nsVisitor = new NamespaceVisitor();
        private readonly ExtensionVisitor extVisitor;

        public CSharpTypeVisitor()
        {
            extVisitor = new ExtensionVisitor(this);
        }

        public override CSharpType VisitQualifiedType(CSharpTypeParser.QualifiedTypeContext context)
        {
            return Visit(context.tpe);
        }

        public override CSharpType VisitCompileUnit(CSharpTypeParser.CompileUnitContext context)
        {
            return Visit(context.tp);
        }

        public override CSharpType VisitType(CSharpTypeParser.TypeContext context)
        {
            var @namespace = context.@namespace != null ? nsVisitor.Visit(context.@namespace) : null;
            var name = context.name.Text;
            var extension = context.extension != null ? extVisitor.Visit(context.extension) : null;
            if(extension == null)
                extension = new Extension(0, Enumerable.Empty<CSharpType>());
            CSharpType result = new GenericType(@namespace, name, extension.TypeArguments);
            var dims = extension.ArrayDimensions;
            while (dims > 0)
            {
                result = new ArrayType(result);
                dims--;
            }
            return result;
        }
    }

    public class CSharpTypesVisitor : CSharpTypeBaseVisitor<IEnumerable<CSharpType>>
    {
        private CSharpTypeVisitor parent;

        public CSharpTypesVisitor(CSharpTypeVisitor parent)
        {
            this.parent = parent;
        }

        public override IEnumerable<CSharpType> VisitTypesMultiple(CSharpTypeParser.TypesMultipleContext context)
        {
            return new [] { parent.Visit(context.tp) }.Concat(Visit(context.tps));
        }

        public override IEnumerable<CSharpType> VisitTypesSingle(CSharpTypeParser.TypesSingleContext context)
        {
            return new[] {parent.Visit(context.tpe)};
        }
    }

    public class Extension
    {
        public Extension(int arrayDims, IEnumerable<CSharpType> arguments)
        {
            ArrayDimensions = arrayDims;
            TypeArguments = arguments.ToArray();
        }
        public int ArrayDimensions { get; }
        public IEnumerable<CSharpType> TypeArguments { get; }
    }

    public class ExtensionVisitor : CSharpTypeBaseVisitor<Extension>
    {
        private readonly CSharpTypesVisitor typesVisitor;
        public ExtensionVisitor(CSharpTypeVisitor parent)
        {
            typesVisitor = new CSharpTypesVisitor(parent);
        }

        public override Extension VisitArraySingle(CSharpTypeParser.ArraySingleContext context)
        {
            return new Extension(1, Enumerable.Empty<CSharpType>());
        }

        public override Extension VisitArrayMultiple(CSharpTypeParser.ArrayMultipleContext context)
        {
            return new Extension(1 + Visit(context.arrs).ArrayDimensions, Enumerable.Empty<CSharpType>());
        }

        public override Extension VisitArgs(CSharpTypeParser.ArgsContext context)
        {
            var types = context.tps != null ? typesVisitor.Visit(context.tps) : Enumerable.Empty<CSharpType>();
            var arrayExtension = context.arrs != null ? Visit(context.arrs) : new Extension(0, Enumerable.Empty<CSharpType>());
            return new Extension(arrayExtension.ArrayDimensions, types);
        }
    }

    public class NamespaceVisitor : CSharpTypeBaseVisitor<Namespace>
    {
        public override Namespace VisitNamespaceMultiple(CSharpTypeParser.NamespaceMultipleContext context)
        {
            return new Namespace(context.name1.Text, Visit(context.next));
        }

        public override Namespace VisitNamespaceSingle(CSharpTypeParser.NamespaceSingleContext context)
        {
            return new Namespace(context.name2.Text);
        }
    }
}
