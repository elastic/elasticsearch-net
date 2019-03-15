using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardStatistics
	{
		[JsonProperty("failed")]
		public int Failed { get; internal set; }

		[JsonProperty("failures")]
		public IReadOnlyCollection<ShardFailure> Failures { get; internal set; } = EmptyReadOnly<ShardFailure>.Collection;

		[JsonProperty("successful")]
		public int Successful { get; internal set; }

		[JsonProperty("total")]
		public int Total { get; internal set; }
	}

	[JsonObject]
	public class ClusterStatistics
	{
		[JsonProperty("skipped")]
		public int Skipped { get; internal set; }

		[JsonProperty("successful")]
		public int Successful { get; internal set; }

		[JsonProperty("total")]
		public int Total { get; internal set; }
	}
}
