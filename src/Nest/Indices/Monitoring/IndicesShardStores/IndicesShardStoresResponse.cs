using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IIndicesShardStoresResponse : IResponse
	{
		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndicesShardStores>))]
		IReadOnlyDictionary<string, IndicesShardStores> Indices { get; }
	}

	[JsonObject]
	public class IndicesShardStoresResponse : ResponseBase, IIndicesShardStoresResponse
	{
		public IReadOnlyDictionary<string, IndicesShardStores> Indices { get; internal set; } = EmptyReadOnly<string, IndicesShardStores>.Dictionary;
	}

	public class IndicesShardStores
	{
		[JsonProperty(PropertyName = "shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardStoreWrapper>))]
		public IReadOnlyDictionary<string, ShardStoreWrapper> Shards { get; internal set; } = EmptyReadOnly<string, ShardStoreWrapper>.Dictionary;
	}

	public class ShardStoreWrapper
	{
		[JsonProperty(PropertyName = "stores")]
		public IReadOnlyCollection<ShardStore> Stores { get; internal set; } = EmptyReadOnly<ShardStore>.Collection;
	}

	[JsonConverter(typeof(ShardStoreJsonConverter))]
	public class ShardStore
	{
		[JsonProperty("allocation")]
		public ShardStoreAllocation Allocation { get; internal set; }

		[JsonProperty("allocation_id")]
		public string AllocationId { get; internal set; }

		[JsonProperty("attributes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		public IReadOnlyDictionary<string, object> Attributes { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;

		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("legacy_version")]
		public long? LegacyVersion { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("store_exception")]
		public ShardStoreException StoreException { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }
	}

	public class ShardStoreException
	{
		[JsonProperty("reason")]
		public string Reason { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }
	}

	[JsonConverter(typeof(StringEnumConverter))]
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
