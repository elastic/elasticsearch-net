using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IIndicesShardStoresResponse : IResponse
	{
		[DataMember(Name ="indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndicesShardStores>))]
		IReadOnlyDictionary<string, IndicesShardStores> Indices { get; }
	}

	[DataContract]
	public class IndicesShardStoresResponse : ResponseBase, IIndicesShardStoresResponse
	{
		public IReadOnlyDictionary<string, IndicesShardStores> Indices { get; internal set; } = EmptyReadOnly<string, IndicesShardStores>.Dictionary;
	}

	public class IndicesShardStores
	{
		[DataMember(Name ="shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardStoreWrapper>))]
		public IReadOnlyDictionary<string, ShardStoreWrapper> Shards { get; internal set; } = EmptyReadOnly<string, ShardStoreWrapper>.Dictionary;
	}

	public class ShardStoreWrapper
	{
		[DataMember(Name ="stores")]
		public IReadOnlyCollection<ShardStore> Stores { get; internal set; } = EmptyReadOnly<ShardStore>.Collection;
	}

	[JsonConverter(typeof(ShardStoreJsonConverter))]
	public class ShardStore
	{
		[DataMember(Name ="allocation")]
		public ShardStoreAllocation Allocation { get; internal set; }

		[DataMember(Name ="allocation_id")]
		public string AllocationId { get; internal set; }

		[DataMember(Name ="attributes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		public IReadOnlyDictionary<string, object> Attributes { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;

		[DataMember(Name ="id")]
		public string Id { get; internal set; }

		[DataMember(Name ="legacy_version")]
		public long? LegacyVersion { get; internal set; }

		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="store_exception")]
		public ShardStoreException StoreException { get; internal set; }

		[DataMember(Name ="transport_address")]
		public string TransportAddress { get; internal set; }
	}

	public class ShardStoreException
	{
		[DataMember(Name ="reason")]
		public string Reason { get; internal set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }
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
