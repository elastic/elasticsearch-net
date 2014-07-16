using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
    public class IndexStatus
    {
        [JsonProperty(PropertyName = "index")]
        public IndexSizeStats Index { get; set; }

        [JsonProperty(PropertyName = "translog")]
        public TranslogStats Translog { get; set; }

        [JsonProperty(PropertyName = "docs")]
        public IndexDocStats IndexDocs { get; set; }
        
        [JsonProperty(PropertyName = "merges")]
        public MergesStats Merges { get; set; }
        
        [JsonProperty(PropertyName = "refresh")]
        public RefreshStats Refresh { get; set; }

        [JsonProperty(PropertyName = "flush")]
        public FlushStats Flush { get; set; }
    
        [JsonProperty(PropertyName = "shards")]
        [JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
        public Dictionary<string, dynamic> Shards { get; internal set; }
    

    }
}
