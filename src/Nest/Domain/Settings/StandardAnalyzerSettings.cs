using Newtonsoft.Json;

namespace Nest.Settings
{
    public class StandardAnalyzerSettings : AnalyzerSettings
    {
        public StandardAnalyzerSettings()
        {
            Type = "standard";
        }

        [JsonProperty("stopwords")]
        public string StopWords { get; set; }
    }
}