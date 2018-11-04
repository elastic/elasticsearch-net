using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISegmentsResponse : IResponse
	{
		IReadOnlyDictionary<string, IndexSegment> Indices { get; }
		ShardsMetaData Shards { get; }
	}

	[JsonObject]
	public class SegmentsResponse : ResponseBase, ISegmentsResponse
	{
		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndexSegment>))]
		public IReadOnlyDictionary<string, IndexSegment> Indices { get; internal set; } = EmptyReadOnly<string, IndexSegment>.Dictionary;

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}
