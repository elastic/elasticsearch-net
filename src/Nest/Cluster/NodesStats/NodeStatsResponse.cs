using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface INodesStatsResponse : INodesResponse
	{
		[DataMember(Name ="cluster_name")]
		string ClusterName { get; }

		[DataMember(Name ="nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, NodeStats>))]
		IReadOnlyDictionary<string, NodeStats> Nodes { get; }
	}

	public class NodesStatsResponse : NodesResponseBase, INodesStatsResponse
	{
		public string ClusterName { get; internal set; }

		public IReadOnlyDictionary<string, NodeStats> Nodes { get; internal set; } = EmptyReadOnly<string, NodeStats>.Dictionary;
	}
}
