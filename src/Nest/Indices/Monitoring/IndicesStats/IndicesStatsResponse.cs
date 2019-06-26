using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{

	[DataContract]
	public class IndicesStatsResponse : ResponseBase
	{
		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, IndicesStats>))]
		public IReadOnlyDictionary<string, IndicesStats> Indices { get; internal set; } = EmptyReadOnly<string, IndicesStats>.Dictionary;

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }

		[DataMember(Name ="_all")]
		public IndicesStats Stats { get; internal set; }
	}
}
