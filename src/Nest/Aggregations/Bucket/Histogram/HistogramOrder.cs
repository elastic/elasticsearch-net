// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	public interface ISortOrder
	{
		string Key { get; set; }

		SortOrder Order { get; set; }
	}

	[JsonFormatter(typeof(SortOrderFormatter<HistogramOrder>))]
	public class HistogramOrder : ISortOrder
	{
		public static HistogramOrder CountAscending => new HistogramOrder { Key = "_count", Order = SortOrder.Ascending };
		public static HistogramOrder CountDescending => new HistogramOrder { Key = "_count", Order = SortOrder.Descending };
		public string Key { get; set; }

		public static HistogramOrder KeyAscending => new HistogramOrder { Key = "_key", Order = SortOrder.Ascending };
		public static HistogramOrder KeyDescending => new HistogramOrder { Key = "_key", Order = SortOrder.Descending };
		public SortOrder Order { get; set; }
	}
}
