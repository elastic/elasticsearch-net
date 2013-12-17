using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    public interface  IStatusResponse: IResponse
    {
        bool OK { get; }
        ShardsMetaData Shards { get; }
        Dictionary<string, IndexStatus> Indices { get; } 
    }

    [JsonObject]
    public class StatusResponse : BaseResponse, IStatusResponse
    {
        public StatusResponse()
        {
            this.IsValid = true;
        }

        [JsonProperty(PropertyName = "ok")]
        public bool OK { get; internal set; }

        [JsonProperty(PropertyName = "_shards")]
        public ShardsMetaData Shards { get; internal set; }

        [JsonProperty("indices")]
        [JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
        public Dictionary<string, IndexStatus> Indices { get; internal set; }

    }
}
