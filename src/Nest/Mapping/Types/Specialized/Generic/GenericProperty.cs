using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGenericProperty : IProperty
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

		[JsonProperty("position_increment_gap")]
		int? PositionIncrementGap { get; set; }

		[JsonProperty("fielddata")]
		IStringFielddata Fielddata { get; set; }
	}

	/// <summary>
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
	/// </summary>
	public class GenericProperty : PropertyBase, IGenericProperty
	{
		public GenericProperty() : base(null) { }

		public TermVectorOption? TermVector { get; set; }
		public double? Boost { get; set; }
		public string SearchAnalyzer { get; set; }
		public bool? IncludeInAll { get; set; }
		public int? IgnoreAbove { get; set; }
		public int? PositionIncrementGap { get; set; }
		public IStringFielddata Fielddata { get; set; }
		public FieldIndexOption? Index { get; set; }
		public string NullValue { get; set; }
		public INorms Norms { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public string Analyzer { get; set; }
	}

	/// <summary>
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
	/// </summary>
	/// <typeparam name="T">the type on which the property is declared</typeparam>
	public class GenericPropertyDescriptor<T>
		: PropertyDescriptorBase<GenericPropertyDescriptor<T>, IGenericProperty, T>, IGenericProperty
		where T : class
	{
		FieldIndexOption? IGenericProperty.Index { get; set; }
		TermVectorOption? IGenericProperty.TermVector { get; set; }
		double? IGenericProperty.Boost { get; set; }
		string IGenericProperty.NullValue { get; set; }
		INorms IGenericProperty.Norms { get; set; }
		IndexOptions? IGenericProperty.IndexOptions { get; set; }
		string IGenericProperty.Analyzer { get; set; }
		string IGenericProperty.SearchAnalyzer { get; set; }
		bool? IGenericProperty.IncludeInAll { get; set; }
		int? IGenericProperty.IgnoreAbove { get; set; }
		int? IGenericProperty.PositionIncrementGap { get; set; }
		IStringFielddata IGenericProperty.Fielddata { get; set; }

		public GenericPropertyDescriptor() : base(null) { }

		public GenericPropertyDescriptor<T> Index(FieldIndexOption? index = FieldIndexOption.NotAnalyzed) => Assign(a => a.Index = index);

		public GenericPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public GenericPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public GenericPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);

		public GenericPropertyDescriptor<T> NotAnalyzed() => Index(FieldIndexOption.NotAnalyzed);

		public GenericPropertyDescriptor<T> Index(FieldIndexOption index) => Assign(a => a.Index = index);

		public GenericPropertyDescriptor<T> TermVector(TermVectorOption termVector) => Assign(a => a.TermVector = termVector);

		public GenericPropertyDescriptor<T> IndexOptions(IndexOptions indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public GenericPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public GenericPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public GenericPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector) => Assign(a => a.Norms = selector?.Invoke(new NormsDescriptor()));

		public GenericPropertyDescriptor<T> IgnoreAbove(int ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		public GenericPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) => Assign(a => a.PositionIncrementGap = positionIncrementGap);

		public GenericPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(a => a.Fielddata = selector?.Invoke(new StringFielddataDescriptor()));
	}
}
