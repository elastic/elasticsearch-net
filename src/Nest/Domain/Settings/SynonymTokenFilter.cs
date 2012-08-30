using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
    public class SynonymTokenFilter : TokenFilterSettings
    {
        public SynonymTokenFilter() : base("synonym")
        {
        }

        [JsonProperty("synonyms_path", NullValueHandling = NullValueHandling.Ignore)]
        public string SynonymsPath { get; set; }

        [JsonProperty("format", NullValueHandling=NullValueHandling.Ignore)]
        public string Format { get; set; }

        [JsonProperty("synonyms", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Synonyms { get; set; }

        [JsonProperty("ignore_case", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IgnoreCase { get; set; }

        [JsonProperty("expand", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Expand { get; set; }
    }
}
