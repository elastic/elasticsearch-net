// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	public class AnalyzeTokenizersSelector : SelectorBase
	{
		/// <summary>
		/// A tokenizer of type edgeNGram.
		/// </summary>
		public ITokenizer EdgeNGram(Func<EdgeNGramTokenizerDescriptor, IEdgeNGramTokenizer> selector) =>
			selector?.Invoke(new EdgeNGramTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type nGram.
		/// </summary>
		public ITokenizer NGram(Func<NGramTokenizerDescriptor, INGramTokenizer> selector) =>
			selector?.Invoke(new NGramTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type keyword that emits the entire input as a single input.
		/// </summary>
		public ITokenizer Keyword(Func<KeywordTokenizerDescriptor, IKeywordTokenizer> selector) =>
			selector?.Invoke(new KeywordTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type letter that divides text at non-letters. That’s to say, it defines tokens as maximal strings of adjacent letters.
		/// <para>
		/// Note, this does a decent job for most European languages, but does a terrible job for some Asian languages, where words are not
		/// separated by spaces.
		/// </para>
		/// </summary>
		public ITokenizer Letter(Func<LetterTokenizerDescriptor, ILetterTokenizer> selector) =>
			selector?.Invoke(new LetterTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type lowercase that performs the function of Letter Tokenizer and Lower Case Token Filter together.
		/// <para>It divides text at non-letters and converts them to lower case. </para>
		/// <para>While it is functionally equivalent to the combination of Letter Tokenizer and Lower Case Token Filter, </para>
		/// <para>there is a performance advantage to doing the two tasks at once, hence this (redundant) implementation.</para>
		/// </summary>
		public ITokenizer Lowercase(Func<LowercaseTokenizerDescriptor, ILowercaseTokenizer> selector) =>
			selector?.Invoke(new LowercaseTokenizerDescriptor());

		/// <summary>
		///  The path_hierarchy tokenizer takes something like this:
		/// <para>/something/something/else</para>
		/// <para>And produces tokens:</para>
		/// <para></para>
		/// <para>/something</para>
		/// <para>/something/something</para>
		/// <para>/something/something/else</para>
		/// </summary>
		public ITokenizer PathHierarchy(Func<PathHierarchyTokenizerDescriptor, IPathHierarchyTokenizer> selector) =>
			selector?.Invoke(new PathHierarchyTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type pattern that can flexibly separate text into terms via a regular expression.
		/// </summary>
		public ITokenizer Pattern(Func<PatternTokenizerDescriptor, IPatternTokenizer> selector) =>
			selector?.Invoke(new PatternTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type standard providing grammar based tokenizer that is a good tokenizer for most European language documents.
		/// <para>The tokenizer implements the Unicode Text Segmentation algorithm, as specified in Unicode Standard Annex #29.</para>
		/// </summary>
		public ITokenizer Standard(Func<StandardTokenizerDescriptor, IStandardTokenizer> selector = null) =>
			selector.InvokeOrDefault(new StandardTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type uax_url_email which works exactly like the standard tokenizer, but tokenizes emails and urls as single tokens
		/// </summary>
		public ITokenizer UaxEmailUrl(Func<UaxEmailUrlTokenizerDescriptor, IUaxEmailUrlTokenizer> selector) =>
			selector?.Invoke(new UaxEmailUrlTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type whitespace that divides text at whitespace.
		/// </summary>
		public ITokenizer Whitespace(Func<WhitespaceTokenizerDescriptor, IWhitespaceTokenizer> selector = null) =>
			selector.InvokeOrDefault(new WhitespaceTokenizerDescriptor());

		/// <summary>
		/// A tokenizer of type pattern that can flexibly separate text into terms via a regular expression.
		/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
		/// </summary>
		public ITokenizer Kuromoji(Func<KuromojiTokenizerDescriptor, IKuromojiTokenizer> selector) =>
			selector?.Invoke(new KuromojiTokenizerDescriptor());

		/// <summary>
		/// Tokenizes text into words on word boundaries, as defined in UAX #29: Unicode Text Segmentation. It behaves much
		/// like the standard tokenizer, but adds better support for some Asian languages by using a dictionary-based approach
		/// to identify words in Thai, Lao, Chinese, Japanese, and Korean, and using custom rules to break Myanmar and Khmer
		/// text into syllables.
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public ITokenizer Icu(Func<IcuTokenizerDescriptor, IIcuTokenizer> selector) =>
			selector?.Invoke(new IcuTokenizerDescriptor());

		/// <inheritdoc cref="INoriTokenizer" />
		public ITokenizer Nori(Func<NoriTokenizerDescriptor, INoriTokenizer> selector) =>
			selector.Invoke(new NoriTokenizerDescriptor());

		/// <inheritdoc cref="ICharGroupTokenizer.TokenizeOnCharacters" />
		/// >
		public ITokenizer CharGroup(Func<CharGroupTokenizerDescriptor, ICharGroupTokenizer> selector) =>
			selector?.Invoke(new CharGroupTokenizerDescriptor());
	}
}
