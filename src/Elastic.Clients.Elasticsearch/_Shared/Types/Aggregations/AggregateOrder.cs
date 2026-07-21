// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public static class AggregateOrder
{
	public static KeyValuePair<Field, SortOrder> KeyDescending => new(Field.KeyField, SortOrder.Desc);
	public static KeyValuePair<Field, SortOrder> KeyAscending => new(Field.KeyField, SortOrder.Asc);
	public static KeyValuePair<Field, SortOrder> CountDescending => new(Field.CountField, SortOrder.Desc);
	public static KeyValuePair<Field, SortOrder> CountAscending => new(Field.CountField, SortOrder.Asc);
}
