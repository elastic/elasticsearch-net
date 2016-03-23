using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndicesShardStoresResponse : IResponse
	{
		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, IndicesShardStores> Indices { get; set; }
	}

	[JsonObject]
	public class IndicesShardStoresResponse : ResponseBase, IIndicesShardStoresResponse
	{
		public Dictionary<string, IndicesShardStores> Indices { get; set; }
	}

	public class IndicesShardStores
	{
		[JsonProperty(PropertyName = "shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, ShardStoreWrapper> Shards { get; set; }
	}

	public class ShardStoreWrapper
	{
		public IEnumerable<ShardStore> Stores { get; set; }
	}

	[JsonConverter(typeof(ShardStoreJsonConverter))]
	public class ShardStore
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; set; }

		[JsonProperty("version")]
		public long Version { get; set; }

		[JsonProperty("store_exeption")]
		public ShardStoreException StoreException { get; set; }

		[JsonProperty("allocation")]
		public ShardStoreAllocation Allocation { get; set; }

		[JsonProperty("attributes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, object> Attributes { get; set; }
	}

	public class ShardStoreException
	{
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("reason")]
		public string Reason { get; set; }
	}

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
