// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A query that uses a query parser in order to parse its content
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(QueryStringQuery))]
	public interface IQueryStringQuery : IQuery
	{
		/// <summary>
		/// When set, <c>*</c> or <c>?</c> are allowed as the first character. Defaults to <c>true</c>.
		/// </summary>
		[DataMember(Name = "allow_leading_wildcard")]
		bool? AllowLeadingWildcard { get; set; }

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
		/// The default field for query terms if no prefix field is specified.
		/// Defaults to the index.query.default_field index settings, which in turn defaults to *.
		/// * extracts all fields in the mapping that are eligible to term queries and filters the metadata fields.
		/// All extracted fields are then combined to build a query when no prefix field is provided.
		/// </summary>
		[DataMember(Name = "default_field")]
		Field DefaultField { get; set; }

		/// <summary>
		/// The default operator used if no explicit operator is specified.
		/// The default operator is <see cref="Operator.Or" />
		/// </summary>
		[DataMember(Name = "default_operator")]
		Operator? DefaultOperator { get; set; }

		/// <summary>
		/// Set to <c>true<c> to enable position increments in result queries. Defaults to <c>true<c>.
		/// </summary>
		[DataMember(Name = "enable_position_increments")]
		bool? EnablePositionIncrements { get; set; }

		/// <summary>
		/// Enables escaping of the query
		/// </summary>
		[DataMember(Name = "escape")]
		bool? Escape { get; set; }

		/// <summary>
		/// The fields to perform the parsed query against.
		/// Defaults to the <c>index.query.default_field</c> index settings, which in turn defaults to <c>*</c>.
		/// <c>*</c> extracts all fields in the mapping that are eligible to term queries and filters the metadata fields.
		/// </summary>
		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// Set the fuzziness for fuzzy queries. Defaults to <see cref="Nest.Fuzziness.Auto" />
		/// </summary>
		[DataMember(Name = "fuzziness")]
		Fuzziness Fuzziness { get; set; }

		/// <summary>
		/// Controls the number of terms fuzzy queries will expand to. Defaults to <c>50</c>
		/// </summary>
		[DataMember(Name = "fuzzy_max_expansions")]
		int? FuzzyMaxExpansions { get; set; }

		/// <summary>
		/// Set the prefix length for fuzzy queries. Default is <c>0</c>.
		/// </summary>
		[DataMember(Name = "fuzzy_prefix_length")]
		int? FuzzyPrefixLength { get; set; }

		/// <summary>
		/// Controls how the query is rewritten if <see cref="Fuzziness" /> is set.
		/// In this scenario, the default is <see cref="MultiTermQueryRewrite.TopTermsBlendedFreqs" />.
		/// </summary>
		[DataMember(Name = "fuzzy_rewrite")]
		MultiTermQueryRewrite FuzzyRewrite { get; set; }

		/// <summary>
		/// Sets whether transpositions are supported in fuzzy queries.
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
		/// Limit on how many automaton states regexp queries are allowed to create.
		/// This protects against too-difficult (e.g. exponentially hard) regexps.
		/// Defaults to <c>10000</c>.
		/// </summary>
		[DataMember(Name = "max_determinized_states")]
		int? MaximumDeterminizedStates { get; set; }

		/// <summary>
		/// A value controlling how many "should" clauses in the resulting boolean query should match.
		/// It can be an absolute value, a percentage or a combination of both.
		/// </summary>
		[DataMember(Name = "minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// Sets the default slop for phrases. If zero, then exact phrase matches are required.
		/// Default value is <c>0</c>.
		/// </summary>
		[DataMember(Name = "phrase_slop")]
		double? PhraseSlop { get; set; }

		/// <summary>
		/// The query to be parsed
		/// </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }

		/// <summary>
		/// The name of the analyzer that is used to analyze quoted phrases in the query string.
		/// For those parts, it overrides other analyzers that are set using the analyzer parameter
		/// or the search_quote_analyzer setting.
		/// </summary>
		[DataMember(Name = "quote_analyzer")]
		string QuoteAnalyzer { get; set; }

		/// <summary>
		/// A suffix to append to fields for quoted parts of the query string.
		/// This allows to use a field that has a different analysis chain for exact matching.
		/// </summary>
		[DataMember(Name = "quote_field_suffix")]
		string QuoteFieldSuffix { get; set; }

		/// <summary>
		/// Controls how a multi term query such as a wildcard or prefix query, is rewritten.
		/// </summary>
		[DataMember(Name = "rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }

		/// <summary>
		/// The disjunction max tie breaker for multi fields. Defaults to <c>0</c>
		/// </summary>
		[DataMember(Name = "tie_breaker")]
		double? TieBreaker { get; set; }

		/// <summary>
		/// Time Zone to be applied to any range query related to dates.
		/// </summary>
		[DataMember(Name = "time_zone")]
		string TimeZone { get; set; }

		/// <summary>
		/// How the fields should be combined to build the text query.
		/// Default is <see cref="TextQueryType.BestFields" />
		/// </summary>
		[DataMember(Name = "type")]
		TextQueryType? Type { get; set; }
	}

	/// <inheritdoc cref="IQueryStringQuery" />
	[DataContract]
	public class QueryStringQuery : QueryBase, IQueryStringQuery
	{
		/// <inheritdoc />
		public bool? AllowLeadingWildcard { get; set; }

		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public bool? AnalyzeWildcard { get; set; }

		/// <inheritdoc />
		public bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <inheritdoc />
		public Field DefaultField { get; set; }

		/// <inheritdoc />
		public Operator? DefaultOperator { get; set; }

		/// <inheritdoc />
		public bool? EnablePositionIncrements { get; set; }

		/// <inheritdoc />
		public bool? Escape { get; set; }

		/// <inheritdoc />
		public Fields Fields { get; set; }

		/// <inheritdoc />
		public Fuzziness Fuzziness { get; set; }

		/// <inheritdoc />
		public int? FuzzyMaxExpansions { get; set; }

		/// <inheritdoc />
		public int? FuzzyPrefixLength { get; set; }

		/// <inheritdoc />
		public MultiTermQueryRewrite FuzzyRewrite { get; set; }

		/// <inheritdoc />
		public bool? FuzzyTranspositions { get; set; }

		/// <inheritdoc />
		public bool? Lenient { get; set; }

		/// <inheritdoc />
		public int? MaximumDeterminizedStates { get; set; }

		/// <inheritdoc />
		public MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <inheritdoc />
		public double? PhraseSlop { get; set; }

		/// <inheritdoc />
		public string Query { get; set; }

		/// <inheritdoc />
		public string QuoteAnalyzer { get; set; }

		/// <inheritdoc />
		public string QuoteFieldSuffix { get; set; }

		/// <inheritdoc />
		public MultiTermQueryRewrite Rewrite { get; set; }

		/// <inheritdoc />
		public double? TieBreaker { get; set; }

		/// <inheritdoc />
		public string TimeZone { get; set; }

		/// <inheritdoc />
		public TextQueryType? Type { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.QueryString = this;

		internal static bool IsConditionless(IQueryStringQuery q) => q.Query.IsNullOrEmpty();
	}

	/// <inheritdoc cref="IQueryStringQuery" />
	[DataContract]
	public class QueryStringQueryDescriptor<T>
		: QueryDescriptorBase<QueryStringQueryDescriptor<T>, IQueryStringQuery>
			, IQueryStringQuery where T : class
	{
		protected override bool Conditionless => QueryStringQuery.IsConditionless(this);
		bool? IQueryStringQuery.AllowLeadingWildcard { get; set; }
		string IQueryStringQuery.Analyzer { get; set; }
		bool? IQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? IQueryStringQuery.AutoGenerateSynonymsPhraseQuery { get; set; }
		Field IQueryStringQuery.DefaultField { get; set; }
		Operator? IQueryStringQuery.DefaultOperator { get; set; }
		bool? IQueryStringQuery.EnablePositionIncrements { get; set; }
		bool? IQueryStringQuery.Escape { get; set; }
		Fields IQueryStringQuery.Fields { get; set; }
		Fuzziness IQueryStringQuery.Fuzziness { get; set; }
		int? IQueryStringQuery.FuzzyMaxExpansions { get; set; }
		int? IQueryStringQuery.FuzzyPrefixLength { get; set; }
		MultiTermQueryRewrite IQueryStringQuery.FuzzyRewrite { get; set; }
		bool? IQueryStringQuery.FuzzyTranspositions { get; set; }
		bool? IQueryStringQuery.Lenient { get; set; }
		int? IQueryStringQuery.MaximumDeterminizedStates { get; set; }
		MinimumShouldMatch IQueryStringQuery.MinimumShouldMatch { get; set; }
		double? IQueryStringQuery.PhraseSlop { get; set; }
		string IQueryStringQuery.Query { get; set; }
		string IQueryStringQuery.QuoteAnalyzer { get; set; }
		string IQueryStringQuery.QuoteFieldSuffix { get; set; }
		MultiTermQueryRewrite IQueryStringQuery.Rewrite { get; set; }
		double? IQueryStringQuery.TieBreaker { get; set; }
		string IQueryStringQuery.TimeZone { get; set; }

		TextQueryType? IQueryStringQuery.Type { get; set; }

		/// <inheritdoc cref="IQueryStringQuery.DefaultField" />
		public QueryStringQueryDescriptor<T> DefaultField(Field field) => Assign(field, (a, v) => a.DefaultField = v);

		/// <inheritdoc cref="IQueryStringQuery.DefaultField" />
		public QueryStringQueryDescriptor<T> DefaultField<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.DefaultField = v);

		/// <inheritdoc cref="IQueryStringQuery.Fields" />
		public QueryStringQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="IQueryStringQuery.Fields" />
		public QueryStringQueryDescriptor<T> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		/// <inheritdoc cref="IQueryStringQuery.Type" />
		public QueryStringQueryDescriptor<T> Type(TextQueryType? type) => Assign(type, (a, v) => a.Type = v);

		/// <inheritdoc cref="IQueryStringQuery.Query" />
		public QueryStringQueryDescriptor<T> Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="IQueryStringQuery.DefaultOperator" />
		public QueryStringQueryDescriptor<T> DefaultOperator(Operator? op) => Assign(op, (a, v) => a.DefaultOperator = v);

		/// <inheritdoc cref="IQueryStringQuery.Analyzer" />
		public QueryStringQueryDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="IQueryStringQuery.QuoteAnalyzer" />
		public QueryStringQueryDescriptor<T> QuoteAnalyzer(string analyzer) => Assign(analyzer, (a, v) => a.QuoteAnalyzer = v);

		/// <inheritdoc cref="IQueryStringQuery.AllowLeadingWildcard" />
		public QueryStringQueryDescriptor<T> AllowLeadingWildcard(bool? allowLeadingWildcard = true) =>
			Assign(allowLeadingWildcard, (a, v) => a.AllowLeadingWildcard = v);

		/// <inheritdoc cref="IQueryStringQuery.Fuzziness" />
		public QueryStringQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);

		/// <inheritdoc cref="IQueryStringQuery.FuzzyPrefixLength" />
		public QueryStringQueryDescriptor<T> FuzzyPrefixLength(int? fuzzyPrefixLength) => Assign(fuzzyPrefixLength, (a, v) => a.FuzzyPrefixLength = v);

		/// <inheritdoc cref="IQueryStringQuery.FuzzyMaxExpansions" />
		public QueryStringQueryDescriptor<T> FuzzyMaxExpansions(int? fuzzyMaxExpansions) => Assign(fuzzyMaxExpansions, (a, v) => a.FuzzyMaxExpansions = v);

		/// <inheritdoc cref="IQueryStringQuery.FuzzyTranspositions" />
		public QueryStringQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranspositions = true) =>
			Assign(fuzzyTranspositions, (a, v) => a.FuzzyTranspositions = v);

		/// <inheritdoc cref="IQueryStringQuery.PhraseSlop" />
		public QueryStringQueryDescriptor<T> PhraseSlop(double? phraseSlop) => Assign(phraseSlop, (a, v) => a.PhraseSlop = v);

		/// <inheritdoc cref="IQueryStringQuery.MinimumShouldMatch" />
		public QueryStringQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(minimumShouldMatch, (a, v) => a.MinimumShouldMatch = v);

		/// <inheritdoc cref="IQueryStringQuery.Lenient" />
		public QueryStringQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(lenient, (a, v) => a.Lenient = v);

		/// <inheritdoc cref="IQueryStringQuery.AnalyzeWildcard" />
		public QueryStringQueryDescriptor<T> AnalyzeWildcard(bool? analyzeWildcard = true) => Assign(analyzeWildcard, (a, v) => a.AnalyzeWildcard = v);

		/// <inheritdoc cref="IQueryStringQuery.TieBreaker" />
		public QueryStringQueryDescriptor<T> TieBreaker(double? tieBreaker) => Assign(tieBreaker, (a, v) => a.TieBreaker = v);

		/// <inheritdoc cref="IQueryStringQuery.MaximumDeterminizedStates" />
		public QueryStringQueryDescriptor<T> MaximumDeterminizedStates(int? maxDeterminizedStates) =>
			Assign(maxDeterminizedStates, (a, v) => a.MaximumDeterminizedStates = v);

		/// <inheritdoc cref="IQueryStringQuery.FuzzyRewrite" />
		public QueryStringQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.FuzzyRewrite = v);

		/// <inheritdoc cref="IQueryStringQuery.Rewrite" />
		public QueryStringQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.Rewrite = v);

		/// <inheritdoc cref="IQueryStringQuery.QuoteFieldSuffix" />
		public QueryStringQueryDescriptor<T> QuoteFieldSuffix(string quoteFieldSuffix) =>
			Assign(quoteFieldSuffix, (a, v) => a.QuoteFieldSuffix = v);

		/// <inheritdoc cref="IQueryStringQuery.Escape" />
		public QueryStringQueryDescriptor<T> Escape(bool? escape = true) => Assign(escape, (a, v) => a.Escape = v);

		/// <inheritdoc cref="IQueryStringQuery.EnablePositionIncrements" />
		public QueryStringQueryDescriptor<T> EnablePositionIncrements(bool? enablePositionIncrements = true) =>
			Assign(enablePositionIncrements, (a, v) => a.EnablePositionIncrements = v);

		/// <inheritdoc cref="IQueryStringQuery.TimeZone" />
		public QueryStringQueryDescriptor<T> TimeZone(string timezone) => Assign(timezone, (a, v) => a.TimeZone = v);

		/// <inheritdoc cref="IQueryStringQuery.AutoGenerateSynonymsPhraseQuery" />
		public QueryStringQueryDescriptor<T> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true) =>
			Assign(autoGenerateSynonymsPhraseQuery, (a, v) => a.AutoGenerateSynonymsPhraseQuery = v);
	}
}
