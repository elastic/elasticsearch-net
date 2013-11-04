using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Globalization;
using Nest.Resolvers;

namespace Nest
{
	internal static class Extensions
	{
		internal static string ToCamelCase(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return s;

			if (!char.IsUpper(s[0]))
				return s;

			string camelCase = char.ToLower(s[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
			if (s.Length > 1)
				camelCase += s.Substring(1);

			return camelCase;
		}

		internal static bool JsonEquals(this string json, string otherjson)
		{
			var nJson = JObject.Parse(json).ToString();
			var nOtherJson = JObject.Parse(otherjson).ToString();
			return nJson == nOtherJson;
		}

		internal static void ThrowIfNullOrEmpty(this string @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			if (string.IsNullOrWhiteSpace(@object))
					throw new ArgumentException("Argument can't be null or empty", parameterName);
		}
		internal static void ThrowIfEmpty<T>(this IEnumerable<T> @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			if (!@object.Any())
				throw new ArgumentException("Argument can not be an empty collection", parameterName);
		}
		internal static bool HasAny<T>(this IEnumerable<T> list)
		{
			return list != null && list.Any();
		}

		internal static IEnumerable<T> NullIfEmpty<T>(this IEnumerable<T> list)
		{
			return list.HasAny() ? list : null;
		}
		internal static void ThrowIfNull<T>(this T value, string name)
		{
			if (value == null)
				throw new ArgumentNullException(name);
		}
		internal static string F(this string format, params object[] args)
		{
			format.ThrowIfNull("format");
			return string.Format(format, args);
		}
		internal static string EscapedFormat(this string format, params object[] args)
		{
			format.ThrowIfNull("format");
			var arguments = new List<object>();
			foreach (var a in args)
			{
				var s = a as string;
				arguments.Add(s != null ? Uri.EscapeDataString(s) : a);
			}
			return string.Format(format, arguments.ToArray());
		}
		internal static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}
		internal static bool IsNullOrEmpty(this TypeNameMarker value)
		{
			return value == null || value.GetHashCode() == 0;
		}
		
		internal static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
		{
			int idx = 0;
			foreach (T item in enumerable)
				handler(item, idx++);
		}
        internal static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> xs)
        {
            if (xs == null)
            {
                return new T[0];
            }

            return xs;
        }


	}

	
	
}
