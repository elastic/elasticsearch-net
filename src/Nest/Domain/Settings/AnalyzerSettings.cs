using Newtonsoft.Json;

namespace Nest.Settings
{
    public abstract class AnalyzerSettings
    {
        [JsonProperty("type")]
        public string Type { get; protected set; }
    }
}