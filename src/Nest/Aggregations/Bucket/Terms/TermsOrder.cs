using System;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(SortOrderFormatter<TermsOrder>))]
	public class TermsOrder : ISortOrder
	{
		public static TermsOrder CountAscending => new TermsOrder { Key = "_count", Order = SortOrder.Ascending };
		public static TermsOrder CountDescending => new TermsOrder { Key = "_count", Order = SortOrder.Descending };

		public string Key { get; set; }

		public static TermsOrder KeyAscending => new TermsOrder { Key = "_key", Order = SortOrder.Ascending };
		public static TermsOrder KeyDescending => new TermsOrder { Key = "_key", Order = SortOrder.Descending };
		public SortOrder Order { get; set; }

		[Obsolete("Deprecated in Elasticsearch 6.0. Use KeyAscending")]
		public static TermsOrder TermAscending => new TermsOrder { Key = "_key", Order = SortOrder.Ascending };

		[Obsolete("Deprecated in Elasticsearch 6.0. Use KeyDescending")]
		public static TermsOrder TermDescending => new TermsOrder { Key = "_key", Order = SortOrder.Descending };
	}
}
