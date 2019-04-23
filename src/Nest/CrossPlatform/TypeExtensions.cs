using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Nest
{
	internal static class DotNetCoreTypeExtensions
	{
		internal static bool IsGenericDictionary(this Type type) => type.GetInterfaces()
			.Any(t =>
				t.IsGenericType && (
					t.GetGenericTypeDefinition() == typeof(IDictionary<,>) ||
					t.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>)));

		internal static bool TryGetGenericDictionaryArguments(this Type type, out Type[] genericArguments)
		{
			var genericDictionary = type.GetInterfaces()
				.FirstOrDefault(t =>
					t.IsGenericType && (
						t.GetGenericTypeDefinition() == typeof(IDictionary<,>) ||
						t.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>)));

			if (genericDictionary == null)
			{
				genericArguments = new Type[0];
				return false;
			}

			genericArguments = genericDictionary.GetGenericArguments();
			return true;
		}

internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type t)
	where TAttribute : Attribute
{
	var attributes = Attribute.GetCustomAttributes(t, typeof(TAttribute), true);
	return attributes.Cast<TAttribute>();
}
	}
}
