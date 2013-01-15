using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// EdgeNGram Filter settings
    /// </summary>
    public class EdgeNGramTokenFilter : TokenFilterSettings
    {
        public EdgeNGramTokenFilter()
            : base("edgeNGram")
        {
            MinGram = 1;
            MaxGram = 2;
	        Side = "front";
        }

        [JsonProperty("min_gram")]
        public int MinGram { get; set; }

        [JsonProperty("max_gram")]
        public int MaxGram { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }
    }
}