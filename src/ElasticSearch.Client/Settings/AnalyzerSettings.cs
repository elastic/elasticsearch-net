using Newtonsoft.Json;

namespace ElasticSearch.Client.Settings
{
    public abstract class AnalyzerSettings
    {
        [JsonProperty("type")]
        public string Type { get; protected set; }
    }
}