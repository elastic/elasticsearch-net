using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
    [JsonObject]
    public class IndexSegment
    {
        [JsonProperty(PropertyName = "shards")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, ShardsSegment> Shards { get; internal set; }
    }
}