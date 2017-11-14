using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(KeyValueJsonConverter<TermsOrder, SortOrder>))]
	public class TermsOrder
	{
		[JsonProperty("key")]
		public string Key { get; set; }
		[JsonProperty("value")]
		public SortOrder Order { get; set; }

		public static TermsOrder CountAscending => new TermsOrder { Key = "_count", Order = SortOrder.Ascending };
		public static TermsOrder CountDescending => new TermsOrder { Key = "_count", Order = SortOrder.Descending };

		//here for backwards compatibility reasons, elasticsearch deprecrated _term as sort order in favor of _key
		public static TermsOrder TermAscending => new TermsOrder { Key = "_key", Order = SortOrder.Ascending };
		public static TermsOrder TermDescending => new TermsOrder { Key = "_key", Order = SortOrder.Descending };

		public static TermsOrder KeyAscending => new TermsOrder { Key = "_key", Order = SortOrder.Ascending };
		public static TermsOrder KeyDescending => new TermsOrder { Key = "_key", Order = SortOrder.Descending };
	}
}
