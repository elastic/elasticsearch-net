// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardRouting
	{
		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name ="relocating_node")]
		public string RelocatingNode { get; internal set; }

		[DataMember(Name ="state")]
		public ShardRoutingState State { get; internal set; }
	}
}
