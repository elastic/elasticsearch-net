// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class AggregateOrder
{
	public static AggregateOrder KeyDescending => new(new System.Collections.Generic.Dictionary<Field, SortOrder>
	{
		{ "_key", SortOrder.Desc }
	});

	public static AggregateOrder KeyAscending => new(new System.Collections.Generic.Dictionary<Field, SortOrder>
	{
		{ "_key", SortOrder.Asc }
	});

	public static AggregateOrder CountDescending => new(new System.Collections.Generic.Dictionary<Field, SortOrder>
	{
		{ "_count", SortOrder.Desc }
	});

	public static AggregateOrder CountAscending => new(new System.Collections.Generic.Dictionary<Field, SortOrder>
	{
		{ "_count", SortOrder.Asc }
	});
}
