using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type nGram.
	/// </summary>
	public class NGramTokenizer : TokenizerBase
    {
		public NGramTokenizer()
        {
            Type = "nGram";
        }

		[JsonProperty("min_gram")]
		public int? MinGram { get; set; }

		[JsonProperty("max_gram")]
		public int? MaxGram { get; set; }
    }
}