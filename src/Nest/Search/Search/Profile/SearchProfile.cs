using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class SearchProfile
	{
		[JsonProperty("collector")]
		public IReadOnlyCollection<Collector> Collector { get; internal set; } =
			EmptyReadOnly<Collector>.Collection;

		[JsonProperty("query")]
		public IReadOnlyCollection<QueryProfile> Query { get; internal set; } =
			EmptyReadOnly<QueryProfile>.Collection;

		[JsonProperty("rewrite_time")]
		public long RewriteTime { get; internal set; }
	}
}
