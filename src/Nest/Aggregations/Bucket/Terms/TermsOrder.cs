using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(KeyValueJsonConverter<TermsOrder, SortOrder>))]
	public class TermsOrder
	{
		public string Key { get; set; }
		public SortOrder Order { get; set; }

		public static TermsOrder CountAscending => new TermsOrder { Key = "_count", Order = SortOrder.Ascending };
		public static TermsOrder CountDescending => new TermsOrder { Key = "_count", Order = SortOrder.Descending };
		[Obsolete("Deprecated in Elasticsearch 6.0. Use KeyAscending")]
		public static TermsOrder TermAscending => new TermsOrder { Key = "_key", Order = SortOrder.Ascending };
		[Obsolete("Deprecated in Elasticsearch 6.0. Use KeyDescending")]
		public static TermsOrder TermDescending => new TermsOrder { Key = "_key", Order = SortOrder.Descending };
		public static TermsOrder KeyAscending => new TermsOrder { Key = "_key", Order = SortOrder.Ascending };
		public static TermsOrder KeyDescending => new TermsOrder { Key = "_key", Order = SortOrder.Descending };
	}
}
