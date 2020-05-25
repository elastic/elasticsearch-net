// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum AllowRebalance
	{
		/// <summary>
		/// (default) Always allow rebalancing.
		/// </summary>
		[EnumMember(Value = "always")]
		All,

		/// <summary>
		/// Only when all primaries in the cluster are allocated.
		/// </summary>
		[EnumMember(Value = "indices_primaries_active")]
		Primaries,

		/// <summary>
		/// Only when all shards (primaries and replicas) in the cluster are allocated.
		/// </summary>
		[EnumMember(Value = "indices_all_active")]
		Replicas,
	}
}
