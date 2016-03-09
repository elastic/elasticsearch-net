using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal static class Extensions
	{
		internal static TReturn InvokeOrDefault<T, TReturn>(this Func<T, TReturn> func, T @default)
			where T : class, TReturn where TReturn : class =>
			func?.Invoke(@default) ?? @default;

		internal static TReturn InvokeOrDefault<T1, T2, TReturn>(this Func<T1, T2, TReturn> func, T1 @default, T2 param2)
			where T1 : class, TReturn where TReturn : class =>
			func?.Invoke(@default, param2) ?? @default;

		internal static string GetStringValue(this Enum enumValue)
		{
			var knownEnum = KnownEnums.Resolve(enumValue);
			if (knownEnum != KnownEnums.UnknownEnum) return knownEnum;

			var type = enumValue.GetType();
			var info = type.GetField(enumValue.ToString());
			var da = info.GetCustomAttribute<EnumMemberAttribute>();

			return da != null ? da.Value : Enum.GetName(enumValue.GetType(), enumValue);
		}

		internal static readonly JsonConverter dateConverter = new IsoDateTimeConverter { Culture = CultureInfo.InvariantCulture };
		internal static readonly JsonSerializer serializer = new JsonSerializer();
		internal static string ToJsonNetString(this DateTime date)
		{
			using (var writer = new JTokenWriter())
			{
				dateConverter.WriteJson(writer, date, serializer);
				return writer.Token.ToString();
			}
		}

		internal static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
		{
			return items.GroupBy(property).Select(x => x.First());
		}

		internal static ConcurrentDictionary<string, object> _enumCache = new ConcurrentDictionary<string, object>();
		internal static T? ToEnum<T>(this string str, StringComparison comparison = StringComparison.OrdinalIgnoreCase) where T : struct
		{
			var enumType = typeof(T);
			var key = $"{enumType.Name}.{str}";
			object value;
			if (_enumCache.TryGetValue(key, out value))
				return (T)value;

			foreach (var name in Enum.GetNames(enumType))
			{
				if (name.Equals(str, comparison))
				{
					var v = (T)Enum.Parse(enumType, name, true);
					_enumCache.TryAdd(key, v);
					return v;
				}

				var enumFieldInfo = enumType.GetField(name);
				var enumMemberAttribute = enumFieldInfo.GetCustomAttribute<EnumMemberAttribute>();
				if (enumMemberAttribute?.Value == str)
				{
					var v = (T) Enum.Parse(enumType, name);
					_enumCache.TryAdd(key, v);
					return v;
				}

				var alternativeEnumMemberAttribute = enumFieldInfo.GetCustomAttribute<AlternativeEnumMemberAttribute>();
				if (alternativeEnumMemberAttribute?.Value == str)
				{
					var v = (T) Enum.Parse(enumType, name);
					_enumCache.TryAdd(key, v);
					return v;
				}
			}

			return null;
		}

#if !DOTNETCORE
		internal static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes);
#else
		internal static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes, 0, bytes.Length);
#endif

		internal static byte[] Utf8Bytes(this string s)
		{
			return s.IsNullOrEmpty() ? null : Encoding.UTF8.GetBytes(s);
		}

		internal static bool IsNullOrEmpty(this TypeName value)
		{
			return value == null || value.GetHashCode() == 0;
		}

		internal static void ThrowIfNullOrEmpty(this string @object, string parameterName, string when = null)
		{
			@object.ThrowIfNull(parameterName, when);
			if (string.IsNullOrWhiteSpace(@object))
				throw new ArgumentException("Argument can't be null or empty" + (when.IsNullOrEmpty() ? "" : " when " + when), parameterName);
		}

		internal static void ThrowIfEmpty<T>(this IEnumerable<T> @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			if (!@object.Any())
				throw new ArgumentException("Argument can not be an empty collection", parameterName);
		}

		internal static IList<T> EagerConcat<T>(this IEnumerable<T> list, IEnumerable<T> other)
		{
			list = list.HasAny() ? list : Enumerable.Empty<T>();
			var l = new List<T>(list);
			if (other.HasAny()) l.AddRange(other);
			return l;
		}

		internal static bool HasAny<T>(this IEnumerable<T> list, Func<T, bool> predicate)
		{
			return list != null && list.Any(predicate);
		}

		internal static bool HasAny<T>(this IEnumerable<T> list)
		{
			return list != null && list.Any();
		}

		internal static void ThrowIfNull<T>(this T value, string name, string message = null)
		{
			if (value == null && message.IsNullOrEmpty()) throw new ArgumentNullException(name);
			else if (value == null) throw new ArgumentNullException(name, "Argument can not be null when " + message);
		}

		internal static string F(this string format, params object[] args)
		{
			var c = CultureInfo.InvariantCulture;
			format.ThrowIfNull(nameof(format));
			return string.Format(c, format, args);
		}

		internal static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		internal static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> handler)
		{
			foreach (T item in enumerable) handler(item);
		}

		internal static List<T> ToListOrNullIfEmpty<T>(this IEnumerable<T> enumerable) =>
			enumerable.HasAny() ? enumerable.ToList() : null;

		internal static void AddIfNotNull<T>(this IList<T> list, T item) where T : class
		{
			if (item == null) return;
			list.Add(item);
		}

		internal static Dictionary<TKey, TValue> NullIfNoKeys<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			var i = dictionary?.Count;
			return i.GetValueOrDefault(0) > 0 ? dictionary : null;
		}

		internal static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> xs) => xs ?? new T[0];
	}
}
