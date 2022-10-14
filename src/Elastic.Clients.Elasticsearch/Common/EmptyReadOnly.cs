// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Elastic.Clients.Elasticsearch;

internal static class EmptyReadOnly<TElement>
{
	public static readonly IReadOnlyCollection<TElement> Collection = new ReadOnlyCollection<TElement>(new TElement[0]);
	public static readonly IReadOnlyList<TElement> List = new List<TElement>();
}

internal static class EmptyReadOnly<TKey, TValue>
{
	public static readonly IReadOnlyDictionary<TKey, TValue> Dictionary = new ReadOnlyDictionary<TKey, TValue>(new Dictionary<TKey, TValue>(0));
}
