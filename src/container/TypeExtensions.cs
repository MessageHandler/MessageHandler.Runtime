using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageHandler.Runtime
{
    internal static class TypeExtensions
    {
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            if (type.BaseType == null) return type.GetInterfaces();

            return Enumerable.Repeat(type.BaseType, 1)
                .Concat(type.GetInterfaces())
                .Concat(type.GetInterfaces().SelectMany(GetBaseTypes))
                .Concat(type.BaseType.GetBaseTypes());
        }
    }
}