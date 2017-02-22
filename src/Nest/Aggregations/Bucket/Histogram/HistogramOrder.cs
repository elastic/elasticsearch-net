using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(KeyValueJsonConverter<HistogramOrder, SortOrder>))]
	public class HistogramOrder
	{
		public string Key { get; set; }
		public SortOrder Order { get; set; }

		public static HistogramOrder CountAscending => new HistogramOrder { Key = "_count", Order = SortOrder.Ascending };
		public static HistogramOrder CountDescending => new HistogramOrder { Key = "_count", Order = SortOrder.Descending };

		public static HistogramOrder KeyAscending => new HistogramOrder { Key = "_key", Order = SortOrder.Ascending };
		public static HistogramOrder KeyDescending => new HistogramOrder { Key = "_key", Order = SortOrder.Descending };

	}
}