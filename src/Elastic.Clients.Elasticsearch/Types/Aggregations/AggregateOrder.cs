// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public static class AggregateOrder
{
	public static IEnumerable<KeyValuePair<Field, SortOrder>> KeyDescending => new[] { new KeyValuePair<Field, SortOrder>(Field.KeyField, SortOrder.Desc) };
	public static IEnumerable<KeyValuePair<Field, SortOrder>> KeyAscending => new[] { new KeyValuePair<Field, SortOrder>(Field.KeyField, SortOrder.Asc) };
	public static IEnumerable<KeyValuePair<Field, SortOrder>> CountDescending => new[] { new KeyValuePair<Field, SortOrder>(Field.CountField, SortOrder.Desc) };
	public static IEnumerable<KeyValuePair<Field, SortOrder>> CountAscending => new[] { new KeyValuePair<Field, SortOrder>(Field.CountField, SortOrder.Asc) };
}
