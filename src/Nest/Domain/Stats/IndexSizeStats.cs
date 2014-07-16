using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
    public class IndexSizeStats
    {
        [JsonProperty(PropertyName = "primary_size")]
        public string PrimarySize { get; set; }

        [JsonProperty(PropertyName = "primary_size_in_bytes")]
        public long PrimarySizeInBytes { get; set; }

        [JsonProperty(PropertyName = "size")]
        public string Size { get; set; }

        [JsonProperty(PropertyName = "size_in_bytes")]
        public long SizeInBytes { get; set; }

    }
}
