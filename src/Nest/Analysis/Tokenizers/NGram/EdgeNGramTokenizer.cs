using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type edgeNGram.
	/// </summary>
	public interface IEdgeNGramTokenizer : ITokenizer
	{
		/// <summary>
		/// Minimum size in codepoints of a single n-gram
		/// </summary>
		[JsonProperty("min_gram")]
		int? MinGram { get; set; }

		/// <summary>
		/// Maximum size in codepoints of a single n-gram
		/// </summary>
		[JsonProperty("max_gram")]
		int? MaxGram { get; set; }

		/// <summary>
		/// Characters classes to keep in the tokens, Elasticsearch
		///  will split on characters that don’t belong to any of these classes.
		/// </summary>
		[JsonProperty("token_chars")]
		IEnumerable<TokenChar> TokenChars { get; set; }
	}
	/// <inheritdoc/>
	public class EdgeNGramTokenizer : TokenizerBase, IEdgeNGramTokenizer
	{
		public EdgeNGramTokenizer() { Type = "edge_ngram"; }

		/// <inheritdoc/>
		public int? MinGram { get; set; }

		/// <inheritdoc/>
		public int? MaxGram { get; set; }

		/// <inheritdoc/>
		public IEnumerable<TokenChar> TokenChars { get; set; }
	}
	/// <inheritdoc/>
	public class EdgeNGramTokenizerDescriptor 
		: TokenizerDescriptorBase<EdgeNGramTokenizerDescriptor, IEdgeNGramTokenizer>, IEdgeNGramTokenizer
	{
		protected override string Type => "edge_ngram";

		int? IEdgeNGramTokenizer.MinGram { get; set; }
		int? IEdgeNGramTokenizer.MaxGram { get; set; }
		IEnumerable<TokenChar> IEdgeNGramTokenizer.TokenChars { get; set; }

		/// <inheritdoc/>
		public EdgeNGramTokenizerDescriptor MinGram(int? minGram) => Assign(a => a.MinGram = minGram);

		/// <inheritdoc/>
		public EdgeNGramTokenizerDescriptor MaxGram(int? maxGram) => Assign(a => a.MaxGram = maxGram);

		/// <inheritdoc/>
		public EdgeNGramTokenizerDescriptor TokenChars(IEnumerable<TokenChar> tokenChars) => 
			Assign(a => a.TokenChars = tokenChars);

		/// <inheritdoc/>
		public EdgeNGramTokenizerDescriptor TokenChars(params TokenChar[] tokenChars) => 
			Assign(a => a.TokenChars = tokenChars);
	}

}