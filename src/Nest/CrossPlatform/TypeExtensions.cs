using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class DotNetCoreTypeExtensions
	{
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
	}
}
