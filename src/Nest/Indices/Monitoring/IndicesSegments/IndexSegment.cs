using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class IndexSegment
	{
		[JsonProperty(PropertyName = "shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardsSegment>))]
		public IReadOnlyDictionary<string, ShardsSegment> Shards { get; internal set; } =
			EmptyReadOnly<string, ShardsSegment>.Dictionary;
	}
}
