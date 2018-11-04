using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class SnapshotRestore
	{
		[JsonProperty("indices")]
		public IReadOnlyCollection<IndexName> Indices { get; internal set; } =
			EmptyReadOnly<IndexName>.Collection;

		[JsonProperty("snapshot")]
		public string Name { get; internal set; }

		[JsonProperty("shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}
