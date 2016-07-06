using System;

namespace Benchmarking
{
    internal static class TypeExtensions
    {
		internal static bool IsGenericType(this Type type) => type.IsGenericType;

		internal static bool IsGenericTypeDefinition(this Type type) => type.IsGenericTypeDefinition;

        internal static bool IsValueType(this Type type) => type.IsValueType;

        internal static bool IsEnumType(this Type type) => type.IsEnum;
    }
}
