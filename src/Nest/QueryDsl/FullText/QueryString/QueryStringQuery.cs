using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<QueryStringQueryDescriptor<object>>))]
	public interface IQueryStringQuery : IQuery
	{
		[JsonProperty("query")]
		string Query { get; set; }

		[JsonProperty("default_field")]
		Field DefaultField { get; set; }

		[JsonProperty("default_operator")]
		Operator? DefaultOperator { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("quote_analyzer")]
		string QuoteAnalyzer { get; set; }

		[JsonProperty("allow_leading_wildcard")]
		bool? AllowLeadingWildcard { get; set; }

		[JsonProperty("lowercase_expanded_terms")]
		bool? LowercaseExpendedTerms { get; set; }

		[JsonProperty("enable_position_increments")]
		bool? EnablePositionIncrements { get; set; }

		[JsonProperty("fuzzy_max_expansions")]
		int? FuzzyMaxExpansions { get; set; }

		[JsonProperty("fuzziness")]
		Fuzziness Fuzziness { get; set; }

		[JsonProperty("fuzzy_prefix_length")]
		int? FuzzyPrefixLength { get; set; }

		[JsonProperty("phrase_slop")]
		double? PhraseSlop { get; set; }

		[JsonProperty("analyze_wildcard")]
		bool? AnalyzeWildcard { get; set; }

		[JsonProperty("auto_generate_phrase_queries")]
		bool? AutoGeneratePhraseQueries { get; set; }

		[JsonProperty("max_determinized_states")]
		int? MaximumDeterminizedStates { get; set; }

		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[JsonProperty("lenient")]
		bool? Lenient { get; set; }

		[JsonProperty("locale")]
		string Locale { get; set; }

		[JsonProperty("time_zone")]
		string Timezone { get; set; }

		[JsonProperty("fields")]
		Fields Fields { get; set; }

		[JsonProperty("use_dis_max")]
		bool? UseDisMax { get; set; }

		[JsonProperty("tie_breaker")]
		double? TieBreaker { get; set; }

		[JsonProperty("rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }

		[JsonProperty("fuzzy_rewrite")]
		MultiTermQueryRewrite FuzzyRewrite { get; set; }

		[JsonProperty("quote_field_suffix")]
		string QuoteFieldSuffix { get; set; }

		[JsonProperty("escape")]
		bool? Escape { get; set; }
	}

	public class QueryStringQuery : QueryBase, IQueryStringQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public int? FuzzyMaxExpansions { get; set; }
		public Fuzziness Fuzziness { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public string Locale { get; set; }
		public MultiTermQueryRewrite Rewrite { get; set; }
		public MultiTermQueryRewrite FuzzyRewrite { get; set; }
		public string QuoteFieldSuffix { get; set; }
		public bool? Escape { get; set; }
		public string Query { get; set; }
		public string Timezone { get; set; }
		public Field DefaultField { get; set; }
		public Fields Fields { get; set; }
		public Operator? DefaultOperator { get; set; }
		public string Analyzer { get; set; }
		public string QuoteAnalyzer { get; set; }
		public bool? AllowLeadingWildcard { get; set; }
		public bool? LowercaseExpendedTerms { get; set; }
		public bool? EnablePositionIncrements { get; set; }
		public int? FuzzyPrefixLength { get; set; }
		public double? PhraseSlop { get; set; }
		public bool? Lenient { get; set; }
		public bool? AnalyzeWildcard { get; set; }
		public bool? AutoGeneratePhraseQueries { get; set; }
		public bool? UseDisMax { get; set; }
		public double? TieBreaker { get; set; }
		public int? MaximumDeterminizedStates { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.QueryString = this;
		internal static bool IsConditionless(IQueryStringQuery q) => q.Query.IsNullOrEmpty();
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class QueryStringQueryDescriptor<T>
		: QueryDescriptorBase<QueryStringQueryDescriptor<T>, IQueryStringQuery>
		, IQueryStringQuery where T : class
	{
		protected override bool Conditionless => QueryStringQuery.IsConditionless(this);

		string IQueryStringQuery.Query { get; set; }
		string IQueryStringQuery.Locale { get; set; }
		string IQueryStringQuery.Timezone { get; set; }
		Field IQueryStringQuery.DefaultField { get; set; }
		Fields IQueryStringQuery.Fields { get; set; }
		Operator? IQueryStringQuery.DefaultOperator { get; set; }
		string IQueryStringQuery.Analyzer { get; set; }
		string IQueryStringQuery.QuoteAnalyzer { get; set; }
		bool? IQueryStringQuery.AllowLeadingWildcard { get; set; }
		bool? IQueryStringQuery.LowercaseExpendedTerms { get; set; }
		bool? IQueryStringQuery.EnablePositionIncrements { get; set; }
		int? IQueryStringQuery.FuzzyMaxExpansions { get; set; }
		Fuzziness IQueryStringQuery.Fuzziness { get; set; }
		int? IQueryStringQuery.FuzzyPrefixLength { get; set; }
		double? IQueryStringQuery.PhraseSlop { get; set; }
		MinimumShouldMatch IQueryStringQuery.MinimumShouldMatch { get; set; }
		bool? IQueryStringQuery.Lenient { get; set; }
		bool? IQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? IQueryStringQuery.AutoGeneratePhraseQueries { get; set; }
		bool? IQueryStringQuery.UseDisMax { get; set; }
		double? IQueryStringQuery.TieBreaker { get; set; }
		int? IQueryStringQuery.MaximumDeterminizedStates { get; set; }
		MultiTermQueryRewrite IQueryStringQuery.FuzzyRewrite { get; set; }
		MultiTermQueryRewrite IQueryStringQuery.Rewrite { get; set; }
		string IQueryStringQuery.QuoteFieldSuffix { get; set; }
		bool? IQueryStringQuery.Escape { get; set; }

		public QueryStringQueryDescriptor<T> DefaultField(Field field) => Assign(a => a.DefaultField = field);
		public QueryStringQueryDescriptor<T> DefaultField(Expression<Func<T, object>> field) => Assign(a => a.DefaultField = field);

		public QueryStringQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public QueryStringQueryDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		public QueryStringQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public QueryStringQueryDescriptor<T> Locale(string locale) => Assign(a => a.Locale = locale);

		public QueryStringQueryDescriptor<T> Timezone(string timezone) => Assign(a => a.Timezone = timezone);

		public QueryStringQueryDescriptor<T> DefaultOperator(Operator? op) => Assign(a => a.DefaultOperator = op);

		public QueryStringQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public QueryStringQueryDescriptor<T> QuoteAnalyzer(string analyzer) => Assign(a => a.QuoteAnalyzer = analyzer);

		public QueryStringQueryDescriptor<T> AllowLeadingWildcard(bool? allowLeadingWildcard = true) =>
			Assign(a => a.AllowLeadingWildcard = allowLeadingWildcard);

		public QueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool? lowercaseExpendedTerms = true) =>
			Assign(a => a.LowercaseExpendedTerms = lowercaseExpendedTerms);

		public QueryStringQueryDescriptor<T> EnablePositionIncrements(bool? enablePositionIncrements = true) =>
			Assign(a => a.EnablePositionIncrements = enablePositionIncrements);

		public QueryStringQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public QueryStringQueryDescriptor<T> FuzzyPrefixLength(int? fuzzyPrefixLength) => Assign(a => a.FuzzyPrefixLength = fuzzyPrefixLength);

		public QueryStringQueryDescriptor<T> FuzzyMaxExpansions(int? fuzzyMaxExpansions) => Assign(a => a.FuzzyMaxExpansions = fuzzyMaxExpansions);

		public QueryStringQueryDescriptor<T> PhraseSlop(double? phraseSlop) => Assign(a => a.PhraseSlop = phraseSlop);

		public QueryStringQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) => Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		public QueryStringQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(a => a.Lenient = lenient);

		public QueryStringQueryDescriptor<T> AnalyzeWildcard(bool? analyzeWildcard = true) => Assign(a => a.AnalyzeWildcard = analyzeWildcard);

		public QueryStringQueryDescriptor<T> AutoGeneratePhraseQueries(bool? autoGeneratePhraseQueries = true) =>
			Assign(a => a.AutoGeneratePhraseQueries = autoGeneratePhraseQueries);

		public QueryStringQueryDescriptor<T> UseDisMax(bool? useDismax = true) => Assign(a => a.UseDisMax = useDismax);

		public QueryStringQueryDescriptor<T> TieBreaker(double? tieBreaker) => Assign(a => a.TieBreaker = tieBreaker);

		public QueryStringQueryDescriptor<T> MaximumDeterminizedStates(int? maxDeterminizedStates) => Assign(a => a.MaximumDeterminizedStates = maxDeterminizedStates);

		public QueryStringQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.FuzzyRewrite = rewrite);

		public QueryStringQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.Rewrite = rewrite);

		public QueryStringQueryDescriptor<T> QuoteFieldSuffix(string quoteFieldSuffix) => Assign(a => a.QuoteFieldSuffix = quoteFieldSuffix);

		public QueryStringQueryDescriptor<T> Escape(bool? escape = true) => Assign(a => a.Escape = escape);

	}
}
