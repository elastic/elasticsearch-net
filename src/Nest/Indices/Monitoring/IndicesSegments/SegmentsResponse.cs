using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISegmentsResponse : IResponse
	{
		IReadOnlyDictionary<string, IndexSegment> Indices { get; }
		ShardStatistics Shards { get; }
	}

	[JsonObject]
	public class SegmentsResponse : ResponseBase, ISegmentsResponse
	{
		[JsonProperty("indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndexSegment>))]
		public IReadOnlyDictionary<string, IndexSegment> Indices { get; internal set; } = EmptyReadOnly<string, IndexSegment>.Dictionary;

		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
