using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type stop that is built using a Lower Case Tokenizer, with Stop Token Filter.
	/// </summary>
	public class StopAnalyzer : AnalyzerBase
    {
		public StopAnalyzer()
        {
            Type = "stop";
        }

		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to the english stop words.
		/// </summary>
        [JsonProperty("stopwords")]
        public IEnumerable<string> StopWords { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a stopwords file configuration.
		/// </summary>
		[JsonProperty("stopwords_path")]
		public string StopwordsPath { get; set; }
    }
}