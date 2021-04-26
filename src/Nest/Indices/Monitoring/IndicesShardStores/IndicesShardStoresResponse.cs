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
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	public class IndicesShardStoresResponse : ResponseBase
	{
		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, IndicesShardStores>))]
		public IReadOnlyDictionary<string, IndicesShardStores> Indices { get; internal set; } = EmptyReadOnly<string, IndicesShardStores>.Dictionary;
	}

	public class IndicesShardStores
	{
		[DataMember(Name = "shards")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, ShardStoreWrapper>))]
		public IReadOnlyDictionary<string, ShardStoreWrapper> Shards { get; internal set; } = EmptyReadOnly<string, ShardStoreWrapper>.Dictionary;
	}

	public class ShardStoreWrapper
	{
		[DataMember(Name = "stores")]
		public IReadOnlyCollection<ShardStore> Stores { get; internal set; } = EmptyReadOnly<ShardStore>.Collection;
	}

	[JsonFormatter(typeof(ShardStoreFormatter))]
	public class ShardStore
	{
		[DataMember(Name = "allocation")]
		public ShardStoreAllocation Allocation { get; internal set; }

		[DataMember(Name = "allocation_id")]
		public string AllocationId { get; internal set; }

		[DataMember(Name = "attributes")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, object>))]
		public IReadOnlyDictionary<string, object> Attributes { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;

		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		[DataMember(Name = "legacy_version")]
		public long? LegacyVersion { get; internal set; }

		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "store_exception")]
		public ShardStoreException StoreException { get; internal set; }

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }
	}

	public class ShardStoreException
	{
		[DataMember(Name = "reason")]
		public string Reason { get; internal set; }

		[DataMember(Name = "type")]
		public string Type { get; internal set; }
	}

	[StringEnum]
	public enum ShardStoreAllocation
	{
		[EnumMember(Value = "primary")]
		Primary,

		[EnumMember(Value = "replica")]
		Replica,

		[EnumMember(Value = "unused")]
		Unused
	}
}
