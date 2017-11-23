using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class NodesMetadata
	{
		[JsonProperty("total")]
		public int Total { get; internal set; }

		[JsonProperty("successful")]
		public int Successful { get; internal set; }

		[JsonProperty("failed")]
		public int Failed { get; internal set; }
	}

	public class NodeUsageInformation
	{
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		[JsonProperty("since")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Since { get;  internal set; }

		[JsonProperty("rest_actions")]
		public IReadOnlyDictionary<string, int> RestActions { get;  internal set; }
	}

	public interface INodesUsageResponse : IResponse
	{
		string ClusterName { get; }

		IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; }

		NodesMetadata NodesMetadata { get; }
	}

	public class NodesUsageResponse : ResponseBase, INodesUsageResponse
	{
		[JsonProperty("cluster_name")]
		public string ClusterName { get; internal set; }

		[JsonProperty("nodes")]
		public IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; internal set; } = EmptyReadOnly<string, NodeUsageInformation>.Dictionary;

		[JsonProperty("_nodes")]
		public NodesMetadata NodesMetadata { get; internal set; }
	}
}
