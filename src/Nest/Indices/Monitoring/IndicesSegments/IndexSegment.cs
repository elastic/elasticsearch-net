using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexSegment
	{
		[JsonProperty("shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardsSegment>))]
		public IReadOnlyDictionary<string, ShardsSegment> Shards { get; internal set; } =
			EmptyReadOnly<string, ShardsSegment>.Dictionary;
	}
}
