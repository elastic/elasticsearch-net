using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest {
	[InterfaceDataContract]
	public interface ITextProperty : ICoreProperty {
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		[DataMember(Name = "boost")]
		double? Boost { get; set; }

		[DataMember(Name = "eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		[DataMember(Name = "fielddata")]
		bool? Fielddata { get; set; }

		[DataMember(Name = "fielddata_frequency_filter")]
		IFielddataFrequencyFilter FielddataFrequencyFilter { get; set; }

		[DataMember(Name = "index")]
		bool? Index { get; set; }

		[DataMember(Name = "index_options")]
		IndexOptions? IndexOptions { get; set; }

		[DataMember(Name = "index_phrases")]
		bool? IndexPhrases { get; set; }

		[DataMember(Name = "index_prefixes")]
		ITextIndexPrefixes IndexPrefixes { get; set; }

		[DataMember(Name = "norms")]
		bool? Norms { get; set; }

		[DataMember(Name = "position_increment_gap")]
		int? PositionIncrementGap { get; set; }

		[DataMember(Name = "search_analyzer")]
		string SearchAnalyzer { get; set; }

		[DataMember(Name = "search_quote_analyzer")]
		string SearchQuoteAnalyzer { get; set; }

		[DataMember(Name = "term_vector")]
		TermVectorOption? TermVector { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class TextProperty : CorePropertyBase, ITextProperty {
		public TextProperty() : base(FieldType.Text) { }

		protected TextProperty(FieldType fieldType) : base(fieldType) { }

		public string Analyzer { get; set; }

		public double? Boost { get; set; }
		public bool? EagerGlobalOrdinals { get; set; }
		public bool? Fielddata { get; set; }
		public IFielddataFrequencyFilter FielddataFrequencyFilter { get; set; }
		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public bool? IndexPhrases { get; set; }
		public ITextIndexPrefixes IndexPrefixes { get; set; }
		public bool? Norms { get; set; }
		public int? PositionIncrementGap { get; set; }
		public string SearchAnalyzer { get; set; }
		public string SearchQuoteAnalyzer { get; set; }
		public TermVectorOption? TermVector { get; set; }
	}

	public abstract class TextPropertyDescriptorBase<TDescriptor, TInterface, T>
		: CorePropertyDescriptorBase<TDescriptor, TInterface, T>, ITextProperty
		where TDescriptor : TextPropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ITextProperty
		where T : class
	{ 

		protected TextPropertyDescriptorBase(FieldType fieldType) : base(fieldType) { }

		string ITextProperty.Analyzer { get; set; }
		double? ITextProperty.Boost { get; set; }
		bool? ITextProperty.EagerGlobalOrdinals { get; set; }
		bool? ITextProperty.Fielddata { get; set; }
		IFielddataFrequencyFilter ITextProperty.FielddataFrequencyFilter { get; set; }
		bool? ITextProperty.Index { get; set; }
		IndexOptions? ITextProperty.IndexOptions { get; set; }
		bool? ITextProperty.IndexPhrases { get; set; }
		ITextIndexPrefixes ITextProperty.IndexPrefixes { get; set; }
		bool? ITextProperty.Norms { get; set; }
		int? ITextProperty.PositionIncrementGap { get; set; }
		string ITextProperty.SearchAnalyzer { get; set; }
		string ITextProperty.SearchQuoteAnalyzer { get; set; }
		TermVectorOption? ITextProperty.TermVector { get; set; }

		public TDescriptor Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public TDescriptor EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true) =>
			Assign(eagerGlobalOrdinals, (a, v) => a.EagerGlobalOrdinals = v);

		public TDescriptor Fielddata(bool? fielddata = true) => Assign(fielddata, (a, v) => a.Fielddata = v);

		public TDescriptor FielddataFrequencyFilter(Func<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter> selector) =>
			Assign(selector, (a, v) => a.FielddataFrequencyFilter = v?.Invoke(new FielddataFrequencyFilterDescriptor()));

		public TDescriptor IndexPrefixes(Func<TextIndexPrefixesDescriptor, ITextIndexPrefixes> selector) =>
			Assign(selector, (a, v) => a.IndexPrefixes = v?.Invoke(new TextIndexPrefixesDescriptor()));

		public TDescriptor Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		public TDescriptor IndexPhrases(bool? indexPhrases = true) => Assign(indexPhrases, (a, v) => a.IndexPhrases = v);

		public TDescriptor IndexOptions(IndexOptions? indexOptions) => Assign(indexOptions, (a, v) => a.IndexOptions = v);

		public TDescriptor Norms(bool? enabled = true) => Assign(enabled, (a, v) => a.Norms = v);

		public TDescriptor PositionIncrementGap(int? positionIncrementGap) =>
			Assign(positionIncrementGap, (a, v) => a.PositionIncrementGap = v);

		public TDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		public TDescriptor SearchAnalyzer(string searchAnalyzer) => Assign(searchAnalyzer, (a, v) => a.SearchAnalyzer = v);

		public TDescriptor SearchQuoteAnalyzer(string searchQuoteAnalyzer) => Assign(searchQuoteAnalyzer, (a, v) => a.SearchQuoteAnalyzer = v);

		public TDescriptor TermVector(TermVectorOption? termVector) => Assign(termVector, (a, v) => a.TermVector = v);
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class TextPropertyDescriptor<T>
		: TextPropertyDescriptorBase<TextPropertyDescriptor<T>, ITextProperty, T>, ITextProperty
		where T : class
	{
		public TextPropertyDescriptor() : base(FieldType.Text) { }
	}
}
