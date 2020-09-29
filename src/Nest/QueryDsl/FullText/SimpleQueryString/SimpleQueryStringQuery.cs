// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A query that uses the SimpleQueryParser to parse its context.
	/// Unlike the regular <see cref="IQueryStringQuery" />, the <see cref="ISimpleQueryStringQuery" /> query will
	/// never throw an exception, and discards invalid parts of the query.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(SimpleQueryStringQueryDescriptor<object>))]
	public interface ISimpleQueryStringQuery : IQuery
	{
		/// <summary>
		/// The analyzer name used to analyze the query
		/// </summary>
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// By default, wildcards terms in a query are not analyzed.
		/// By setting this value to <c>true</c>, a best effort will be made to analyze those as well.
		/// </summary>
		[DataMember(Name = "analyze_wildcard")]
		bool? AnalyzeWildcard { get; set; }

		/// <summary></summary>
		[DataMember(Name = "auto_generate_synonyms_phrase_query")]
		bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <summary>
		/// The default operator used if no explicit operator is specified
		/// The default operator is <see cref="Operator.Or" />
		/// </summary>
		[DataMember(Name = "default_operator")]
		Operator? DefaultOperator { get; set; }

		/// <summary>
		/// The fields to perform the parsed query against.
		/// Defaults to the <c>index.query.default_field</c> index settings, which in turn defaults to <c>*</c>.
		/// <c>*</c> extracts all fields in the mapping that are eligible to term queries and filters the metadata fields.
		/// </summary>
		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// Flags specifying which features to enable.
		/// Defaults to <see cref="SimpleQueryStringFlags.All" />.
		/// </summary>
		[DataMember(Name = "flags")]
		SimpleQueryStringFlags? Flags { get; set; }

		/// <summary>
		/// Controls the number of terms fuzzy queries will expand to. Defaults to <c>50</c>
		/// </summary>
		[DataMember(Name = "fuzzy_max_expansions")]
		int? FuzzyMaxExpansions { get; set; }

		/// <summary>
		/// Set the prefix length for fuzzy queries. Default is <c>0</c>
		/// </summary>
		[DataMember(Name = "fuzzy_prefix_length")]
		int? FuzzyPrefixLength { get; set; }

		/// <summary>
		/// Sets whether transpositions are supported in fuzzy queries. Default is <c>true</c>.
		/// <para />
		/// The default metric used by fuzzy queries to determine a match is the Damerau-Levenshtein
		/// distance formula which supports transpositions. Setting transposition to false will
		/// switch to classic Levenshtein distance.
		/// If not set, Damerau-Levenshtein distance metric will be used.
		/// </summary>
		[DataMember(Name = "fuzzy_transpositions")]
		bool? FuzzyTranspositions { get; set; }

		/// <summary>
		/// If set to <c>true</c> will cause format based failures (like providing text to a numeric field)
		/// to be ignored
		/// </summary>
		[DataMember(Name = "lenient")]
		bool? Lenient { get; set; }

		/// <summary>
		/// A value controlling how many "should" clauses in the resulting boolean query should match.
		/// It can be an absolute value, a percentage or a combination of both
		/// </summary>
		[DataMember(Name = "minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// The query to be parsed
		/// </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }

		/// <summary>
		/// A suffix to append to fields for quoted parts of the query string.
		/// This allows to use a field that has a different analysis chain for exact matching.
		/// </summary>
		[DataMember(Name = "quote_field_suffix")]
		string QuoteFieldSuffix { get; set; }
	}

	/// <inheritdoc cref="ISimpleQueryStringQuery" />
	public class SimpleQueryStringQuery : QueryBase, ISimpleQueryStringQuery
	{
		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public bool? AnalyzeWildcard { get; set; }

		/// <inheritdoc />
		public bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <inheritdoc />
		public Operator? DefaultOperator { get; set; }

		/// <inheritdoc />
		public Fields Fields { get; set; }

		/// <inheritdoc />
		public SimpleQueryStringFlags? Flags { get; set; }

		/// <inheritdoc />
		public int? FuzzyMaxExpansions { get; set; }

		/// <inheritdoc />
		public int? FuzzyPrefixLength { get; set; }

		/// <inheritdoc />
		public bool? FuzzyTranspositions { get; set; }

		/// <inheritdoc />
		public bool? Lenient { get; set; }

		/// <inheritdoc />
		public MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <inheritdoc />
		public string Query { get; set; }

		/// <inheritdoc />
		public string QuoteFieldSuffix { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SimpleQueryString = this;

		internal static bool IsConditionless(ISimpleQueryStringQuery q) => q.Query.IsNullOrEmpty();
	}

	/// <inheritdoc cref="ISimpleQueryStringQuery" />
	public class SimpleQueryStringQueryDescriptor<T>
		: QueryDescriptorBase<SimpleQueryStringQueryDescriptor<T>, ISimpleQueryStringQuery>
			, ISimpleQueryStringQuery where T : class
	{
		protected override bool Conditionless => SimpleQueryStringQuery.IsConditionless(this);
		string ISimpleQueryStringQuery.Analyzer { get; set; }
		bool? ISimpleQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? ISimpleQueryStringQuery.AutoGenerateSynonymsPhraseQuery { get; set; }
		Operator? ISimpleQueryStringQuery.DefaultOperator { get; set; }
		Fields ISimpleQueryStringQuery.Fields { get; set; }
		SimpleQueryStringFlags? ISimpleQueryStringQuery.Flags { get; set; }
		int? ISimpleQueryStringQuery.FuzzyMaxExpansions { get; set; }
		int? ISimpleQueryStringQuery.FuzzyPrefixLength { get; set; }
		bool? ISimpleQueryStringQuery.FuzzyTranspositions { get; set; }
		bool? ISimpleQueryStringQuery.Lenient { get; set; }
		MinimumShouldMatch ISimpleQueryStringQuery.MinimumShouldMatch { get; set; }
		string ISimpleQueryStringQuery.Query { get; set; }
		string ISimpleQueryStringQuery.QuoteFieldSuffix { get; set; }

		/// <inheritdoc cref="ISimpleQueryStringQuery.Fields" />
		public SimpleQueryStringQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Fields" />
		public SimpleQueryStringQueryDescriptor<T> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Query" />
		public SimpleQueryStringQueryDescriptor<T> Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Analyzer" />
		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.DefaultOperator" />
		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator? op) => Assign(op, (a, v) => a.DefaultOperator = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Flags" />
		public SimpleQueryStringQueryDescriptor<T> Flags(SimpleQueryStringFlags? flags) => Assign(flags, (a, v) => a.Flags = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.AnalyzeWildcard" />
		public SimpleQueryStringQueryDescriptor<T> AnalyzeWildcard(bool? analyzeWildcard = true) =>
			Assign(analyzeWildcard, (a, v) => a.AnalyzeWildcard = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Lenient" />
		public SimpleQueryStringQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(lenient, (a, v) => a.Lenient = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.MinimumShouldMatch" />
		public SimpleQueryStringQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(minimumShouldMatch, (a, v) => a.MinimumShouldMatch = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.QuoteFieldSuffix" />
		public SimpleQueryStringQueryDescriptor<T> QuoteFieldSuffix(string quoteFieldSuffix) =>
			Assign(quoteFieldSuffix, (a, v) => a.QuoteFieldSuffix = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.FuzzyPrefixLength" />
		public SimpleQueryStringQueryDescriptor<T> FuzzyPrefixLength(int? fuzzyPrefixLength) => Assign(fuzzyPrefixLength, (a, v) => a.FuzzyPrefixLength = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.FuzzyMaxExpansions" />
		public SimpleQueryStringQueryDescriptor<T> FuzzyMaxExpansions(int? fuzzyMaxExpansions) =>
			Assign(fuzzyMaxExpansions, (a, v) => a.FuzzyMaxExpansions = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.FuzzyTranspositions" />
		public SimpleQueryStringQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranspositions = true) =>
			Assign(fuzzyTranspositions, (a, v) => a.FuzzyTranspositions = v);

		/// <inheritdoc cref="ISimpleQueryStringQuery.AutoGenerateSynonymsPhraseQuery" />
		public SimpleQueryStringQueryDescriptor<T> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true) =>
			Assign(autoGenerateSynonymsPhraseQuery, (a, v) => a.AutoGenerateSynonymsPhraseQuery = v);
	}
}
