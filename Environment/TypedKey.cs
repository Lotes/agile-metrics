using ClassLibrary1.E00_Addons;
using System;
using System.Collections.Generic;

namespace ClassLibrary1.E02_TypedKeys
{
    public class TypedKey
    {
        private static Dictionary<string, TypedKey> instances = new Dictionary<string, TypedKey>();
        protected TypedKey(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }
        public Type Type { get; private set; }

        public static TypedKey<T> Create<T>(string name)
        {
            name = name.ToUpper();
            return (TypedKey<T>)instances.GetOrLazyInsert(name, () => new TypedKey<T>(name));
        }
        public static TypedKey Create(string name, Type type)
        {
            name = name.ToUpper();
            return instances.GetOrLazyInsert(name, () => new TypedKey(name, type));
        }
    }

    public class TypedKey<TType> : TypedKey
    {
        public TypedKey(string name) : base(name, typeof(TType))
        {
        }
    }
}