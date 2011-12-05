using Newtonsoft.Json;

namespace ElasticSearch.Client.Settings
{
    public abstract class TokenFilterSettings
    {
        protected TokenFilterSettings(string type)
        {
            this.Type = type;
        }

        [JsonProperty("type")]
        public string Type { get; protected set; }
    }
}