using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
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
