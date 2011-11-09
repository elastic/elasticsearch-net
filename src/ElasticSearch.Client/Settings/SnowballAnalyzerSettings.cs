using Newtonsoft.Json;

namespace ElasticSearch.Client.Settings
{
    public class SnowballAnalyzerSettings : AnalyzerSettings
    {
        public SnowballAnalyzerSettings()
        {
            this.Type = "snowball";
        }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("stopwords")]
        public string StopWords { get; set; }
    }
}