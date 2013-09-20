using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
    [JsonObject]
    public class IndexSegment
    {
        [JsonProperty(PropertyName = "shards")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, ShardsSegment> Shards { get; internal set; }
    }
}