using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(KeyValueJsonConverter<TermsOrder, SortOrder>))]
	public class TermsOrder
	{
		public string Key { get; set; }
		public SortOrder Order { get; set; }

		public static TermsOrder CountAscending => new TermsOrder { Key = "_count", Order = SortOrder.Ascending };
		public static TermsOrder CountDescending => new TermsOrder { Key = "_count", Order = SortOrder.Descending };

		public static TermsOrder TermAscending => new TermsOrder { Key = "_term", Order = SortOrder.Ascending };
		public static TermsOrder TermDescending => new TermsOrder { Key = "_term", Order = SortOrder.Descending };
	}
}
