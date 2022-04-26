// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class HistogramOrder
{
	public static HistogramOrder KeyDescending => new() { Key = SortOrder.Desc };

	public static HistogramOrder KeyAscending => new() { Key = SortOrder.Asc };

	public static HistogramOrder CountDescending => new() { Count = SortOrder.Desc };

	public static HistogramOrder CountAscending => new() { Count = SortOrder.Asc };
}
