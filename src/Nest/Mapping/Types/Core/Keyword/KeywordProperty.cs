using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IKeywordProperty : IDocValuesProperty
	{
		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		[DataMember(Name ="ignore_above")]
		int? IgnoreAbove { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="index_options")]
		IndexOptions? IndexOptions { get; set; }

		[DataMember(Name ="normalizer")]
		string Normalizer { get; set; }

		[DataMember(Name ="norms")]
		bool? Norms { get; set; }

		[DataMember(Name ="null_value")]
		string NullValue { get; set; }

		/// <summary> Whether full text queries should split the input on whitespace when building a query for this field. </summary>
		[DataMember(Name ="split_queries_on_whitespace")]
		bool? SplitQueriesOnWhitespace { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class KeywordProperty : DocValuesPropertyBase, IKeywordProperty
	{
		public KeywordProperty() : base(FieldType.Keyword) { }

		public double? Boost { get; set; }
		public bool? EagerGlobalOrdinals { get; set; }
		public int? IgnoreAbove { get; set; }
		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public string Normalizer { get; set; }
		public bool? Norms { get; set; }
		public string NullValue { get; set; }

		/// <inheritdoc cref="IKeywordProperty.SplitQueriesOnWhitespace" />
		public bool? SplitQueriesOnWhitespace { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class KeywordPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<KeywordPropertyDescriptor<T>, IKeywordProperty, T>, IKeywordProperty
		where T : class
	{
		public KeywordPropertyDescriptor() : base(FieldType.Keyword) { }

		double? IKeywordProperty.Boost { get; set; }
		bool? IKeywordProperty.EagerGlobalOrdinals { get; set; }
		int? IKeywordProperty.IgnoreAbove { get; set; }
		bool? IKeywordProperty.Index { get; set; }
		IndexOptions? IKeywordProperty.IndexOptions { get; set; }
		string IKeywordProperty.Normalizer { get; set; }
		bool? IKeywordProperty.Norms { get; set; }
		string IKeywordProperty.NullValue { get; set; }
		bool? IKeywordProperty.SplitQueriesOnWhitespace { get; set; }

		public KeywordPropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);

		public KeywordPropertyDescriptor<T> EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true) =>
			Assign(a => a.EagerGlobalOrdinals = eagerGlobalOrdinals);

		public KeywordPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		public KeywordPropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Index = index);

		public KeywordPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public KeywordPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(a => a.Norms = enabled);

		/// <inheritdoc cref="IKeywordProperty.SplitQueriesOnWhitespace" />
		public KeywordPropertyDescriptor<T> SplitQueriesOnWhitespace(bool? split = true) => Assign(a => a.SplitQueriesOnWhitespace = split);

		public KeywordPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public KeywordPropertyDescriptor<T> Normalizer(string normalizer) => Assign(a => a.Normalizer = normalizer);
	}
}
