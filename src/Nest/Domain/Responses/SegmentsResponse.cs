using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    public interface ISegmentsResponse : IResponse
    {
        bool OK { get; }
        ShardsMetaData Shards { get; }
        Dictionary<string, IndexSegment> Indices { get; set; }
    }

    [JsonObject]
	public class SegmentsResponse : BaseResponse, ISegmentsResponse
    {
		public SegmentsResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName="indices")]
		public Dictionary<string, IndexSegment> Indices { get; set; } 

		
	}
}