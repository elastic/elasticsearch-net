using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
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

		[JsonProperty("include_in_all")]
		/// <remarks>Removed in 6.x</remarks>
		bool? IncludeInAll { get; set; }

		[JsonIgnore]
		[Obsolete("Please use Indexed. Will be fixed in NEST 7.x")]
		FieldIndexOption? Index { get; set; }

		[JsonProperty("index")]
		bool? Indexed { get; set; }

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
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
	/// </summary>
	[DebuggerDisplay("{DebugDisplay}")]
	public class GenericProperty : DocValuesPropertyBase, IGenericProperty
	{
		private FieldIndexOption? _index;

#pragma warning disable 618
		public GenericProperty() : base(null) { }
#pragma warning restore 618
		public string Analyzer { get; set; }
		public double? Boost { get; set; }
		public IStringFielddata Fielddata { get; set; }
		public int? IgnoreAbove { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		public bool? IncludeInAll { get; set; }

		[Obsolete("Please use Indexed. Will be fixed in NEST 7.x")]
		public FieldIndexOption? Index
		{
			get => _index;
			set
			{
				_index = value;
				switch (_index)
				{
					case FieldIndexOption.Analyzed:
					case FieldIndexOption.NotAnalyzed:
						Indexed = true;
						break;
					case FieldIndexOption.No:
						Indexed = false;
						break;
					default:
						Indexed = null;
						break;
				}
			}
		}

		public bool? Indexed { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public bool? Norms { get; set; }
		public string NullValue { get; set; }
		public int? PositionIncrementGap { get; set; }
		public string SearchAnalyzer { get; set; }

		public TermVectorOption? TermVector { get; set; }
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
		private FieldIndexOption? _index;

#pragma warning disable 618
		public GenericPropertyDescriptor() : base(null) { }
#pragma warning restore 618
		string IGenericProperty.Analyzer { get; set; }
		double? IGenericProperty.Boost { get; set; }
		IStringFielddata IGenericProperty.Fielddata { get; set; }
		int? IGenericProperty.IgnoreAbove { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		bool? IGenericProperty.IncludeInAll { get; set; }

		FieldIndexOption? IGenericProperty.Index
		{
			get => _index;
			set
			{
				_index = value;
				switch (_index)
				{
					case FieldIndexOption.Analyzed:
					case FieldIndexOption.NotAnalyzed:
						Self.Indexed = true;
						break;
					case FieldIndexOption.No:
						Self.Indexed = false;
						break;
					default:
						Self.Indexed = null;
						break;
				}
			}
		}

		bool? IGenericProperty.Indexed { get; set; }
		IndexOptions? IGenericProperty.IndexOptions { get; set; }
		bool? IGenericProperty.Norms { get; set; }
		string IGenericProperty.NullValue { get; set; }
		int? IGenericProperty.PositionIncrementGap { get; set; }
		string IGenericProperty.SearchAnalyzer { get; set; }
		TermVectorOption? IGenericProperty.TermVector { get; set; }

		public GenericPropertyDescriptor<T> Type(string type) => Assign(a => a.Type = type);

		[Obsolete("Please use the overload that accepts bool?. Will be fixed in NEST 7.x")]
		public GenericPropertyDescriptor<T> Index(FieldIndexOption? index = FieldIndexOption.NotAnalyzed) => Assign(a => a.Index = index);

		public GenericPropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Indexed = index);

		public GenericPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public GenericPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		/// <remarks>Removed in 6.x</remarks>
		public GenericPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);

		[Obsolete("Deprecated. Will be removed in NEST 7.x")]
		public GenericPropertyDescriptor<T> NotAnalyzed() => Index(FieldIndexOption.NotAnalyzed);

		[Obsolete("Please use the overload that accepts bool?. Will be fixed in NEST 7.x")]
		public GenericPropertyDescriptor<T> Index(FieldIndexOption index) => Assign(a => a.Index = index);

		public GenericPropertyDescriptor<T> TermVector(TermVectorOption termVector) => Assign(a => a.TermVector = termVector);

		public GenericPropertyDescriptor<T> IndexOptions(IndexOptions indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public GenericPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public GenericPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public GenericPropertyDescriptor<T> Norms(bool enabled = true) => Assign(a => a.Norms = enabled);

		public GenericPropertyDescriptor<T> IgnoreAbove(int ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		public GenericPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) =>
			Assign(a => a.PositionIncrementGap = positionIncrementGap);

		public GenericPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(a => a.Fielddata = selector?.Invoke(new StringFielddataDescriptor()));
	}
}
