using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class SearchProfile
	{
		[JsonProperty("rewrite_time")]
		public long RewriteTime { get; internal set; }

		[JsonProperty("query")]
		public IReadOnlyCollection<QueryProfile> Query { get; internal set; } =
			EmptyReadOnly<QueryProfile>.Collection;

		[JsonProperty("collector")]
		public IReadOnlyCollection<Collector> Collector { get; internal set; } =
			EmptyReadOnly<Collector>.Collection;
	}
}
