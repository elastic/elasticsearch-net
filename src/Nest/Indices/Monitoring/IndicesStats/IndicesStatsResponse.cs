using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public interface IIndicesStatsResponse : IResponse
	{
		IReadOnlyDictionary<string, IndicesStats> Indices { get; }
		ShardStatistics Shards { get; }
		IndicesStats Stats { get; }
	}

	[DataContract]
	public class IndicesStatsResponse : ResponseBase, IIndicesStatsResponse
	{
		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, IndicesStats>))]
		public IReadOnlyDictionary<string, IndicesStats> Indices { get; internal set; } = EmptyReadOnly<string, IndicesStats>.Dictionary;

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }

		[DataMember(Name ="_all")]
		public IndicesStats Stats { get; internal set; }
	}
}
