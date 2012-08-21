using Newtonsoft.Json;

namespace Nest
{
    public class NgramTokenFiler : TokenFilterSettings
    {
        public NgramTokenFiler()
            : base("nGram")
        {
            MinGram = 1;
            MaxGram = 2;
        }

        [JsonProperty("min_gram")]
        public int MinGram { get; set; }

        [JsonProperty("max_gram")]
        public int MaxGram { get; set; }
    }
}