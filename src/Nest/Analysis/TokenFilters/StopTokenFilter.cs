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

		[JsonProperty("stopwords")]
		internal object _Stopwords { get; set; }

		[JsonIgnore]
		public IEnumerable<string> Stopwords
		{
			get { return _Stopwords as IEnumerable<string>; }
			set { _Stopwords = value; }
		}

		[JsonIgnore]
		public string PredefinedStopwords
		{
			get { return _Stopwords as string; }
			set { _Stopwords = value; }
		}

		[JsonProperty("enable_position_increments")]
		public bool? EnablePositionIncrements { get; set; }

        [JsonProperty("ignore_case")]
		public bool? IgnoreCase { get; set; }

        [JsonProperty("stopwords_path")]
        public string StopwordsPath { get; set; }

		[JsonProperty("remove_trailing")]
		public bool? RemoveTrailing { get; set; }
    }
}