using System.Collections.Generic;
using System.Runtime.Serialization;

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
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndexSegment>))]
		public IReadOnlyDictionary<string, IndexSegment> Indices { get; internal set; } = EmptyReadOnly<string, IndexSegment>.Dictionary;

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
