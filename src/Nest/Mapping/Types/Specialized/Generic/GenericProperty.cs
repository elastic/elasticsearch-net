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

		public GenericProperty() : base(FieldType.Object) => TypeOverride = null;

		public string Analyzer { get; set; }
		public double? Boost { get; set; }
		public IStringFielddata Fielddata { get; set; }
		public int? IgnoreAbove { get; set; }

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
		private FieldIndexOption? _index;

		public GenericPropertyDescriptor() : base(FieldType.Object) => TypeOverride = null;

		string IGenericProperty.Analyzer { get; set; }
		double? IGenericProperty.Boost { get; set; }
		IStringFielddata IGenericProperty.Fielddata { get; set; }
		int? IGenericProperty.IgnoreAbove { get; set; }

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

		public GenericPropertyDescriptor<T> Type(string type) => Assign(type, (a, v) => TypeOverride = v);

		[Obsolete("Please use the overload that accepts bool?. Will be fixed in NEST 7.x")]
		public GenericPropertyDescriptor<T> Index(FieldIndexOption? index = FieldIndexOption.NotAnalyzed) => Assign(index, (a, v) => a.Index = v);

		public GenericPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Indexed = v);

		public GenericPropertyDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public GenericPropertyDescriptor<T> NullValue(string nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		[Obsolete("Deprecated. Will be removed in NEST 7.x")]
		public GenericPropertyDescriptor<T> NotAnalyzed() => Index(FieldIndexOption.NotAnalyzed);

		public GenericPropertyDescriptor<T> TermVector(TermVectorOption? termVector) => Assign(termVector, (a, v) => a.TermVector = v);

		public GenericPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(indexOptions, (a, v) => a.IndexOptions = v);

		public GenericPropertyDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		public GenericPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(searchAnalyzer, (a, v) => a.SearchAnalyzer = v);

		public GenericPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(enabled, (a, v) => a.Norms = v);

		public GenericPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(ignoreAbove, (a, v) => a.IgnoreAbove = v);

		public GenericPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) =>
			Assign(positionIncrementGap, (a, v) => a.PositionIncrementGap = v);

		public GenericPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(selector, (a, v) => a.Fielddata = v?.Invoke(new StringFielddataDescriptor()));
	}
}
