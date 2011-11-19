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
        public string Filter { get; set; }

        [JsonProperty("char_filter")]
        public string CharFilter { get; set; }
    }
}