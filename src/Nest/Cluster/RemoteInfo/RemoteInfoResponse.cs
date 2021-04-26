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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<RemoteInfoResponse, string, RemoteInfo>))]
	public class RemoteInfoResponse : DictionaryResponseBase<string, RemoteInfo>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, RemoteInfo> Remotes => Self.BackingDictionary;
	}

	public class RemoteInfo
	{
		[DataMember(Name = "connected")]
		public bool Connected { get; internal set; }

		[DataMember(Name = "skip_unavailable")]
		public bool SkipUnavailable { get; internal set; }

		[DataMember(Name = "initial_connect_timeout")]
		public Time InitialConnectTimeout { get; internal set; }

		[DataMember(Name = "max_connections_per_cluster")]
		public int MaxConnectionsPerCluster { get; internal set; }

		[DataMember(Name = "num_nodes_connected")]
		public long NumNodesConnected { get; internal set; }

		[DataMember(Name = "seeds")]
		public IReadOnlyCollection<string> Seeds { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
