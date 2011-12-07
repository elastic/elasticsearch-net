using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Settings
{
    public class CustomAnalyzerSettings : AnalyzerSettings
    {
        public CustomAnalyzerSettings()
        {
            this.Type = "custom";
        }

        [JsonProperty("tokenizer")]
        public string Tokenizer { get; set; }

        [JsonProperty("filter")]
        public IList<string> Filter { get; set; }

        [JsonProperty("char_filter")]
        public IList<string> CharFilter { get; set; }
    }
}