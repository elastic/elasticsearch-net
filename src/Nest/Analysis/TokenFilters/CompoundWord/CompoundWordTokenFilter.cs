using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Token filters that allow to decompose compound words.
	/// </summary>
	public interface ICompoundWordTokenFilter : ITokenFilter
	{
		/// <summary>
		/// A list of words to use.
		/// </summary>
		[JsonProperty("word_list")]
		IEnumerable<string> WordList { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a list of words.
		/// </summary>
		[JsonProperty("word_list_path")]
		string WordListPath { get; set; }

		/// <summary>
		/// Minimum word size.
		/// </summary>
		[JsonProperty("min_word_size")]
		int MinWordSize { get; set; }

		/// <summary>
		/// Minimum subword size.
		/// </summary>
		[JsonProperty("min_subword_size")]
		int MinSubwordSize { get; set; }

		/// <summary>
		/// Maximum subword size.
		/// </summary>
		[JsonProperty("max_subword_size")]
		int MaxSubwordSize { get; set; }

		/// <summary>
		/// Only matching the longest.
		/// </summary>
		[JsonProperty("only_longest_match")]
		bool OnlyLongestMatch { get; set; }
	}
	public abstract class CompoundWordTokenFilter : TokenFilterBase
	{
		protected CompoundWordTokenFilter(string type) : base(type) { } 

		/// <inheritdoc/>
		public IEnumerable<string> WordList { get; set; }

		/// <inheritdoc/>
		public string WordListPath { get; set; }

		/// <inheritdoc/>
		public int MinWordSize { get; set; }

		/// <inheritdoc/>
		public int MinSubwordSize { get; set; }

		/// <inheritdoc/>
		public int MaxSubwordSize { get; set; }

		/// <inheritdoc/>
		public bool OnlyLongestMatch { get; set; }
	}

}
