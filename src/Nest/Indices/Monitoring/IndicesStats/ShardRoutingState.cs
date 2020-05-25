// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum ShardRoutingState
	{
		/// <summary>
		/// The shard is not assigned to any node.
		/// </summary>
		[EnumMember(Value = "UNASSIGNED")]
		Unassigned,

		/// <summary>
		/// The shard is initializing (probably recovering from either a peer shard or gateway).
		/// </summary>
		[EnumMember(Value = "INITIALIZING")]
		Initializing,

		/// <summary>
		/// The shard is started.
		/// </summary>
		[EnumMember(Value = "STARTED")]
		Started,

		/// <summary>
		/// The shard is in the process being relocated.
		/// </summary>
		[EnumMember(Value = "RELOCATING")]
		Relocating
	}
}
