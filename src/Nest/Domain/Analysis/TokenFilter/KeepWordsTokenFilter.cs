using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
	/// </summary>
	public class KeepWordsTokenFilter : TokenFilterBase
	{
		public KeepWordsTokenFilter()
			: base("keep")
		{

		}

		/// <summary>
		/// A list of words to keep.
		/// </summary>
		[JsonProperty("keep_words")]
		public IEnumerable<string> KeepWords { get; set; }

		/// <summary>
		/// A path to a words file.
		/// </summary>
		[JsonProperty("rules_path")]
		public string KeepWordsPath { get; set; }

		/// <summary>
		/// A boolean indicating whether to lower case the words.
		/// </summary>
		[JsonProperty("keep_words_case")]
		public bool? KeepWordsCase { get; set; }

	}
}