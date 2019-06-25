using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class SegmentsResponse : ResponseBase
	{
		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, IndexSegment>))]
		public IReadOnlyDictionary<string, IndexSegment> Indices { get; internal set; } = EmptyReadOnly<string, IndexSegment>.Dictionary;

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
