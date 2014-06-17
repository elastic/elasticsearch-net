using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions
{
    public static class EnumerableExtensions
    {
		public static void ThrowIfEmpty<T>(this IEnumerable<T> @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			if (!@object.Any())
				throw new ArgumentException("Argument can not be an empty collection", parameterName);
		}

		public static IEnumerable<T> NullIfEmpty<T>(this IEnumerable<T> list)
		{
			return list.HasAny() ? list : null;
		}

		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> xs)
		{
			if (xs == null)
			{
				return new T[0];
			}

			return xs;
		}

		public static bool HasAny<T>(this IEnumerable<T> list, Func<T, bool> predicate)
		{
			return list != null && list.Any(predicate);
		}

		public static bool HasAny<T>(this IEnumerable<T> list)
		{
			return list != null && list.Any();
		}
    }
}
