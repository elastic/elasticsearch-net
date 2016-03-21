using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class QueryProfile
	{
		[JsonProperty("query_type")]
		public string QueryType { get; internal set; }

		[JsonProperty("lucene")]
		public string Lucene { get; internal set; }

		[JsonProperty("time")]
		public Time Time { get; internal set; }

		[JsonProperty("breakdown")]
		public QueryBreakdown Breakdown { get; internal set; }

		[JsonProperty("children")]
		public IEnumerable<QueryProfile> Children { get; internal set; }
	}
}
