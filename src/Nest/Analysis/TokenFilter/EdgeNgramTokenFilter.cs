using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
	/// A token filter of type edgeNGram.
    /// </summary>
    public class EdgeNGramTokenFilter : TokenFilterBase
    {
        public EdgeNGramTokenFilter()
            : base("edgeNGram")
        {

        }

        [JsonProperty("min_gram")]
        public int? MinGram { get; set; }

        [JsonProperty("max_gram")]
        public int? MaxGram { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }
    }
}