using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Antlr4.Runtime;

namespace MetricEditor.Services.TypeParser
{
    public static class TypeUtils
    {
       
        public static void Initialize()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                AddAssembly(assembly);
        }

        private static readonly Dictionary<string, Type> typesByFullName = new Dictionary<string, Type>();
        private static void AddAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var name = type.FullName;
                if (!name.Contains("<") && !name.Contains("+") && !name.Contains("^") && !name.Contains("$"))
                    AddType(type);
            }
        }

        private static void AddType(Type type)
        {
            typesByFullName[type.FullName] = type; //ignores duplicates
        }

        public static bool TryResolve(string fullNameWithoutAssemblyPart, out Type result)
        {
            try
            {
                result = Resolve(fullNameWithoutAssemblyPart);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static Type Resolve(string fullNameWithoutAssemblyPart)
        {
            var inputStream = new AntlrInputStream(fullNameWithoutAssemblyPart);
            var speakLexer = new CSharpTypeLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);
            var parser = new CSharpTypeParser(commonTokenStream);
            var parseContext = parser.compileUnit();
            parser.AddErrorListener(new ThrowExceptionListener());
            var visitor = new CSharpTypeVisitor();
            return visitor.Visit(parseContext).ToType();
        }

        private class TypeAggregator : ICSharpTypeAggregator<Type>
        {
            public static readonly TypeAggregator Instance = new TypeAggregator();
            public Type MakeType(Namespace ns, string name)
            {
                if (ns != null)
                    name = ns.FullName + "." + name;
                return typesByFullName[name];
            }

            public Type MakeGeneric(Type type, IEnumerable<Type> typeArguments)
            {
                return type.MakeGenericType(typeArguments.ToArray());
            }

            public Type MakeArray(Type elementType)
            {
                return elementType.MakeArrayType();
            }
        }

        private static Type ToType(this CSharpType @this)
        {
            return @this.Aggregate(TypeAggregator.Instance);
        }

        public static IEnumerable<Type> RegisteredTypes { get { return typesByFullName.Values; } }
    }
}
