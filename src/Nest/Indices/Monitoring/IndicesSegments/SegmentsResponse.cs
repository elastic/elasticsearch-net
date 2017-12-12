using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISegmentsResponse : IResponse
	{
		ShardStatistics Shards { get; }
		IReadOnlyDictionary<string, IndexSegment> Indices { get; }
	}

	[JsonObject]
	public class SegmentsResponse : ResponseBase, ISegmentsResponse
	{

		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }

		[JsonProperty("indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndexSegment>))]
		public IReadOnlyDictionary<string, IndexSegment> Indices { get; internal set; } = EmptyReadOnly<string, IndexSegment>.Dictionary;


	}
}
