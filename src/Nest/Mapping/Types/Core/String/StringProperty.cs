using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[Obsolete("Only valid for indices created before Elasticsearch 5.0 and will be removed in the next major version.  For newly created indices, use `text` or `keyword` instead.")]
	public interface IStringProperty : IDocValuesProperty
	{
		[JsonProperty("index")]
		FieldIndexOption? Index { get; set; }

		[JsonProperty("term_vector")]
		TermVectorOption? TermVector { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		string NullValue { get; set; }

		[JsonProperty("norms")]
		bool? Norms { get; set; }

		[JsonProperty("index_options")]
		IndexOptions? IndexOptions { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("ignore_above")]
		int? IgnoreAbove { get; set; }

		[JsonProperty("position_increment_gap")]
		int? PositionIncrementGap { get; set; }

		[JsonProperty("fielddata")]
		IStringFielddata Fielddata { get; set; }
	}

	[Obsolete("Only valid for indices created before Elasticsearch 5.0 and will be removed in the next major version.  For newly created indices, use `text` or `keyword` instead.")]
	[DebuggerDisplay("{DebugDisplay}")]
	public class StringProperty : DocValuesPropertyBase, IStringProperty
	{
		public StringProperty() : base(FieldType.String) { }

		public FieldIndexOption? Index { get; set; }
		public TermVectorOption? TermVector { get; set; }
		public double? Boost { get; set; }
		public string NullValue { get; set; }
		public bool? Norms { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public string Analyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public int? IgnoreAbove { get; set; }
		public int? PositionIncrementGap { get; set; }
		public IStringFielddata Fielddata { get; set; }
	}

	[Obsolete("Only valid for indices created before Elasticsearch 5.0 and will be removed in the next major version.  For newly created indices, use `text` or `keyword` instead.")]
	[DebuggerDisplay("{DebugDisplay}")]
	public class StringPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<StringPropertyDescriptor<T>, IStringProperty, T>, IStringProperty
		where T : class
	{
		FieldIndexOption? IStringProperty.Index { get; set; }
		TermVectorOption? IStringProperty.TermVector { get; set; }
		double? IStringProperty.Boost { get; set; }
		string IStringProperty.NullValue { get; set; }
		bool? IStringProperty.Norms { get; set; }
		IndexOptions? IStringProperty.IndexOptions { get; set; }
		string IStringProperty.Analyzer { get; set; }
		string IStringProperty.SearchAnalyzer { get; set; }
		int? IStringProperty.IgnoreAbove { get; set; }
		int? IStringProperty.PositionIncrementGap { get; set; }
		IStringFielddata IStringProperty.Fielddata { get; set; }

		public StringPropertyDescriptor() : base(FieldType.String) { }

		/// <summary>
		/// Shortcut into .Index(FieldIndexOption.NotAnalyzed)
		/// </summary>
		public StringPropertyDescriptor<T> NotAnalyzed() => Index(FieldIndexOption.NotAnalyzed);

		public StringPropertyDescriptor<T> Index(FieldIndexOption? index) => Assign(a => a.Index = index);

		public StringPropertyDescriptor<T> TermVector(TermVectorOption? termVector) => Assign(a => a.TermVector = termVector);

		public StringPropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);

		public StringPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public StringPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public StringPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public StringPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public StringPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(a => a.Norms = enabled);

		public StringPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		public StringPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) => Assign(a => a.PositionIncrementGap = positionIncrementGap);

		public StringPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(a => a.Fielddata = selector?.Invoke(new StringFielddataDescriptor()));
	}
}
