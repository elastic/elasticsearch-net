using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type nGram.
	/// </summary>
	public interface INGramTokenizer : ITokenizer
	{
		/// <summary>
		/// Minimum size in codepoints of a single n-gram, defaults to 1.
		/// </summary>
		[JsonProperty("min_gram")]
		int? MinGram { get; set; }

		/// <summary>
		/// Maximum size in codepoints of a single n-gram, defaults to 2.
		/// </summary>
		[JsonProperty("max_gram")]
		int? MaxGram { get; set; }

		/// <summary>
		/// Characters classes to keep in the tokens, Elasticsearch will
		/// split on characters that don’t belong to any of these classes. 
		/// </summary>
		[JsonProperty("token_chars")]
		IEnumerable<TokenChar> TokenChars { get; set; }
	}

	/// <inheritdoc/>
	public class NGramTokenizer : TokenizerBase, INGramTokenizer
	{
		public NGramTokenizer() { Type = "ngram"; }

		/// <inheritdoc/>
		public int? MinGram { get; set; }

		/// <inheritdoc/>
		public int? MaxGram { get; set; }

		/// <inheritdoc/>
		public IEnumerable<TokenChar> TokenChars { get; set; }
	}
	/// <inheritdoc/>
	public class NGramTokenizerDescriptor 
		: TokenizerDescriptorBase<NGramTokenizerDescriptor, INGramTokenizer>, INGramTokenizer
	{
		protected override string Type => "ngram";

		int? INGramTokenizer.MinGram { get; set; }
		int? INGramTokenizer.MaxGram { get; set; }
		IEnumerable<TokenChar> INGramTokenizer.TokenChars { get; set; }

		/// <inheritdoc/>
		public NGramTokenizerDescriptor MinGram(int? minGram) => Assign(a => a.MinGram = minGram);

		/// <inheritdoc/>
		public NGramTokenizerDescriptor MaxGram(int? minGram) => Assign(a => a.MaxGram = minGram);

		/// <inheritdoc/>
		public NGramTokenizerDescriptor TokenChars(IEnumerable<TokenChar> tokenChars) => 
			Assign(a => a.TokenChars = tokenChars);

		/// <inheritdoc/>
		public NGramTokenizerDescriptor TokenChars(params TokenChar[] tokenChars) => Assign(a => a.TokenChars = tokenChars);

	}
}