using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class UpdateResponse : BaseResponse
	{
		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; private set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }

    [JsonProperty(PropertyName = "_index")]
    public string Index { get; private set; }
    [JsonProperty(PropertyName = "_type")]
    public string Type { get; private set; }
    [JsonProperty(PropertyName = "_id")]
    public string Id { get; private set; }
    [JsonProperty(PropertyName = "_version")]
    public string Version { get; private set; }
	}
}
