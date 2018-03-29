using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A query that uses a query parser in order to parse its content
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<QueryStringQueryDescriptor<object>>))]
	public interface IQueryStringQuery : IQuery
	{
		/// <summary>
		/// How the fields should be combined to build the text query.
		/// Default is <see cref="TextQueryType.BestFields"/>
		/// </summary>
		[JsonProperty("type")]
		TextQueryType? Type { get; set; }

		/// <summary>
		/// The query to be parsed
		/// </summary>
		[JsonProperty("query")]
		string Query { get; set; }

		/// <summary>
		/// The default field for query terms if no prefix field is specified.
		/// Defaults to the index.query.default_field index settings, which in turn defaults to *.
		/// * extracts all fields in the mapping that are eligible to term queries and filters the metadata fields.
		/// All extracted fields are then combined to build a query when no prefix field is provided.
		/// </summary>
		[JsonProperty("default_field")]
		Field DefaultField { get; set; }

		/// <summary>
		/// The default operator used if no explicit operator is specified.
		/// The default operator is <see cref="Operator.Or"/>
		/// </summary>
		[JsonProperty("default_operator")]
		Operator? DefaultOperator { get; set; }

		/// <summary>
		/// The analyzer name used to analyze the query
		/// </summary>
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// The name of the analyzer that is used to analyze quoted phrases in the query string.
		/// For those parts, it overrides other analyzers that are set using the analyzer parameter
		/// or the search_quote_analyzer setting.
		/// </summary>
		[JsonProperty("quote_analyzer")]
		string QuoteAnalyzer { get; set; }

		/// <summary>
		/// When set, <c>*</c> or <c>?</c> are allowed as the first character. Defaults to <c>true</c>.
		/// </summary>
		[JsonProperty("allow_leading_wildcard")]
		bool? AllowLeadingWildcard { get; set; }

		/// <summary>
		/// Controls the number of terms fuzzy queries will expand to. Defaults to <c>50</c>
		/// </summary>
		[JsonProperty("fuzzy_max_expansions")]
		int? FuzzyMaxExpansions { get; set; }

		/// <summary>
		/// Set the fuzziness for fuzzy queries. Defaults to <see cref="Fuzziness.Auto"/>
		/// </summary>
		[JsonProperty("fuzziness")]
		Fuzziness Fuzziness { get; set; }

		/// <summary>
		/// Set the prefix length for fuzzy queries. Default is <c>0</c>.
		/// </summary>
		[JsonProperty("fuzzy_prefix_length")]
		int? FuzzyPrefixLength { get; set; }

		/// <summary>
		/// Sets whether transpositions are supported in fuzzy queries.
		/// <para />
		/// The default metric used by fuzzy queries to determine a match is the Damerau-Levenshtein
		/// distance formula which supports transpositions. Setting transposition to false will
		/// switch to classic Levenshtein distance.
		/// If not set, Damerau-Levenshtein distance metric will be used.
		/// </summary>
		[JsonProperty("fuzzy_transpositions")]
		bool? FuzzyTranspositions { get; set; }

		/// <summary>
		/// Sets the default slop for phrases. If zero, then exact phrase matches are required.
		/// Default value is <c>0</c>.
		/// </summary>
		[JsonProperty("phrase_slop")]
		double? PhraseSlop { get; set; }

		/// <summary>
		/// By default, wildcards terms in a query are not analyzed.
		/// By setting this value to <c>true</c>, a best effort will be made to analyze those as well.
		/// </summary>
		[JsonProperty("analyze_wildcard")]
		bool? AnalyzeWildcard { get; set; }

		/// <summary>
		/// Limit on how many automaton states regexp queries are allowed to create.
		/// This protects against too-difficult (e.g. exponentially hard) regexps.
		/// Defaults to <c>10000</c>.
		/// </summary>
		[JsonProperty("max_determinized_states")]
		int? MaximumDeterminizedStates { get; set; }

		/// <summary>
		/// A value controlling how many "should" clauses in the resulting boolean query should match.
		/// It can be an absolute value, a percentage or a combination of both.
		/// </summary>
		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// If set to <c>true</c> will cause format based failures (like providing text to a numeric field)
		/// to be ignored
		/// </summary>
		[JsonProperty("lenient")]
		bool? Lenient { get; set; }

		/// <summary>
		/// The fields to perform the parsed query against.
		/// Defaults to the <c>index.query.default_field</c> index settings, which in turn defaults to <c>*</c>.
		/// <c>*</c> extracts all fields in the mapping that are eligible to term queries and filters the metadata fields.
		/// </summary>
		[JsonProperty("fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// The disjunction max tie breaker for multi fields. Defaults to <c>0</c>
		/// </summary>
		[JsonProperty("tie_breaker")]
		double? TieBreaker { get; set; }

		/// <summary>
		/// Controls how a multi term query such as a wildcard or prefix query, is rewritten.
		/// </summary>
		[JsonProperty("rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }

		/// <summary>
		/// Controls how the query is rewritten if <see cref="Fuzziness"/> is set.
		/// In this scenario, the default is <see cref="MultiTermQueryRewrite.TopTermsBlendedFreqs"/>.
		/// </summary>
		[JsonProperty("fuzzy_rewrite")]
		MultiTermQueryRewrite FuzzyRewrite { get; set; }

		/// <summary>
		/// A suffix to append to fields for quoted parts of the query string.
		/// This allows to use a field that has a different analysis chain for exact matching.
		/// </summary>
		[JsonProperty("quote_field_suffix")]
		string QuoteFieldSuffix { get; set; }

		/// <summary>
		/// Enables escaping of the query
		/// </summary>
		[JsonProperty("escape")]
		bool? Escape { get; set; }

		/// <summary>
		/// Time Zone to be applied to any range query related to dates.
		/// </summary>
		[JsonProperty("time_zone")]
		string Timezone { get; set; }

		/// <summary>
		/// Set to <c>true<c> to enable position increments in result queries. Defaults to <c>true<c>.
		/// </summary>
		[JsonProperty("enable_position_increments")]
		bool? EnablePositionIncrements { get; set; }

    /// <summary></summary>
		[JsonProperty("auto_generate_synonyms_phrase_query")]
		bool? AutoGenerateSynonymsPhraseQuery { get; set; }
	}

	/// <inheritdoc cref="IQueryStringQuery"/>
	public class QueryStringQuery : QueryBase, IQueryStringQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		/// <inheritdoc />
		public TextQueryType? Type { get; set; }
		/// <inheritdoc />
		public int? FuzzyMaxExpansions { get; set; }
		/// <inheritdoc />
		public Fuzziness Fuzziness { get; set; }
		/// <inheritdoc />
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		/// <inheritdoc />
		public MultiTermQueryRewrite Rewrite { get; set; }
		/// <inheritdoc />
		public MultiTermQueryRewrite FuzzyRewrite { get; set; }
		/// <inheritdoc />
		public string QuoteFieldSuffix { get; set; }
		/// <inheritdoc />
		public bool? Escape { get; set; }
		/// <inheritdoc />
		public bool? AutoGenerateSynonymsPhraseQuery { get; set; }
    /// <inheritdoc />
		public string Query { get; set; }
		/// <inheritdoc />
		public string Timezone { get; set; }
		/// <inheritdoc />
		public Field DefaultField { get; set; }
		/// <inheritdoc />
		public Fields Fields { get; set; }
		/// <inheritdoc />
		public Operator? DefaultOperator { get; set; }
		/// <inheritdoc />
		public string Analyzer { get; set; }
		/// <inheritdoc />
		public string QuoteAnalyzer { get; set; }
		/// <inheritdoc />
		public bool? AllowLeadingWildcard { get; set; }
		/// <inheritdoc />
		public bool? EnablePositionIncrements { get; set; }
		/// <inheritdoc />
		public int? FuzzyPrefixLength { get; set; }
		/// <inheritdoc />
		public bool? FuzzyTranspositions { get; set; }
		/// <inheritdoc />
		public double? PhraseSlop { get; set; }
		/// <inheritdoc />
		public bool? Lenient { get; set; }
		/// <inheritdoc />
		public bool? AnalyzeWildcard { get; set; }
		/// <inheritdoc />
		public double? TieBreaker { get; set; }
		/// <inheritdoc />
		public int? MaximumDeterminizedStates { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.QueryString = this;
		internal static bool IsConditionless(IQueryStringQuery q) => q.Query.IsNullOrEmpty();
	}

	/// <inheritdoc cref="IQueryStringQuery"/>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class QueryStringQueryDescriptor<T>
		: QueryDescriptorBase<QueryStringQueryDescriptor<T>, IQueryStringQuery>
		, IQueryStringQuery where T : class
	{
		protected override bool Conditionless => QueryStringQuery.IsConditionless(this);

		TextQueryType? IQueryStringQuery.Type { get; set; }
		string IQueryStringQuery.Query { get; set; }
		Field IQueryStringQuery.DefaultField { get; set; }
		Fields IQueryStringQuery.Fields { get; set; }
		Operator? IQueryStringQuery.DefaultOperator { get; set; }
		string IQueryStringQuery.Analyzer { get; set; }
		string IQueryStringQuery.QuoteAnalyzer { get; set; }
		bool? IQueryStringQuery.AllowLeadingWildcard { get; set; }
		int? IQueryStringQuery.FuzzyMaxExpansions { get; set; }
		Fuzziness IQueryStringQuery.Fuzziness { get; set; }
		int? IQueryStringQuery.FuzzyPrefixLength { get; set; }
		bool? IQueryStringQuery.FuzzyTranspositions { get; set; }
		double? IQueryStringQuery.PhraseSlop { get; set; }
		MinimumShouldMatch IQueryStringQuery.MinimumShouldMatch { get; set; }
		bool? IQueryStringQuery.Lenient { get; set; }
		bool? IQueryStringQuery.AnalyzeWildcard { get; set; }
		double? IQueryStringQuery.TieBreaker { get; set; }
		int? IQueryStringQuery.MaximumDeterminizedStates { get; set; }
		MultiTermQueryRewrite IQueryStringQuery.FuzzyRewrite { get; set; }
		MultiTermQueryRewrite IQueryStringQuery.Rewrite { get; set; }
		string IQueryStringQuery.QuoteFieldSuffix { get; set; }
		bool? IQueryStringQuery.Escape { get; set; }
		bool? IQueryStringQuery.EnablePositionIncrements { get; set; }
		string IQueryStringQuery.Timezone { get; set; }
		bool? IQueryStringQuery.AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <inheritdoc cref="IQueryStringQuery.DefaultField"/>
		public QueryStringQueryDescriptor<T> DefaultField(Field field) => Assign(a => a.DefaultField = field);

		/// <inheritdoc cref="IQueryStringQuery.DefaultField"/>
		public QueryStringQueryDescriptor<T> DefaultField(Expression<Func<T, object>> field) => Assign(a => a.DefaultField = field);

		/// <inheritdoc cref="IQueryStringQuery.Fields"/>
		public QueryStringQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="IQueryStringQuery.Fields"/>
		public QueryStringQueryDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		/// <inheritdoc cref="IQueryStringQuery.Type"/>
		public QueryStringQueryDescriptor<T> Type(TextQueryType? type) => Assign(a => a.Type = type);

		/// <inheritdoc cref="IQueryStringQuery.Query"/>
		public QueryStringQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		/// <inheritdoc cref="IQueryStringQuery.DefaultOperator"/>
		public QueryStringQueryDescriptor<T> DefaultOperator(Operator? op) => Assign(a => a.DefaultOperator = op);

		/// <inheritdoc cref="IQueryStringQuery.Analyzer"/>
		public QueryStringQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		/// <inheritdoc cref="IQueryStringQuery.QuoteAnalyzer"/>
		public QueryStringQueryDescriptor<T> QuoteAnalyzer(string analyzer) => Assign(a => a.QuoteAnalyzer = analyzer);

		/// <inheritdoc cref="IQueryStringQuery.AllowLeadingWildcard"/>
		public QueryStringQueryDescriptor<T> AllowLeadingWildcard(bool? allowLeadingWildcard = true) =>
			Assign(a => a.AllowLeadingWildcard = allowLeadingWildcard);

		/// <inheritdoc cref="IQueryStringQuery.Fuzziness"/>
		public QueryStringQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		/// <inheritdoc cref="IQueryStringQuery.FuzzyPrefixLength"/>
		public QueryStringQueryDescriptor<T> FuzzyPrefixLength(int? fuzzyPrefixLength) => Assign(a => a.FuzzyPrefixLength = fuzzyPrefixLength);

		/// <inheritdoc cref="IQueryStringQuery.FuzzyMaxExpansions"/>
		public QueryStringQueryDescriptor<T> FuzzyMaxExpansions(int? fuzzyMaxExpansions) => Assign(a => a.FuzzyMaxExpansions = fuzzyMaxExpansions);

		/// <inheritdoc cref="IQueryStringQuery.FuzzyTranspositions"/>
		public QueryStringQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranspositions = true) =>
			Assign(a => a.FuzzyTranspositions = fuzzyTranspositions);

		/// <inheritdoc cref="IQueryStringQuery.PhraseSlop"/>
		public QueryStringQueryDescriptor<T> PhraseSlop(double? phraseSlop) => Assign(a => a.PhraseSlop = phraseSlop);

		/// <inheritdoc cref="IQueryStringQuery.MinimumShouldMatch"/>
		public QueryStringQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) => Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		/// <inheritdoc cref="IQueryStringQuery.Lenient"/>
		public QueryStringQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(a => a.Lenient = lenient);

		/// <inheritdoc cref="IQueryStringQuery.AnalyzeWildcard"/>
		public QueryStringQueryDescriptor<T> AnalyzeWildcard(bool? analyzeWildcard = true) => Assign(a => a.AnalyzeWildcard = analyzeWildcard);

		/// <inheritdoc cref="IQueryStringQuery.TieBreaker"/>
		public QueryStringQueryDescriptor<T> TieBreaker(double? tieBreaker) => Assign(a => a.TieBreaker = tieBreaker);

		/// <inheritdoc cref="IQueryStringQuery.MaximumDeterminizedStates"/>
		public QueryStringQueryDescriptor<T> MaximumDeterminizedStates(int? maxDeterminizedStates) => Assign(a => a.MaximumDeterminizedStates = maxDeterminizedStates);

		/// <inheritdoc cref="IQueryStringQuery.FuzzyRewrite"/>
		public QueryStringQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.FuzzyRewrite = rewrite);

		/// <inheritdoc cref="IQueryStringQuery.Rewrite"/>
		public QueryStringQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.Rewrite = rewrite);

		/// <inheritdoc cref="IQueryStringQuery.QuoteFieldSuffix"/>
		public QueryStringQueryDescriptor<T> QuoteFieldSuffix(string quoteFieldSuffix) =>
			Assign(a => a.QuoteFieldSuffix = quoteFieldSuffix);

		/// <inheritdoc cref="IQueryStringQuery.Escape"/>
		public QueryStringQueryDescriptor<T> Escape(bool? escape = true) => Assign(a => a.Escape = escape);

		/// <inheritdoc cref="IQueryStringQuery.EnablePositionIncrements"/>
		public QueryStringQueryDescriptor<T> EnablePositionIncrements(bool? enablePositionIncrements = true) =>
			Assign(a => a.EnablePositionIncrements = enablePositionIncrements);

		/// <inheritdoc cref="IQueryStringQuery.Timezone"/>
		public QueryStringQueryDescriptor<T> Timezone(string timezone) => Assign(a => a.Timezone = timezone);
      
    /// <inheritdoc cref="IQueryStringQuery.AutoGenerateSynonymsPhraseQuery"/>
		public QueryStringQueryDescriptor<T> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true) =>
			Assign(a => a.AutoGenerateSynonymsPhraseQuery = autoGenerateSynonymsPhraseQuery);
	}
}
