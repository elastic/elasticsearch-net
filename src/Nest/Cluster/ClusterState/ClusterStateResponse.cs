// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DynamicResponseFormatter<ClusterStateResponse>))]
	public class ClusterStateResponse : DynamicResponseBase
	{
		public DynamicDictionary State => Self.BackingDictionary;

		[DataMember(Name = "cluster_name")]
		public string ClusterName => State.Get<string>("cluster_name");

		/// <summary>The Universally Unique Identifier for the cluster.</summary>
		/// <remarks>While the cluster is still forming, it is possible for the `cluster_uuid` to be `_na_`.</remarks>
		[DataMember(Name = "cluster_uuid")]
		public string ClusterUUID => State.Get<string>("cluster_uuid");

		[DataMember(Name = "master_node")]
		public string MasterNode => State.Get<string>("master_node");

		[DataMember(Name = "state_uuid")]
		public string StateUUID => State.Get<string>("state_uuid");

		[DataMember(Name = "version")]
		public long? Version => State.Get<long?>("version");
	}
}
