using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Analyzers, string, IAnalyzer>))]
	public interface IAnalyzers : IIsADictionary<string, IAnalyzer> { }

	public class Analyzers : IsADictionaryBase<string, IAnalyzer>, IAnalyzers
	{
		public Analyzers() : base() { }
		public Analyzers(IDictionary<string, IAnalyzer> container) : base(container) { }
		public Analyzers(Dictionary<string, IAnalyzer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, IAnalyzer analyzer) => BackingDictionary.Add(name, analyzer);
	}

	public class AnalyzersDescriptor : IsADictionaryDescriptorBase<AnalyzersDescriptor, IAnalyzers, string, IAnalyzer>
	{
		public AnalyzersDescriptor() : base(new Analyzers()) { }

		public AnalyzersDescriptor UserDefined(string name, IAnalyzer analyzer) => Assign(name, analyzer);

		/// <summary>
		/// An analyzer of type custom that allows to combine a Tokenizer with zero or more Token Filters, 
		/// and zero or more Char Filters. 
		/// <para>The custom analyzer accepts a logical/registered name of the tokenizer to use, and a list of 
		/// logical/registered names of token filters.</para>
		/// </summary>
		public AnalyzersDescriptor Custom(string name, Func<CustomAnalyzerDescriptor, ICustomAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new CustomAnalyzerDescriptor()));

		/// <summary>
		/// An analyzer of type keyword that “tokenizes” an entire stream as a single token. This is useful for data like zip codes, ids and so on. 
		/// <para>Note, when using mapping definitions, it make more sense to simply mark the field as not_analyzed.</para>
		/// </summary>
		public AnalyzersDescriptor Keyword(string name, Func<KeywordAnalyzerDescriptor, IKeywordAnalyzer> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new KeywordAnalyzerDescriptor()));

		/// <summary>
		/// A set of analyzers aimed at analyzing specific language text. 
		/// </summary>
		public AnalyzersDescriptor Language(string name, Func<LanguageAnalyzerDescriptor, ILanguageAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new LanguageAnalyzerDescriptor()));

		/// <summary>
		/// An analyzer of type pattern that can flexibly separate text into terms via a regular expression. 
		/// </summary>
		public AnalyzersDescriptor Pattern(string name, Func<PatternAnalyzerDescriptor, IPatternAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new PatternAnalyzerDescriptor()));

		/// <summary>
		/// An analyzer of type simple that is built using a Lower Case Tokenizer.
		/// </summary>
		public AnalyzersDescriptor Simple(string name, Func<SimpleAnalyzerDescriptor, ISimpleAnalyzer> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new SimpleAnalyzerDescriptor()));

		/// <summary>
		/// An analyzer of type snowball that uses the standard tokenizer, with standard filter, lowercase filter, stop filter, and snowball filter.
		/// <para> The Snowball Analyzer is a stemming analyzer from Lucene that is originally based on the snowball project from snowball.tartarus.org.</para>
		/// </summary>
		public AnalyzersDescriptor Snowball(string name, Func<SnowballAnalyzerDescriptor, ISnowballAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new SnowballAnalyzerDescriptor()));

		/// <summary>
		/// An analyzer of type standard that is built of using Standard Tokenizer, with Standard Token Filter, Lower Case Token Filter, and Stop Token Filter.
		/// </summary>
		public AnalyzersDescriptor Standard(string name, Func<StandardAnalyzerDescriptor, IStandardAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new StandardAnalyzerDescriptor()));

		/// <summary>
		/// An analyzer of type stop that is built using a Lower Case Tokenizer, with Stop Token Filter.
		/// </summary>
		public AnalyzersDescriptor Stop(string name, Func<StopAnalyzerDescriptor, IStopAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new StopAnalyzerDescriptor()));

		/// <summary>
		/// An analyzer of type whitespace that is built using a Whitespace Tokenizer.
		/// </summary>
		public AnalyzersDescriptor Whitespace(string name, Func<WhitespaceAnalyzerDescriptor, IWhitespaceAnalyzer> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new WhitespaceAnalyzerDescriptor()));

	}
}
