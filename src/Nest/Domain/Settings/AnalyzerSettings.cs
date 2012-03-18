using Newtonsoft.Json;

namespace Nest
{
    public abstract class AnalyzerSettings
    {
        [JsonProperty("type")]
        public string Type { get; protected set; }
    }
}