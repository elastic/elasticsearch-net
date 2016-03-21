using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class SearchProfile
	{
		[JsonProperty("rewrite_time")]
		public long RewriteTime { get; internal set; }

		[JsonProperty("query")]
		public IEnumerable<QueryProfile> Query { get; internal set; }

		[JsonProperty("collector")]
		public IEnumerable<Collector> Collector { get; internal set; }
	}
}
