// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum RebalanceEnable
	{
		/// <summary>
		/// (default) Allows shard balancing for all kinds of shards.
		/// </summary>
		[EnumMember(Value = "all")]
		All,

		/// <summary>
		/// Allows shard balancing only for primary shards.
		/// </summary>
		[EnumMember(Value = "primaries")]
		Primaries,

		/// <summary>
		/// Allows shard balancing only for replica shards.
		/// </summary>
		[EnumMember(Value = "replicas")]
		Replicas,

		/// <summary>
		/// No shard balancing of any kind are allowed for any indices.
		/// </summary>
		[EnumMember(Value = "none")]
		None,
	}
}
