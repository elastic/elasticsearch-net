using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type pattern that can flexibly separate text into terms via a regular expression. 
	/// </summary>
	public class PatternTokenizer : TokenizerBase
    {
		public PatternTokenizer()
        {
            Type = "pattern";
        }

		/// <summary>
		/// The regular expression pattern, defaults to \W+.
		/// </summary>
		[JsonProperty("pattern")]
		public string Pattern { get; set; }

		/// <summary>
		/// The regular expression flags.
		/// </summary>
		[JsonProperty("flags")]
		public string Flags { get; set; }
		
		/// <summary>
		/// Which group to extract into tokens. Defaults to -1 (split).
		/// </summary>
		[JsonProperty("group")]
		public int? Group { get; set; }
    }
}