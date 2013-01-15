using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Protects words from being modified by stemmers. Must be placed before any stemming filters.
	/// </summary>
    public class KeywordMarkerTokenFilter : TokenFilterBase
    {
		public KeywordMarkerTokenFilter()
			: base("keyword_marker")
        {
        }

        [JsonProperty("keywords")]
        public IEnumerable<string> Keywords { get; set; }

		[JsonProperty("keywords_path")]
        public string KeywordsPath { get; set; }

		[JsonProperty("ignore_case")]
		public bool? IgnoreCase { get; set; }
    }
}