using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type nGram.
	/// </summary>
    public class NgramTokenFilter : TokenFilterBase
    {
        public NgramTokenFilter()
            : base("nGram")
        {
        }

        [JsonProperty("min_gram")]
        public int? MinGram { get; set; }

        [JsonProperty("max_gram")]
        public int? MaxGram { get; set; }

		[JsonProperty("token_chars")]
		public IList<string> TokenChars { get; set; }
    }
}