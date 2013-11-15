using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type stop that removes stop words from token streams.
	/// </summary>
    public class StopTokenFilter : TokenFilterBase
    {
        public StopTokenFilter() : base("stop")
        {

        }

        [JsonProperty("enable_position_increments")]
		public bool? EnablePositionIncrements { get; set; }

        [JsonProperty("ignore_case")]
		public bool? IgnoreCase { get; set; }

        [JsonProperty("stopwords_path")]
        public string StopwordsPath { get; set; }

        [JsonProperty("stopwords")]
        public IEnumerable<string> Stopwords { get; set; }

		[JsonProperty("remove_trailing")]
		public bool? RemoveTrailing { get; set; }
    }
}