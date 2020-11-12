// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

		[EnumMember(Value = "data_cold")]
		DataCold,

		[EnumMember(Value = "data_content")]
		DataContent,

		[EnumMember(Value = "data_hot")]
		DataHot,

		[EnumMember(Value = "data_warm")]
		DataWarm,

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
