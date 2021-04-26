/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
using Elastic.Transport;
using Nest.Utf8Json;

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
