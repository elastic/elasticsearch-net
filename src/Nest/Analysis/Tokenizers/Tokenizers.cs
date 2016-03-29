using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Tokenizers, string, ITokenizer>))]
	public interface ITokenizers : IIsADictionary<string, ITokenizer> { }

	public class Tokenizers : IsADictionaryBase<string, ITokenizer>, ITokenizers
	{
		public Tokenizers() : base() { }
		public Tokenizers(IDictionary<string, ITokenizer> container) : base(container) { }
		public Tokenizers(Dictionary<string, ITokenizer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, ITokenizer analyzer) => BackingDictionary.Add(name, analyzer);
	}

	public class TokenizersDescriptor :IsADictionaryDescriptorBase<TokenizersDescriptor, ITokenizers, string, ITokenizer>
	{
		public TokenizersDescriptor() : base(new Tokenizers()) { }

		public TokenizersDescriptor UserDefined(string name, ITokenizer analyzer) => Assign(name, analyzer);
		
		/// <summary>
		/// A tokenizer of type edgeNGram.
		/// </summary>
		public TokenizersDescriptor EdgeNGram(string name, Func<EdgeNGramTokenizerDescriptor, IEdgeNGramTokenizer> selector) =>
			Assign(name, selector?.Invoke(new EdgeNGramTokenizerDescriptor()));

		/// <summary>
		/// A tokenizer of type nGram.
		/// </summary>
		public TokenizersDescriptor NGram(string name, Func<NGramTokenizerDescriptor, INGramTokenizer> selector) =>
			Assign(name, selector?.Invoke(new NGramTokenizerDescriptor()));

		/// <summary>
		/// A tokenizer of type keyword that emits the entire input as a single input.
		/// </summary>
		public TokenizersDescriptor Keyword(string name, Func<KeywordTokenizerDescriptor, IKeywordTokenizer> selector) =>
			Assign(name, selector?.Invoke(new KeywordTokenizerDescriptor()));

		/// <summary>
		/// A tokenizer of type letter that divides text at non-letters. That’s to say, it defines tokens as maximal strings of adjacent letters. 
		/// <para>Note, this does a decent job for most European languages, but does a terrible job for some Asian languages, where words are not separated by spaces.</para>
		/// </summary>
		public TokenizersDescriptor Letter(string name, Func<LetterTokenizerDescriptor, ILetterTokenizer> selector) =>
			Assign(name, selector?.Invoke(new LetterTokenizerDescriptor()));

		/// <summary>
		/// A tokenizer of type lowercase that performs the function of Letter Tokenizer and Lower Case Token Filter together. 
		/// <para>It divides text at non-letters and converts them to lower case. </para>
		/// <para>While it is functionally equivalent to the combination of Letter Tokenizer and Lower Case Token Filter, </para>
		/// <para>there is a performance advantage to doing the two tasks at once, hence this (redundant) implementation.</para>
		/// </summary>
		public TokenizersDescriptor Lowercase(string name, Func<LowercaseTokenizerDescriptor, ILowercaseTokenizer> selector) =>
			Assign(name, selector?.Invoke(new LowercaseTokenizerDescriptor()));

		/// <summary>
		/// The path_hierarchy tokenizer takes something like this:
		///<para>/something/something/else</para>
		///<para>And produces tokens:</para>
		///<para></para>
		///<para>/something</para>
		///<para>/something/something</para>
		///<para>/something/something/else</para>
		/// </summary>
		public TokenizersDescriptor PathHierarchy(string name, Func<PathHierarchyTokenizerDescriptor, IPathHierarchyTokenizer> selector) =>
			Assign(name, selector?.Invoke(new PathHierarchyTokenizerDescriptor()));

		/// <summary>
		/// A tokenizer of type pattern that can flexibly separate text into terms via a regular expression. 
		/// </summary>
		public TokenizersDescriptor Pattern(string name, Func<PatternTokenizerDescriptor, IPatternTokenizer> selector) =>
			Assign(name, selector?.Invoke(new PatternTokenizerDescriptor()));

		/// <summary>
		/// A tokenizer of type standard providing grammar based tokenizer that is a good tokenizer for most European language documents. 
		/// <para>The tokenizer implements the Unicode Text Segmentation algorithm, as specified in Unicode Standard Annex #29.</para>
		/// </summary>
		public TokenizersDescriptor Standard(string name, Func<StandardTokenizerDescriptor, IStandardTokenizer> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new StandardTokenizerDescriptor()));

		/// <summary>
		/// A tokenizer of type uax_url_email which works exactly like the standard tokenizer, but tokenizes emails and urls as single tokens
		/// </summary>
		public TokenizersDescriptor UaxEmailUrl(string name, Func<UaxEmailUrlTokenizerDescriptor, IUaxEmailUrlTokenizer> selector) =>
			Assign(name, selector?.Invoke(new UaxEmailUrlTokenizerDescriptor()));

		/// <summary>
		/// A tokenizer of type whitespace that divides text at whitespace.
		/// </summary>
		public TokenizersDescriptor Whitespace(string name, Func<WhitespaceTokenizerDescriptor, IWhitespaceTokenizer> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new WhitespaceTokenizerDescriptor()));
	}
}
