using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class NodeUsageMetadata
	{
		[JsonProperty(PropertyName = "total")]
		public int Total { get; internal set; }

		[JsonProperty(PropertyName = "successful")]
		public int Successful { get; internal set; }

		[JsonProperty(PropertyName = "failed")]
		public int Failed { get; internal set; }
	}

	public class NodeUsageInformation
	{
		[JsonProperty(PropertyName = "timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		[JsonProperty(PropertyName = "since")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Since { get;  internal set; }

		[JsonProperty(PropertyName = "rest_actions")]
		public IReadOnlyDictionary<string, int> RestActions { get;  internal set; }
	}

	public interface INodesUsageResponse : IResponse
	{
		string ClusterName { get; }

		IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; }

		NodeUsageMetadata NodeMetadata { get; }
	}

	public class NodesUsageResponse : ResponseBase, INodesUsageResponse
	{
		[JsonProperty(PropertyName = "cluster_name")]
		public string ClusterName { get; internal set; }

		[JsonProperty(PropertyName = "nodes")]
		public IReadOnlyDictionary<string, NodeUsageInformation> Nodes { get; internal set; } = EmptyReadOnly<string, NodeUsageInformation>.Dictionary;

		[JsonProperty(PropertyName = "_nodes")]
		public NodeUsageMetadata NodeMetadata { get; internal set; }
	}
}
