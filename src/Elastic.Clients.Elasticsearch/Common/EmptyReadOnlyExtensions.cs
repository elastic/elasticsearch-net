// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Elastic.Clients.Elasticsearch;

internal static class EmptyReadOnlyExtensions
{
	public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable) =>
		enumerable == null ? EmptyReadOnly<T>.Collection : new ReadOnlyCollection<T>(enumerable.ToList());

	public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IList<T> enumerable) =>
		enumerable == null || enumerable.Count == 0 ? EmptyReadOnly<T>.Collection : new ReadOnlyCollection<T>(enumerable);
}
