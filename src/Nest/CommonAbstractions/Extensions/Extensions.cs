// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Nest
{
	internal static class Extensions
	{
		private static readonly ConcurrentDictionary<string, object> EnumCache = new ConcurrentDictionary<string, object>();

		internal static bool NotWritable(this QueryContainer q) => q == null || !q.IsWritable;

		internal static bool NotWritable(this IEnumerable<QueryContainer> qs) => qs == null || qs.All(q => q.NotWritable());

		internal static TReturn InvokeOrDefault<T, TReturn>(this Func<T, TReturn> func, T @default)
			where T : class, TReturn where TReturn : class =>
			func?.Invoke(@default) ?? @default;

		internal static TReturn InvokeOrDefault<T1, T2, TReturn>(this Func<T1, T2, TReturn> func, T1 @default, T2 param2)
			where T1 : class, TReturn where TReturn : class =>
			func?.Invoke(@default, param2) ?? @default;

		internal static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property) =>
			items.GroupBy(property).Select(x => x.First());

		internal static string ToEnumValue<T>(this T enumValue) where T : struct
		{
			var enumType = typeof(T);
			var name = Enum.GetName(enumType, enumValue);
			var enumMemberAttribute = enumType.GetField(name).GetCustomAttribute<EnumMemberAttribute>();

			if (enumMemberAttribute != null)
				return enumMemberAttribute.Value;

			var alternativeEnumMemberAttribute = enumType.GetField(name).GetCustomAttribute<AlternativeEnumMemberAttribute>();

			return alternativeEnumMemberAttribute != null
				? alternativeEnumMemberAttribute.Value
				: enumValue.ToString();
		}

		internal static T? ToEnum<T>(this string str, StringComparison comparison = StringComparison.OrdinalIgnoreCase) where T : struct
		{
			if (str == null) return null;

			var enumType = typeof(T);
			var key = $"{enumType.Name}.{str}";
			if (EnumCache.TryGetValue(key, out var value))
				return (T)value;

			foreach (var name in Enum.GetNames(enumType))
			{
				if (name.Equals(str, comparison))
				{
					var v = (T)Enum.Parse(enumType, name, true);
					EnumCache.TryAdd(key, v);
					return v;
				}

				var enumFieldInfo = enumType.GetField(name);
				var enumMemberAttribute = enumFieldInfo.GetCustomAttribute<EnumMemberAttribute>();
				if (enumMemberAttribute?.Value.Equals(str, comparison) ?? false)
				{
					var v = (T)Enum.Parse(enumType, name);
					EnumCache.TryAdd(key, v);
					return v;
				}

				var alternativeEnumMemberAttribute = enumFieldInfo.GetCustomAttribute<AlternativeEnumMemberAttribute>();
				if (alternativeEnumMemberAttribute?.Value.Equals(str, comparison) ?? false)
				{
					var v = (T)Enum.Parse(enumType, name);
					EnumCache.TryAdd(key, v);
					return v;
				}
			}

			return null;
		}

		internal static string Utf8String(this ref ArraySegment<byte> segment) =>
			StringEncoding.UTF8.GetString(segment.Array, segment.Offset, segment.Count);

		internal static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes, 0, bytes.Length);

		internal static byte[] Utf8Bytes(this string s) => s.IsNullOrEmpty() ? null : Encoding.UTF8.GetBytes(s);

		internal static bool IsNullOrEmpty(this IndexName value) => value == null || value.GetHashCode() == 0;

		internal static bool IsNullable(this Type type) =>
			type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

		internal static void ThrowIfNullOrEmpty(this string @object, string parameterName, string when = null)
		{
			@object.ThrowIfNull(parameterName, when);
			if (string.IsNullOrWhiteSpace(@object))
				throw new ArgumentException("Argument can't be null or empty" + (when.IsNullOrEmpty() ? "" : " when " + when), parameterName);
		}

		// ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
		internal static void ThrowIfEmpty<T>(this IEnumerable<T> @object, string parameterName)
		{
			if (@object == null) throw new ArgumentNullException(parameterName);
			if (!@object.Any())
				throw new ArgumentException("Argument can not be an empty collection", parameterName);
		}

		internal static List<T> AsInstanceOrToListOrDefault<T>(this IEnumerable<T> list) => list as List<T> ?? list?.ToList() ?? new List<T>();

		internal static List<T> AsInstanceOrToListOrNull<T>(this IEnumerable<T> list) => list as List<T> ?? list?.ToList();

		internal static List<T> EagerConcat<T>(this IEnumerable<T> list, IEnumerable<T> other)
		{
			var first = list.AsInstanceOrToListOrDefault();
			if (other == null) return first;

			var second = other.AsInstanceOrToListOrDefault();
			var newList = new List<T>(first.Count + second.Count);
			newList.AddRange(first);
			newList.AddRange(second);
			return newList;
		}

		internal static IEnumerable<T> AddIfNotNull<T>(this IEnumerable<T> list, T other)
		{
			if (other == null) return list;

			var l = list.AsInstanceOrToListOrDefault();
			l.Add(other);
			return l;
		}

		internal static bool HasAny<T>(this IEnumerable<T> list, Func<T, bool> predicate) => list != null && list.Any(predicate);

		internal static bool HasAny<T>(this IEnumerable<T> list) => list != null && list.Any();

		internal static bool IsEmpty<T>(this IEnumerable<T> list)
		{
			if (list == null) return true;

			var enumerable = list as T[] ?? list.ToArray();
			return !enumerable.Any() || enumerable.All(t => t == null);
		}

		internal static void ThrowIfNull<T>(this T value, string name, string message = null)
		{
			if (value == null && message.IsNullOrEmpty()) throw new ArgumentNullException(name);
			else if (value == null) throw new ArgumentNullException(name, "Argument can not be null when " + message);
		}

		internal static bool IsNullOrEmpty(this string value) => string.IsNullOrWhiteSpace(value);

		internal static bool IsNullOrEmptyCommaSeparatedList(this string value, out string[] split)
		{
			split = null;
			if (string.IsNullOrWhiteSpace(value)) return true;

			split = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Where(t => !t.IsNullOrEmpty())
				.Select(t => t.Trim())
				.ToArray();
			return split.Length == 0;
		}

		internal static List<T> ToListOrNullIfEmpty<T>(this IEnumerable<T> enumerable)
		{
			var list = enumerable.AsInstanceOrToListOrNull();
			return list != null && list.Count > 0 ? list : null;
		}

		internal static void AddIfNotNull<T>(this IList<T> list, T item) where T : class
		{
			if (item == null) return;

			list.Add(item);
		}

		internal static void AddRangeIfNotNull<T>(this List<T> list, IEnumerable<T> item) where T : class
		{
			if (item == null) return;

			list.AddRange(item.Where(x => x != null));
		}

		internal static Dictionary<TKey, TValue> NullIfNoKeys<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			var i = dictionary?.Count;
			return i.GetValueOrDefault(0) > 0 ? dictionary : null;
		}

		internal static async Task ForEachAsync<TSource, TResult>(
			this IEnumerable<TSource> lazyList,
			Func<TSource, long, Task<TResult>> taskSelector,
			Action<TSource, TResult> resultProcessor,
			Action<Exception> done,
			int maxDegreeOfParallelism,
			SemaphoreSlim additionalRateLimiter = null
		)
		{
			var semaphore = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);
			long page = 0;

			try
			{
				var tasks = new List<Task>(maxDegreeOfParallelism);
				foreach (var item in lazyList)
				{
					tasks.Add(ProcessAsync(item, taskSelector, resultProcessor, semaphore, additionalRateLimiter, page++));
					if (tasks.Count < maxDegreeOfParallelism)
						continue;

					var task = await Task.WhenAny(tasks).ConfigureAwait(false);
					if (task.Exception != null
						&& (task.IsFaulted && task.Exception.Flatten().InnerExceptions.First() is Exception e))
					{
						ExceptionDispatchInfo.Capture(e).Throw();
						return;
					}
					tasks.Remove(task);
				}

				await Task.WhenAll(tasks).ConfigureAwait(false);
				done(null);
			}
			catch (Exception e)
			{
				done(e);
				throw;
			}
		}

		private static async Task ProcessAsync<TSource, TResult>(
			TSource item,
			Func<TSource, long, Task<TResult>> taskSelector,
			Action<TSource, TResult> resultProcessor,
			SemaphoreSlim localRateLimiter,
			SemaphoreSlim additionalRateLimiter,
			long page
		)
		{
			if (localRateLimiter != null) await localRateLimiter.WaitAsync().ConfigureAwait(false);
			if (additionalRateLimiter != null) await additionalRateLimiter.WaitAsync().ConfigureAwait(false);
			try
			{
				var result = await taskSelector(item, page).ConfigureAwait(false);
				resultProcessor(item, result);
			}
			finally
			{
				localRateLimiter?.Release();
				additionalRateLimiter?.Release();
			}
		}

		internal static bool NullOrEquals<T>(this T o, T other)
		{
			if (o == null && other == null) return true;
			if (o == null || other == null) return false;

			return o.Equals(other);
		}
	}
}
