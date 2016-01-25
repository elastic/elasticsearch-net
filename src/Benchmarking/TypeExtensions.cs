using System;

#if DOTNETCORE
using System.Collections.Generic;
using System.Reflection;
#endif

namespace Benchmarking
{
    internal static class TypeExtensions
    {
#if DOTNETCORE
		internal static bool IsAssignableFrom(this Type t, Type other) => t.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());

		internal static Type GetGenericTypeDefinition(this Type t) => t.GetTypeInfo().GetGenericTypeDefinition();
#endif

        internal static bool IsGenericType(this Type type)
        {
#if DOTNETCORE
			return type.GetTypeInfo().IsGenericType;
#else
            return type.IsGenericType;
#endif
        }

        internal static bool IsGenericTypeDefinition(this Type type)
        {
#if DOTNETCORE
			return type.GetTypeInfo().IsGenericTypeDefinition;
#else
            return type.IsGenericTypeDefinition;
#endif
        }

        internal static bool IsValueType(this Type type)
        {
#if DOTNETCORE
			return type.GetTypeInfo().IsValueType;
#else
            return type.IsValueType;
#endif
        }

        internal static bool IsEnumType(this Type type)
        {
#if DOTNETCORE
			return type.GetTypeInfo().IsEnum;
#else
            return type.IsEnum;
#endif
        }

#if DOTNETCORE
		internal static IEnumerable<Type> GetInterfaces(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces;
		}

        internal static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeParameters;
        } 
#endif
    }
}