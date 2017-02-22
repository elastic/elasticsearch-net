using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class ShardsMetaData
	{
		[JsonProperty]
		public int Total { get; internal set; }

		[JsonProperty]
		public int Successful { get; internal set; }

		[JsonProperty]
		public int Failed { get; internal set; }

		[JsonProperty("failures")]
		public IReadOnlyCollection<ShardFailure> Failures { get; internal set; } = EmptyReadOnly<ShardFailure>.Collection;
	}
}
