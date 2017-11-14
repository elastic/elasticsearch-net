using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IIndicesShardStoresResponse : IResponse
	{
		[JsonProperty("indices")]
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
		[JsonProperty("shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardStoreWrapper>))]
		public IReadOnlyDictionary<string, ShardStoreWrapper> Shards { get; internal set; } = EmptyReadOnly<string, ShardStoreWrapper>.Dictionary;
	}

	public class ShardStoreWrapper
	{
		[JsonProperty("stores")]
		public IReadOnlyCollection<ShardStore> Stores { get; internal set; } = EmptyReadOnly<ShardStore>.Collection;
	}

	[JsonConverter(typeof(ShardStoreJsonConverter))]
	public class ShardStore
	{
		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty("legacy_version")]
		public long? LegacyVersion { get; internal set; }

		[JsonProperty("allocation_id")]
		public string AllocationId { get; internal set; }

		[JsonProperty("store_exception")]
		public ShardStoreException StoreException { get; internal set; }

		[JsonProperty("allocation")]
		public ShardStoreAllocation Allocation { get; internal set; }

		[JsonProperty("attributes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		public IReadOnlyDictionary<string, object> Attributes { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}

	public class ShardStoreException
	{
		[JsonProperty("type")]
		public string Type { get; internal set; }
		[JsonProperty("reason")]
		public string Reason { get; internal set; }
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
