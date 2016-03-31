using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IStringProperty : IProperty
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
		INorms Norms { get; set; }

		[JsonProperty("index_options")]
		IndexOptions? IndexOptions { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("ignore_above")]
		int? IgnoreAbove { get; set; }

		[JsonProperty("position_offset_gap")]
		int? PositionOffsetGap { get; set; }

		[JsonProperty("fielddata")]
		IStringFielddata Fielddata { get; set; }
	}

	public class StringProperty : PropertyBase, IStringProperty
	{
		public StringProperty() : base("string") { }

		public FieldIndexOption? Index { get; set; }
		public TermVectorOption? TermVector { get; set; }
		public double? Boost { get; set; }
		public string NullValue { get; set; }
		public INorms Norms { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public string Analyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public bool? IncludeInAll { get; set; }
		public int? IgnoreAbove { get; set; }
		public int? PositionOffsetGap { get; set; }
		public IStringFielddata Fielddata { get; set; }
	}

	public class StringPropertyDescriptor<T>
		: PropertyDescriptorBase<StringPropertyDescriptor<T>, IStringProperty, T>, IStringProperty
		where T : class
	{
		FieldIndexOption? IStringProperty.Index { get; set; }
		TermVectorOption? IStringProperty.TermVector { get; set; }
		double? IStringProperty.Boost { get; set; }
		string IStringProperty.NullValue { get; set; }
		INorms IStringProperty.Norms { get; set; }
		IndexOptions? IStringProperty.IndexOptions { get; set; }
		string IStringProperty.Analyzer { get; set; }
		string IStringProperty.SearchAnalyzer { get; set; }
		bool? IStringProperty.IncludeInAll { get; set; }
		int? IStringProperty.IgnoreAbove { get; set; }
		int? IStringProperty.PositionOffsetGap { get; set; }
		IStringFielddata IStringProperty.Fielddata { get; set; }

		public StringPropertyDescriptor() : base("string") { }

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

		public StringPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector) => Assign(a => a.Norms = selector?.Invoke(new NormsDescriptor()));

		public StringPropertyDescriptor<T> IgnoreAbove(int ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		public StringPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);

		public StringPropertyDescriptor<T> PositionOffsetGap(int positionOffsetGap) => Assign(a => a.PositionOffsetGap = positionOffsetGap);

		public StringPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(a => a.Fielddata = selector?.Invoke(new StringFielddataDescriptor()));
	}
}
