using Newtonsoft.Json;

namespace ElasticSearch.Client.Settings
{
    public class PatternReplaceTokenFilterSettings : TokenFilterSettings
    {
        public PatternReplaceTokenFilterSettings() : base("pattern_replace")
        {
        }

        [JsonProperty("pattern")]
        public string Pattern { get; set; }
    }
}