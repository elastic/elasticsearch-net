using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalyzers : IWrapDictionary { }

	public class Analyzers : WrapDictionary<string, IAnalyzer>, IAnalyzers
	{
		public Analyzers() : base() { }
		public Analyzers(IDictionary<string, IAnalyzer> container) : base(container) { }
		public Analyzers(Dictionary<string, IAnalyzer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, IAnalyzer analyzer) => _backingDictionary.Add(name, analyzer);
	}

	public class AnalyzersDescriptor : WrapDictionary<string, IAnalyzer>, IAnalyzers
	{
		protected AnalyzersDescriptor Assign(string name, IAnalyzer analyzer) =>
			Fluent.Assign<AnalyzersDescriptor, AnalyzersDescriptor>(this, (a) => _backingDictionary.Add(name, analyzer));

		public AnalyzersDescriptor Add(string name, IAnalyzer analyzer) => Assign(name, analyzer);

		public AnalyzersDescriptor Custom(string name, Func<CustomAnalyzerDescriptor, ICustomAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new CustomAnalyzerDescriptor()));

		public AnalyzersDescriptor Keyword(string name, Func<KeywordAnalyzerDescriptor, IKeywordAnalyzer> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new KeywordAnalyzerDescriptor()));

		public AnalyzersDescriptor Language(string name, Func<LanguageAnalyzerDescriptor, ILanguageAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new LanguageAnalyzerDescriptor()));

		public AnalyzersDescriptor Pattern(string name, Func<PatternAnalyzerDescriptor, IPatternAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new PatternAnalyzerDescriptor()));

		public AnalyzersDescriptor Simple(string name, Func<SimpleAnalyzerDescriptor, ISimpleAnalyzer> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new SimpleAnalyzerDescriptor()));

		public AnalyzersDescriptor Snowball(string name, Func<SnowballAnalyzerDescriptor, ISnowballAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new SnowballAnalyzerDescriptor()));

		public AnalyzersDescriptor Standard(string name, Func<StandardAnalyzerDescriptor, IStandardAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new StandardAnalyzerDescriptor()));

		public AnalyzersDescriptor Stop(string name, Func<StopAnalyzerDescriptor, IStopAnalyzer> selector) =>
			Assign(name, selector?.Invoke(new StopAnalyzerDescriptor()));

		public AnalyzersDescriptor Whitespace(string name, Func<WhitespaceAnalyzerDescriptor, IWhitespaceAnalyzer> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new WhitespaceAnalyzerDescriptor()));

	}
}
