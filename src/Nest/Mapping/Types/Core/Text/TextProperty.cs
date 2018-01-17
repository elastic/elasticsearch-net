using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ITextProperty : ICoreProperty
	{
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("search_quote_analyzer")]
		string SearchQuoteAnalyzer { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		[JsonProperty("fielddata")]
		bool? Fielddata { get; set; }

		[JsonProperty("fielddata_frequency_filter")]
		IFielddataFrequencyFilter FielddataFrequencyFilter { get; set; }

		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("index_options")]
		IndexOptions? IndexOptions { get; set; }

		[JsonProperty("norms")]
		bool? Norms { get; set; }

		[JsonProperty("position_increment_gap")]
		int? PositionIncrementGap { get; set; }

		[JsonProperty("term_vector")]
		TermVectorOption? TermVector { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class TextProperty : CorePropertyBase, ITextProperty
	{
		public TextProperty() : base(FieldType.Text) { }

		public double? Boost { get; set; }
		public bool? EagerGlobalOrdinals { get; set; }
		public bool? Fielddata { get; set; }
		public IFielddataFrequencyFilter FielddataFrequencyFilter { get; set; }
		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public bool? Norms { get; set; }
		public int? PositionIncrementGap { get; set; }
		public string Analyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public string SearchQuoteAnalyzer { get; set; }
		public TermVectorOption? TermVector { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class TextPropertyDescriptor<T>
		: CorePropertyDescriptorBase<TextPropertyDescriptor<T>, ITextProperty, T>, ITextProperty
		where T : class
	{
		double? ITextProperty.Boost { get; set; }
		bool? ITextProperty.EagerGlobalOrdinals { get; set; }
		bool? ITextProperty.Fielddata { get; set; }
		IFielddataFrequencyFilter ITextProperty.FielddataFrequencyFilter { get; set; }
		bool? ITextProperty.Index { get; set; }
		IndexOptions? ITextProperty.IndexOptions { get; set; }
		bool? ITextProperty.Norms { get; set; }
		int? ITextProperty.PositionIncrementGap { get; set; }
		string ITextProperty.Analyzer { get; set; }
		string ITextProperty.SearchAnalyzer { get; set; }
		string ITextProperty.SearchQuoteAnalyzer { get; set; }
		TermVectorOption? ITextProperty.TermVector { get; set; }

		public TextPropertyDescriptor() : base(FieldType.Text) { }

		public TextPropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);
		public TextPropertyDescriptor<T> EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true) => Assign(a => a.EagerGlobalOrdinals = eagerGlobalOrdinals);
 		public TextPropertyDescriptor<T> Fielddata(bool? fielddata = true) => Assign(a => a.Fielddata = fielddata);
		public TextPropertyDescriptor<T> FielddataFrequencyFilter(Func<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter> selector) =>
			Assign(a => a.FielddataFrequencyFilter = selector?.Invoke(new FielddataFrequencyFilterDescriptor()));
		public TextPropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Index = index);
		public TextPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(a => a.IndexOptions = indexOptions);
		public TextPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(a => a.Norms = enabled);
		public TextPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) => Assign(a => a.PositionIncrementGap = positionIncrementGap);
		public TextPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);
		public TextPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);
		public TextPropertyDescriptor<T> SearchQuoteAnalyzer(string searchQuoteAnalyzer) => Assign(a => a.SearchQuoteAnalyzer = searchQuoteAnalyzer);
		public TextPropertyDescriptor<T> TermVector(TermVectorOption? termVector) => Assign(a => a.TermVector = termVector);
	}
}
