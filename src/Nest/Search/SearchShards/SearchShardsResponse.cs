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

namespace Nest
{
	public class SearchShardsResponse : ResponseBase
	{
		[DataMember(Name = "nodes")]
		public IReadOnlyDictionary<string, SearchNode> Nodes { get; internal set; } = EmptyReadOnly<string, SearchNode>.Dictionary;

		[DataMember(Name = "shards")]
		public IReadOnlyCollection<IReadOnlyCollection<SearchShard>> Shards { get; internal set; } =
			EmptyReadOnly<IReadOnlyCollection<SearchShard>>.Collection;
	}

	[DataContract]
	public class SearchNode
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }
	}

	[DataContract]
	public class SearchShard
	{
		[DataMember(Name = "index")]
		public string Index { get; internal set; }

		[DataMember(Name = "node")]
		public string Node { get; internal set; }

		[DataMember(Name = "primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name = "relocating_node")]
		public string RelocatingNode { get; internal set; }

		[DataMember(Name = "shard")]
		public int Shard { get; internal set; }

		[DataMember(Name = "state")]
		public string State { get; internal set; }
	}
}
