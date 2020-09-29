// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A text-like field that is optimized to provide out-of-the-box support for the "search as you type" completion use case.
	/// <para></para>
	/// It creates a series of subfields that are analyzed to index terms that can be efficiently matched by a query that
	/// partially matches the entire indexed text value. Both prefix completion (i.e matching terms starting at the beginning of the input)
	/// and infix completion (i.e. matching terms at any position within the input) are supported.
	/// </summary>
	[InterfaceDataContract]
	public interface ISearchAsYouTypeProperty : ICoreProperty
	{
		/// <summary>
		/// The analyzer which should be used for analyzed string fields, both at index-time and at
		/// search-time (unless overridden by the search_analyzer).
		/// Defaults to the default index analyzer, or the standard analyzer.
		/// </summary>
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// Should the field be searchable? Accepts <c>true</c> (default) or <c>false</c>.
		/// </summary>
		[DataMember(Name = "index")]
		bool? Index { get; set; }

		/// <summary>
		/// What information should be stored in the index, for search and highlighting purposes. Defaults to <see cref="Nest.IndexOptions.Positions"/>.
		/// </summary>
		[DataMember(Name = "index_options")]
		IndexOptions? IndexOptions { get; set; }

		/// <summary>
		/// The largest shingle size to index the input with and create subfields for.
		/// </summary>
		[DataMember(Name = "max_shingle_size")]
		int? MaxShingleSize { get; set; }

		/// <summary>
		/// Whether field-length should be taken into account when scoring queries. Accepts true or false. This option configures the root field
		/// and shingle subfields, where its default is true.
		/// It does not configure the prefix subfield, where it it false.
		/// </summary>
		[DataMember(Name = "norms")]
		bool? Norms { get; set; }

		/// <summary>
		/// The analyzer that should be used at search time on analyzed fields. Defaults to the analyzer setting.
		/// </summary>
		[DataMember(Name = "search_analyzer")]
		string SearchAnalyzer { get; set; }

		/// <summary>
		/// The analyzer that should be used at search time when a phrase is encountered. Defaults to the search_analyzer setting.
		/// </summary>
		[DataMember(Name = "search_quote_analyzer")]
		string SearchQuoteAnalyzer { get; set; }

		/// <summary>
		/// Whether term vectors should be stored for an analyzed field. Defaults to no. This option
		/// configures the root field and shingle subfields, but not the prefix subfield.
		/// </summary>
		[DataMember(Name = "term_vector")]
		TermVectorOption? TermVector { get; set; }
	}

	/// <inheritdoc cref="ISearchAsYouTypeProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class SearchAsYouTypeProperty : CorePropertyBase, ISearchAsYouTypeProperty
	{
		public SearchAsYouTypeProperty() : base(FieldType.SearchAsYouType) { }

		/// <inheritdoc />
		public string Analyzer { get; set; }
		/// <inheritdoc />
		public bool? Index { get; set; }
		/// <inheritdoc />
		public IndexOptions? IndexOptions { get; set; }
		/// <inheritdoc />
		public int? MaxShingleSize { get; set; }
		/// <inheritdoc />
		public bool? Norms { get; set; }
		/// <inheritdoc />
		public string SearchAnalyzer { get; set; }
		/// <inheritdoc />
		public string SearchQuoteAnalyzer { get; set; }
		/// <inheritdoc />
		public TermVectorOption? TermVector { get; set; }
	}

	/// <inheritdoc cref="ISearchAsYouTypeProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class SearchAsYouTypePropertyDescriptor<T>
		: CorePropertyDescriptorBase<SearchAsYouTypePropertyDescriptor<T>, ISearchAsYouTypeProperty, T>, ISearchAsYouTypeProperty
		where T : class
	{
		public SearchAsYouTypePropertyDescriptor() : base(FieldType.SearchAsYouType) { }

		string ISearchAsYouTypeProperty.Analyzer { get; set; }
		bool? ISearchAsYouTypeProperty.Index { get; set; }
		IndexOptions? ISearchAsYouTypeProperty.IndexOptions { get; set; }
		int? ISearchAsYouTypeProperty.MaxShingleSize { get; set; }
		bool? ISearchAsYouTypeProperty.Norms { get; set; }
		string ISearchAsYouTypeProperty.SearchAnalyzer { get; set; }
		string ISearchAsYouTypeProperty.SearchQuoteAnalyzer { get; set; }
		TermVectorOption? ISearchAsYouTypeProperty.TermVector { get; set; }

		/// <inheritdoc cref="ISearchAsYouTypeProperty.Analyzer" />
		public SearchAsYouTypePropertyDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="ISearchAsYouTypeProperty.Index" />
		public SearchAsYouTypePropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		/// <inheritdoc cref="ISearchAsYouTypeProperty.IndexOptions" />
		public SearchAsYouTypePropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(indexOptions, (a, v) => a.IndexOptions = v);

		/// <inheritdoc cref="ISearchAsYouTypeProperty.MaxShingleSize" />
		public SearchAsYouTypePropertyDescriptor<T> MaxShingleSize(int? maxShingleSize) => Assign(maxShingleSize, (a, v) => a.MaxShingleSize = v);

		/// <inheritdoc cref="ISearchAsYouTypeProperty.Norms" />
		public SearchAsYouTypePropertyDescriptor<T> Norms(bool? enabled = true) => Assign(enabled, (a, v) => a.Norms = v);

		/// <inheritdoc cref="ISearchAsYouTypeProperty.SearchAnalyzer" />
		public SearchAsYouTypePropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(searchAnalyzer, (a, v) => a.SearchAnalyzer = v);

		/// <inheritdoc cref="ISearchAsYouTypeProperty.SearchQuoteAnalyzer" />
		public SearchAsYouTypePropertyDescriptor<T> SearchQuoteAnalyzer(string searchQuoteAnalyzer) =>
			Assign(searchQuoteAnalyzer, (a, v) => a.SearchQuoteAnalyzer = v);

		/// <inheritdoc cref="ISearchAsYouTypeProperty.TermVector" />
		public SearchAsYouTypePropertyDescriptor<T> TermVector(TermVectorOption? termVector) => Assign(termVector, (a, v) => a.TermVector = v);
	}
}
