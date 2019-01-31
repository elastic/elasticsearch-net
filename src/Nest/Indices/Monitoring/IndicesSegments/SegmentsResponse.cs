using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public interface ISegmentsResponse : IResponse
	{
		IReadOnlyDictionary<string, IndexSegment> Indices { get; }
		ShardStatistics Shards { get; }
	}

	[DataContract]
	public class SegmentsResponse : ResponseBase, ISegmentsResponse
	{
		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, IndexSegment>))]
		public IReadOnlyDictionary<string, IndexSegment> Indices { get; internal set; } = EmptyReadOnly<string, IndexSegment>.Dictionary;

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
