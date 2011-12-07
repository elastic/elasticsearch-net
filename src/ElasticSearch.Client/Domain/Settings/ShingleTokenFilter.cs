using Newtonsoft.Json;

namespace ElasticSearch.Client.Settings
{
    public class ShingleTokenFilter : TokenFilterSettings
    {
        public ShingleTokenFilter() : base("shingle")
        {
            MaxShingleSize = 2;
            OutputUnigrams = true;
        }

        [JsonProperty("max_shingle_size")]
        public int MaxShingleSize { get; set; }

        [JsonProperty("output_unigrams")]
        public bool OutputUnigrams { get; set; }
    }
}