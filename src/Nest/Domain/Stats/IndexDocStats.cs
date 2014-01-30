using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    public class IndexDocStats
    {
        [JsonProperty("num_docs")]
        public int NumberOfDocs { get; set; }
        [JsonProperty("max_docs")]
        public int MaximumDocs { get; set; }
        [JsonProperty("deleted_docs")]
        public int DeletedDocs { get; set; }
    }
}
