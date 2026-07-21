// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch;

internal static class Extensions
{
	// ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
	internal static void ThrowIfEmpty<T>(this IEnumerable<T> @object, string parameterName)
	{
		if (@object == null)
			throw new ArgumentNullException(parameterName);
		if (!@object.Any())
			throw new ArgumentException("Argument can not be an empty collection", parameterName);
	}

	// ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
	internal static T[] NotEmpty<T>(this IEnumerable<T> @object, string parameterName)
	{
		if (!@object.HasAny(out var enumerated))
			throw new ArgumentException("Argument can not be an empty collection", parameterName);
		return enumerated;
	}

	internal static bool HasAny<T>(this IEnumerable<T> list, out T[] enumerated)
	{
		enumerated = list == null ? null : list as T[] ?? list.ToArray();
		return enumerated.HasAny();
	}

	internal static List<T> AsInstanceOrToListOrDefault<T>(this IEnumerable<T> list) =>
		list as List<T> ?? list?.ToList() ?? new List<T>();

	internal static List<T> AsInstanceOrToListOrNull<T>(this IEnumerable<T> list) =>
		list as List<T> ?? list?.ToList();

	internal static List<T> EagerConcat<T>(this IEnumerable<T> list, IEnumerable<T> other)
	{
		var first = list.AsInstanceOrToListOrDefault();
		if (other == null)
			return first;

		var second = other.AsInstanceOrToListOrDefault();
		var newList = new List<T>(first.Count + second.Count);
		newList.AddRange(first);
		newList.AddRange(second);
		return newList;
	}

	internal static IEnumerable<T> AddIfNotNull<T>(this IEnumerable<T> list, T other)
	{
		if (other == null)
			return list;

		var l = list.AsInstanceOrToListOrDefault();
		l.Add(other);
		return l;
	}

	internal static bool HasAny<T>(this IEnumerable<T> list) => list != null && list.Any();

	internal static bool IsNullOrEmpty<T>(this IEnumerable<T>? list)
	{
		if (list is null)
			return true;

		var enumerable = list as T[] ?? list.ToArray();

		return (enumerable.Length == 0) || enumerable.All(x => x is null);
	}

	internal static void ThrowIfNull<T>(this T value, string name, string message = null)
	{
		if (value == null && message.IsNullOrEmpty())
			throw new ArgumentNullException(name);
		else if (value == null)
			throw new ArgumentNullException(name, "Argument can not be null when " + message);
	}

	internal static bool IsNullOrEmpty(this string? value) => string.IsNullOrWhiteSpace(value);

	internal static bool IsNullOrEmptyCommaSeparatedList(this string? value, [NotNullWhen(false)] out string[]? split)
	{
		split = null;
		if (string.IsNullOrWhiteSpace(value))
			return true;

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
		if (item == null)
			return;

		list.Add(item);
	}

	internal static void AddRangeIfNotNull<T>(this List<T> list, IEnumerable<T> item) where T : class
	{
		if (item == null)
			return;

		list.AddRange(item.Where(x => x != null));
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
				tasks.Add(ProcessAsync(item, taskSelector, resultProcessor, semaphore, additionalRateLimiter,
					page++));
				if (tasks.Count < maxDegreeOfParallelism)
					continue;

				var task = await Task.WhenAny(tasks).ConfigureAwait(false);
				if (task.Exception != null && task.IsFaulted && task.Exception.Flatten().InnerExceptions.First() is
					{ } e)
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
		if (localRateLimiter != null)
			await localRateLimiter.WaitAsync().ConfigureAwait(false);
		if (additionalRateLimiter != null)
			await additionalRateLimiter.WaitAsync().ConfigureAwait(false);
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
}
