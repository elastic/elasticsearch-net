using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
	/// </summary>
	[DataContract]
	public interface IGenericProperty : IDocValuesProperty
	{
		[DataMember(Name ="analyzer")]
		string Analyzer { get; set; }

		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="fielddata")]
		IStringFielddata Fielddata { get; set; }

		[DataMember(Name ="ignore_above")]
		int? IgnoreAbove { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="index_options")]
		IndexOptions? IndexOptions { get; set; }

		[DataMember(Name ="norms")]
		bool? Norms { get; set; }

		[DataMember(Name ="null_value")]
		string NullValue { get; set; }

		[DataMember(Name ="position_increment_gap")]
		int? PositionIncrementGap { get; set; }

		[DataMember(Name ="search_analyzer")]
		string SearchAnalyzer { get; set; }

		[DataMember(Name ="term_vector")]
		TermVectorOption? TermVector { get; set; }
	}

	/// <summary>
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
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
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
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

		public GenericPropertyDescriptor<T> Type(string type) => Assign(a => TypeOverride = type);

		public GenericPropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Index = index);

		public GenericPropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);

		public GenericPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public GenericPropertyDescriptor<T> TermVector(TermVectorOption? termVector) => Assign(a => a.TermVector = termVector);

		public GenericPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public GenericPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public GenericPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public GenericPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(a => a.Norms = enabled);

		public GenericPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		public GenericPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) =>
			Assign(a => a.PositionIncrementGap = positionIncrementGap);

		public GenericPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(a => a.Fielddata = selector?.Invoke(new StringFielddataDescriptor()));
	}
}
