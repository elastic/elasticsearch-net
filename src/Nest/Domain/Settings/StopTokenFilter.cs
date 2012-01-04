using Newtonsoft.Json;

namespace Nest.Settings
{
    public class StopTokenFilter : TokenFilterSettings
    {
        public StopTokenFilter() : base("stop")
        {
            EnablePositionIncrements = true;
        }

        [JsonProperty("enable_position_increments")]
        public bool EnablePositionIncrements { get; set; }

        [JsonProperty("ignore_case")]
        public bool IgnoreCase { get; set; }

        [JsonProperty("stopwords_path")]
        public string StopwordsPath { get; set; }

        [JsonProperty("stopwords")]
        public string Stopwords { get; set; }
    }
}