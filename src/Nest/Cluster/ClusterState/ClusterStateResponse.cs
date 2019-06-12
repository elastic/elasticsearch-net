using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DynamicResponseFormatter<ClusterStateResponse>))]
	public class ClusterStateResponse : DynamicResponseBase
	{
		public DynamicBody State => Self.BackingBody;

		[DataMember(Name = "cluster_name")]
		public string ClusterName => State["cluster_name"] as string;

		/// <summary>The Universally Unique Identifier for the cluster.</summary>
		/// <remarks>While the cluster is still forming, it is possible for the `cluster_uuid` to be `_na_`.</remarks>
		[DataMember(Name = "cluster_uuid")]
		public string ClusterUUID => State["cluster_uuid"] as string;

		[DataMember(Name = "master_node")]
		public string MasterNode => State["master_node"] as string;

		[DataMember(Name = "state_uuid")]
		public string StateUUID => State["state_uuid"] as string;

		[DataMember(Name = "version")]
		public long? Version => State["version"] as long?;
	}
}
