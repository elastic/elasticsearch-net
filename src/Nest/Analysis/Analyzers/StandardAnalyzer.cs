using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type standard that is built of using Standard Tokenizer, with Standard Token Filter, Lower Case Token Filter, and Stop Token Filter.
	/// </summary>
	public class StandardAnalyzer : AnalyzerBase
    {
        public StandardAnalyzer()
        {
            Type = "standard";
        }

		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to the english stop words.
		/// </summary>
        [JsonProperty("stopwords")]
        public IEnumerable<string> StopWords { get; set; }

		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[JsonProperty("max_token_length")]
		public int? MaxTokenLength { get; set; }
    }
}