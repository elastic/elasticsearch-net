using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface IClusterStatsResponse : IResponse
	{
		[JsonProperty("cluster_name")]
		string ClusterName { get; set; }

		[JsonProperty("timestamp")]
		long Timestamp { get; set; }

		[JsonProperty("status")]
		ClusterStatus Status { get; set; }

		[JsonProperty("indices")]
		ClusterIndicesStats Indices { get; set; }

		[JsonProperty("nodes")]
		ClusterNodesStats Nodes { get; set; }
	}

	public class ClusterStatsResponse : BaseResponse, IClusterStatsResponse
	{
		public string ClusterName { get; set; }

		public long Timestamp { get; set; }

		public ClusterStatus Status { get; set; }

		public ClusterIndicesStats Indices { get; set; }

		public ClusterNodesStats Nodes { get; set; }
	}
}
