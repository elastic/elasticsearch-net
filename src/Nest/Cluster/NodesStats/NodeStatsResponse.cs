using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface INodesStatsResponse : INodesResponse
	{
		[JsonProperty("cluster_name")]
		string ClusterName { get; }

		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, NodeStats>))]
		IReadOnlyDictionary<string, NodeStats> Nodes { get; }
	}

	public class NodesStatsResponse : NodesResponseBase, INodesStatsResponse
	{
		public string ClusterName { get; internal set; }

		public IReadOnlyDictionary<string, NodeStats> Nodes { get; internal set; } = EmptyReadOnly<string, NodeStats>.Dictionary;

	}
}
