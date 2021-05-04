// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A field to index full-text values, such as the body of an email or the description of a product.
	/// These fields are analyzed in Elasticsearch, by passing through an analyzer to convert the string
	/// into a list of individual terms before being indexed.
	/// <para />
	/// Text fields are not used for sorting and seldom used for aggregations
	/// </summary>
	[InterfaceDataContract]
	public interface ITextProperty : ICoreProperty
	{
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		[DataMember(Name = "eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		[DataMember(Name = "fielddata")]
		bool? Fielddata { get; set; }

		[DataMember(Name = "fielddata_frequency_filter")]
		IFielddataFrequencyFilter FielddataFrequencyFilter { get; set; }

		[DataMember(Name = "index")]
		bool? Index { get; set; }

		[DataMember(Name = "index_options")]
		IndexOptions? IndexOptions { get; set; }

		[DataMember(Name = "index_phrases")]
		bool? IndexPhrases { get; set; }

		[DataMember(Name = "index_prefixes")]
		ITextIndexPrefixes IndexPrefixes { get; set; }

		[DataMember(Name = "norms")]
		bool? Norms { get; set; }

		[DataMember(Name = "position_increment_gap")]
		int? PositionIncrementGap { get; set; }

		[DataMember(Name = "search_analyzer")]
		string SearchAnalyzer { get; set; }

		[DataMember(Name = "search_quote_analyzer")]
		string SearchQuoteAnalyzer { get; set; }

		[DataMember(Name = "term_vector")]
		TermVectorOption? TermVector { get; set; }
	}

	/// <inheritdoc cref="ITextProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class TextProperty : CorePropertyBase, ITextProperty
	{
		public TextProperty() : base(FieldType.Text) { }
		public string Analyzer { get; set; }
		public bool? EagerGlobalOrdinals { get; set; }
		public bool? Fielddata { get; set; }
		public IFielddataFrequencyFilter FielddataFrequencyFilter { get; set; }
		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public bool? IndexPhrases { get; set; }
		public ITextIndexPrefixes IndexPrefixes { get; set; }
		public bool? Norms { get; set; }
		public int? PositionIncrementGap { get; set; }
		public string SearchAnalyzer { get; set; }
		public string SearchQuoteAnalyzer { get; set; }
		public TermVectorOption? TermVector { get; set; }
	}

	/// <inheritdoc cref="ITextProperty"/>
	public class TextPropertyDescriptor<T>
		: CorePropertyDescriptorBase<TextPropertyDescriptor<T>, ITextProperty, T>, ITextProperty
		where T : class
	{
		public TextPropertyDescriptor() : base(FieldType.Text) { }

		string ITextProperty.Analyzer { get; set; }
		bool? ITextProperty.EagerGlobalOrdinals { get; set; }
		bool? ITextProperty.Fielddata { get; set; }
		IFielddataFrequencyFilter ITextProperty.FielddataFrequencyFilter { get; set; }
		bool? ITextProperty.Index { get; set; }
		IndexOptions? ITextProperty.IndexOptions { get; set; }
		bool? ITextProperty.IndexPhrases { get; set; }
		ITextIndexPrefixes ITextProperty.IndexPrefixes { get; set; }
		bool? ITextProperty.Norms { get; set; }
		int? ITextProperty.PositionIncrementGap { get; set; }
		string ITextProperty.SearchAnalyzer { get; set; }
		string ITextProperty.SearchQuoteAnalyzer { get; set; }
		TermVectorOption? ITextProperty.TermVector { get; set; }

		public TextPropertyDescriptor<T> EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true) =>
			Assign(eagerGlobalOrdinals, (a, v) => a.EagerGlobalOrdinals = v);

		public TextPropertyDescriptor<T> Fielddata(bool? fielddata = true) => Assign(fielddata, (a, v) => a.Fielddata = v);

		public TextPropertyDescriptor<T> FielddataFrequencyFilter(Func<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter> selector) =>
			Assign(selector, (a, v) => a.FielddataFrequencyFilter = v?.Invoke(new FielddataFrequencyFilterDescriptor()));

		public TextPropertyDescriptor<T> IndexPrefixes(Func<TextIndexPrefixesDescriptor, ITextIndexPrefixes> selector) =>
			Assign(selector, (a, v) => a.IndexPrefixes = v?.Invoke(new TextIndexPrefixesDescriptor()));

		public TextPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		public TextPropertyDescriptor<T> IndexPhrases(bool? indexPhrases = true) => Assign(indexPhrases, (a, v) => a.IndexPhrases = v);

		public TextPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(indexOptions, (a, v) => a.IndexOptions = v);

		public TextPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(enabled, (a, v) => a.Norms = v);

		public TextPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) =>
			Assign(positionIncrementGap, (a, v) => a.PositionIncrementGap = v);

		public TextPropertyDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		public TextPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(searchAnalyzer, (a, v) => a.SearchAnalyzer = v);

		public TextPropertyDescriptor<T> SearchQuoteAnalyzer(string searchQuoteAnalyzer) => Assign(searchQuoteAnalyzer, (a, v) => a.SearchQuoteAnalyzer = v);

		public TextPropertyDescriptor<T> TermVector(TermVectorOption? termVector) => Assign(termVector, (a, v) => a.TermVector = v);
	}
}
