using System.Runtime.Serialization;
using Utf8Json;

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
