// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ClusterRerouteExplanation
	{
		[DataMember(Name ="command")]
		public string Command { get; set; }

		[DataMember(Name ="decisions")]
		public IEnumerable<ClusterRerouteDecision> Decisions { get; set; }

		[DataMember(Name ="parameters")]
		public ClusterRerouteParameters Parameters { get; set; }
	}
}
