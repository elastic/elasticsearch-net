using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	///     A generic property to map properties that may be of different types.
	///     Not all methods are valid for all types.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGenericProperty : IDocValuesProperty
	{
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("fielddata")]
		IStringFielddata Fielddata { get; set; }

		[JsonProperty("ignore_above")]
		int? IgnoreAbove { get; set; }

		[JsonProperty("index")]
		bool? Index { get; set; }

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

	/// <summary>
	///     A generic property to map properties that may be of different types.
	///     Not all methods are valid for all types.
	/// </summary>
	[DebuggerDisplay("{DebugDisplay}")]
	public class GenericProperty : DocValuesPropertyBase, IGenericProperty
	{
		public GenericProperty() : base(FieldType.Object) => TypeOverride = null;

		public string Analyzer { get; set; }
		public double? Boost { get; set; }
		public IStringFielddata Fielddata { get; set; }
		public int? IgnoreAbove { get; set; }
		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public bool? Norms { get; set; }
		public string NullValue { get; set; }
		public int? PositionIncrementGap { get; set; }
		public string SearchAnalyzer { get; set; }

		public TermVectorOption? TermVector { get; set; }

		public string Type
		{
			get => TypeOverride;
			set => TypeOverride = value;
		}
	}

	/// <summary>
	///     A generic property to map properties that may be of different types.
	///     Not all methods are valid for all types.
	/// </summary>
	/// <typeparam name="T">the type on which the property is declared</typeparam>
	[DebuggerDisplay("{DebugDisplay}")]
	public class GenericPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<GenericPropertyDescriptor<T>, IGenericProperty, T>, IGenericProperty
		where T : class
	{
		public GenericPropertyDescriptor() : base(FieldType.Object) => TypeOverride = null;

		string IGenericProperty.Analyzer { get; set; }
		double? IGenericProperty.Boost { get; set; }
		IStringFielddata IGenericProperty.Fielddata { get; set; }
		int? IGenericProperty.IgnoreAbove { get; set; }
		bool? IGenericProperty.Index { get; set; }
		IndexOptions? IGenericProperty.IndexOptions { get; set; }
		bool? IGenericProperty.Norms { get; set; }
		string IGenericProperty.NullValue { get; set; }
		int? IGenericProperty.PositionIncrementGap { get; set; }
		string IGenericProperty.SearchAnalyzer { get; set; }
		TermVectorOption? IGenericProperty.TermVector { get; set; }

		public GenericPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public GenericPropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);

		public GenericPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(a => a.Fielddata = selector?.Invoke(new StringFielddataDescriptor()));

		public GenericPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		public GenericPropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Index = index);

		public GenericPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public GenericPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(a => a.Norms = enabled);

		public GenericPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public GenericPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) =>
			Assign(a => a.PositionIncrementGap = positionIncrementGap);

		public GenericPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public GenericPropertyDescriptor<T> TermVector(TermVectorOption? termVector) => Assign(a => a.TermVector = termVector);

		public GenericPropertyDescriptor<T> Type(string type) => Assign(a => TypeOverride = type);
	}
}
