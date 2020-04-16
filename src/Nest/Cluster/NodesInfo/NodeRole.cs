using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	// TODO: Make a string in 8.0
	[StringEnum]
	public enum NodeRole
	{
		[EnumMember(Value = "master")]
		Master,

		[EnumMember(Value = "data")]
		Data,

		[EnumMember(Value = "client")]
		Client,

		[EnumMember(Value = "ingest")]
		Ingest,

		[EnumMember(Value = "ml")]
		MachineLearning,

		[EnumMember(Value = "voting_only")]
		VotingOnly,

		[EnumMember(Value = "transform")]
		Transform,

		[EnumMember(Value = "remote_cluster_client")]
		RemoteClusterClient,

		[EnumMember(Value = "coordinating_only")]
		CoordinatingOnly,
	}
}
