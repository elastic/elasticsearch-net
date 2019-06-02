using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type nGram.
	/// </summary>
	public interface INGramTokenizer : ITokenizer
	{
		/// <summary>
		/// Maximum size in codepoints of a single n-gram, defaults to 2.
		/// </summary>
		[DataMember(Name ="max_gram")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxGram { get; set; }

		/// <summary>
		/// Minimum size in codepoints of a single n-gram, defaults to 1.
		/// </summary>
		[DataMember(Name ="min_gram")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MinGram { get; set; }

		/// <summary>
		/// Characters classes to keep in the tokens, Elasticsearch will
		/// split on characters that don’t belong to any of these classes.
		/// </summary>
		[DataMember(Name ="token_chars")]
		IEnumerable<TokenChar> TokenChars { get; set; }
	}

	/// <inheritdoc />
	public class NGramTokenizer : TokenizerBase, INGramTokenizer
	{
		public NGramTokenizer() => Type = "ngram";

		/// <inheritdoc />
		public int? MaxGram { get; set; }

		/// <inheritdoc />
		public int? MinGram { get; set; }

		/// <inheritdoc />
		public IEnumerable<TokenChar> TokenChars { get; set; }
	}

	/// <inheritdoc />
	public class NGramTokenizerDescriptor
		: TokenizerDescriptorBase<NGramTokenizerDescriptor, INGramTokenizer>, INGramTokenizer
	{
		protected override string Type => "ngram";
		int? INGramTokenizer.MaxGram { get; set; }

		int? INGramTokenizer.MinGram { get; set; }
		IEnumerable<TokenChar> INGramTokenizer.TokenChars { get; set; }

		/// <inheritdoc />
		public NGramTokenizerDescriptor MinGram(int? minGram) => Assign(minGram, (a, v) => a.MinGram = v);

		/// <inheritdoc />
		public NGramTokenizerDescriptor MaxGram(int? minGram) => Assign(minGram, (a, v) => a.MaxGram = v);

		/// <inheritdoc />
		public NGramTokenizerDescriptor TokenChars(IEnumerable<TokenChar> tokenChars) =>
			Assign(tokenChars, (a, v) => a.TokenChars = v);

		/// <inheritdoc />
		public NGramTokenizerDescriptor TokenChars(params TokenChar[] tokenChars) => Assign(tokenChars, (a, v) => a.TokenChars = v);
	}
}
