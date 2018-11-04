using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[Obsolete(
		"Only valid for indices created before Elasticsearch 5.0, removed in Elasticsearch 6.0.  For newly created indices, use `text` or `keyword` instead.")]
	public interface IStringProperty : IDocValuesProperty
	{
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		[JsonIgnore]
		[Obsolete("Use FielddataUpgrade")]
		IStringFielddata Fielddata { get; set; }

		[JsonProperty("fielddata_frequency_filter")]
		IFielddataFrequencyFilter FielddataFrequencyFilter { get; set; }

		[JsonProperty("fielddata")]
		bool? FielddataUpgrade { get; set; }

		[JsonProperty("ignore_above")]
		int? IgnoreAbove { get; set; }

		[JsonProperty("include_in_all")]
		/// <remarks>Removed in 6.x</remarks>
		bool? IncludeInAll { get; set; }

		[JsonProperty("index")]
		FieldIndexOption? Index { get; set; }

		[JsonProperty("index_options")]
		IndexOptions? IndexOptions { get; set; }

		[JsonProperty("norms")]
		bool? Norms { get; set; }

		[JsonProperty("null_value")]
		string NullValue { get; set; }

		[JsonProperty("position_increment_gap")]
		int? PositionIncrementGap { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("term_vector")]
		TermVectorOption? TermVector { get; set; }
	}

	[Obsolete(
		"Only valid for indices created before Elasticsearch 5.0, removed in Elasticsearch 6.0.  For newly created indices, use `text` or `keyword` instead.")]
	[DebuggerDisplay("{DebugDisplay}")]
	public class StringProperty : DocValuesPropertyBase, IStringProperty
	{
		public StringProperty() : base("string") { }

		public string Analyzer { get; set; }
		public double? Boost { get; set; }
		public bool? EagerGlobalOrdinals { get; set; }

		[Obsolete("Use FielddataUpgrade")]
		public IStringFielddata Fielddata { get; set; }

		public IFielddataFrequencyFilter FielddataFrequencyFilter { get; set; }
		public bool? FielddataUpgrade { get; set; }
		public int? IgnoreAbove { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		public bool? IncludeInAll { get; set; }

		public FieldIndexOption? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public bool? Norms { get; set; }
		public string NullValue { get; set; }
		public int? PositionIncrementGap { get; set; }
		public string SearchAnalyzer { get; set; }
		public TermVectorOption? TermVector { get; set; }
	}

	[Obsolete(
		"Only valid for indices created before Elasticsearch 5.0, removed in Elasticsearch 6.0.  For newly created indices, use `text` or `keyword` instead.")]
	[DebuggerDisplay("{DebugDisplay}")]
	public class StringPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<StringPropertyDescriptor<T>, IStringProperty, T>, IStringProperty
		where T : class
	{
		public StringPropertyDescriptor() : base("string") { }

		string IStringProperty.Analyzer { get; set; }
		double? IStringProperty.Boost { get; set; }
		bool? IStringProperty.EagerGlobalOrdinals { get; set; }

		[Obsolete("Use FielddataUpgrade")]
		IStringFielddata IStringProperty.Fielddata { get; set; }

		IFielddataFrequencyFilter IStringProperty.FielddataFrequencyFilter { get; set; }
		bool? IStringProperty.FielddataUpgrade { get; set; }
		int? IStringProperty.IgnoreAbove { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		bool? IStringProperty.IncludeInAll { get; set; }

		FieldIndexOption? IStringProperty.Index { get; set; }
		IndexOptions? IStringProperty.IndexOptions { get; set; }
		bool? IStringProperty.Norms { get; set; }
		string IStringProperty.NullValue { get; set; }
		int? IStringProperty.PositionIncrementGap { get; set; }
		string IStringProperty.SearchAnalyzer { get; set; }
		TermVectorOption? IStringProperty.TermVector { get; set; }

		/// <summary>
		/// Shortcut into .Index(FieldIndexOption.NotAnalyzed)
		/// </summary>
		public StringPropertyDescriptor<T> NotAnalyzed() => Index(FieldIndexOption.NotAnalyzed);

		public StringPropertyDescriptor<T> Index(FieldIndexOption index) => Assign(a => a.Index = index);

		public StringPropertyDescriptor<T> TermVector(TermVectorOption termVector) => Assign(a => a.TermVector = termVector);

		public StringPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public StringPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public StringPropertyDescriptor<T> IndexOptions(IndexOptions indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public StringPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public StringPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public StringPropertyDescriptor<T> Norms(bool enabled = true) => Assign(a => a.Norms = enabled);

		public StringPropertyDescriptor<T> IgnoreAbove(int ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		/// <remarks>Removed in 6.x</remarks>
		public StringPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);

		public StringPropertyDescriptor<T> PositionIncrementGap(int positionIncrementGap) =>
			Assign(a => a.PositionIncrementGap = positionIncrementGap);

		[Obsolete("Use Fielddata that takes a bool argument")]
		public StringPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(a => a.Fielddata = selector?.Invoke(new StringFielddataDescriptor()));

		public StringPropertyDescriptor<T> EagerGlobalOrdinals(bool eagerGlobalOrdinals = true) =>
			Assign(a => a.EagerGlobalOrdinals = eagerGlobalOrdinals);

		public StringPropertyDescriptor<T> Fielddata(bool fielddata = true) => Assign(a => a.FielddataUpgrade = fielddata);

		public StringPropertyDescriptor<T> FielddataFrequencyFilter(Func<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter> selector) =>
			Assign(a => a.FielddataFrequencyFilter = selector?.Invoke(new FielddataFrequencyFilterDescriptor()));
	}
}
