using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<QueryStringQueryDescriptor<object>>))]
	public interface IQueryStringQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "timezone")]
		string Timezone { get; set; }

		[JsonProperty(PropertyName = "default_field")]
		Field DefaultField { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<Field> Fields { get; set; }

		[JsonProperty(PropertyName = "default_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? DefaultOperator { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "allow_leading_wildcard")]
		bool? AllowLeadingWildcard { get; set; }

		[JsonProperty(PropertyName = "lowercase_expanded_terms")]
		bool? LowercaseExpendedTerms { get; set; }

		[JsonProperty(PropertyName = "enable_position_increments")]
		bool? EnablePositionIncrements { get; set; }

		[JsonProperty(PropertyName = "fuzzy_prefix_length")]
		int? FuzzyPrefixLength { get; set; }

		[JsonProperty(PropertyName = "fuzzy_min_sim")]
		double? FuzzyMinimumSimilarity { get; set; }

		[JsonProperty(PropertyName = "phrase_slop")]
		double? PhraseSlop { get; set; }
		[JsonProperty(PropertyName = "lenient")]
		bool? Lenient { get; set; }

		[JsonProperty(PropertyName = "analyze_wildcard")]
		bool? AnalyzeWildcard { get; set; }

		[JsonProperty(PropertyName = "auto_generate_phrase_queries")]
		bool? AutoGeneratePhraseQueries { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		string MinimumShouldMatchPercentage { get; set; }

		[JsonProperty(PropertyName = "use_dis_max")]
		bool? UseDisMax { get; set; }

		[JsonProperty(PropertyName = "tie_breaker")]
		double? TieBreaker { get; set; }

		[JsonProperty(PropertyName = "max_determinized_states")]
		int? MaximumDeterminizedStates { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	public class QueryStringQuery : QueryBase, IQueryStringQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Query { get; set; }
		public string Timezone { get; set; }
		public Field DefaultField { get; set; }
		public IEnumerable<Field> Fields { get; set; }
		public Operator? DefaultOperator { get; set; }
		public string Analyzer { get; set; }
		public bool? AllowLeadingWildcard { get; set; }
		public bool? LowercaseExpendedTerms { get; set; }
		public bool? EnablePositionIncrements { get; set; }
		public int? FuzzyPrefixLength { get; set; }
		public double? FuzzyMinimumSimilarity { get; set; }
		public double? PhraseSlop { get; set; }
		public bool? Lenient { get; set; }
		public bool? AnalyzeWildcard { get; set; }
		public bool? AutoGeneratePhraseQueries { get; set; }
		public string MinimumShouldMatchPercentage { get; set; }
		public bool? UseDisMax { get; set; }
		public double? TieBreaker { get; set; }
		public int? MaximumDeterminizedStates { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.QueryString = this;
		internal static bool IsConditionless(IQueryStringQuery q) => q.Query.IsNullOrEmpty();
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class QueryStringQueryDescriptor<T> 
		: QueryDescriptorBase<QueryStringQueryDescriptor<T>, IQueryStringQuery>
		, IQueryStringQuery where T : class
	{
		bool IQuery.Conditionless => QueryStringQuery.IsConditionless(this);
		string IQueryStringQuery.Query { get; set; }
		string IQueryStringQuery.Timezone { get; set; }
		Field IQueryStringQuery.DefaultField { get; set; }
		IEnumerable<Field> IQueryStringQuery.Fields { get; set; }
		Operator? IQueryStringQuery.DefaultOperator { get; set; }
		string IQueryStringQuery.Analyzer { get; set; }
		bool? IQueryStringQuery.AllowLeadingWildcard { get; set; }
		bool? IQueryStringQuery.LowercaseExpendedTerms { get; set; }
		bool? IQueryStringQuery.EnablePositionIncrements { get; set; }
		int? IQueryStringQuery.FuzzyPrefixLength { get; set; }
		double? IQueryStringQuery.FuzzyMinimumSimilarity { get; set; }
		double? IQueryStringQuery.PhraseSlop { get; set; }
		bool? IQueryStringQuery.Lenient { get; set; }
		bool? IQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? IQueryStringQuery.AutoGeneratePhraseQueries { get; set; }
		string IQueryStringQuery.MinimumShouldMatchPercentage { get; set; }
		bool? IQueryStringQuery.UseDisMax { get; set; }
		double? IQueryStringQuery.TieBreaker { get; set; }
		int? IQueryStringQuery.MaximumDeterminizedStates { get; set; }
		RewriteMultiTerm? IQueryStringQuery.Rewrite { get; set; }

		public QueryStringQueryDescriptor<T> DefaultField(string field) => Assign(a => a.DefaultField = field);

		public QueryStringQueryDescriptor<T> DefaultField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.DefaultField = objectPath);

		public QueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields) =>
			Assign(a => a.Fields = fields?.Select(f => (Field) f).ToListOrNullIfEmpty());

		public QueryStringQueryDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths) =>
			Assign(a => a.Fields = objectPaths?.Select(f => (Field) f).ToListOrNullIfEmpty());

		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(Func<
			FluentDictionary<Expression<Func<T, object>>, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				Assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<Expression<Func<T, object>>, double?>())
					.Select(o => Field.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(
			Func<FluentDictionary<string, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				Assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<string, double?>())
					.Select(o => Field.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public QueryStringQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public QueryStringQueryDescriptor<T> Timezone(string timezone) => Assign(a => a.Timezone = timezone);

		public QueryStringQueryDescriptor<T> DefaultOperator(Operator op) => Assign(a => a.DefaultOperator = op);

		public QueryStringQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public QueryStringQueryDescriptor<T> AllowLeadingWildcard(bool allowLeadingWildcard = true) =>
			Assign(a => a.AllowLeadingWildcard = allowLeadingWildcard);

		public QueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms = true) =>
			Assign(a => a.LowercaseExpendedTerms = lowercaseExpendedTerms);

		public QueryStringQueryDescriptor<T> EnablePositionIncrements(bool enablePositionIncrements = true) =>
			Assign(a => a.EnablePositionIncrements = enablePositionIncrements);

		public QueryStringQueryDescriptor<T> FuzzyPrefixLength(int fuzzyPrefixLength) =>
			Assign(a => a.FuzzyPrefixLength = fuzzyPrefixLength);

		public QueryStringQueryDescriptor<T> FuzzyMinimumSimilarity(double fuzzyMinimumSimilarity) =>
			Assign(a => a.FuzzyMinimumSimilarity = fuzzyMinimumSimilarity);

		public QueryStringQueryDescriptor<T> PhraseSlop(double phraseSlop) => Assign(a => a.PhraseSlop = phraseSlop);

		public QueryStringQueryDescriptor<T> Rewrite(RewriteMultiTerm rewriteMultiTerm) => Assign(a => a.Rewrite = rewriteMultiTerm);

		public QueryStringQueryDescriptor<T> Lenient(bool lenient = true) => Assign(a => a.Lenient = lenient);

		public QueryStringQueryDescriptor<T> AnalyzeWildcard(bool analyzeWildcard = true) =>
			Assign(a => a.AnalyzeWildcard = analyzeWildcard);

		public QueryStringQueryDescriptor<T> AutoGeneratePhraseQueries(bool autoGeneratePhraseQueries = true) =>
			Assign(a => a.AutoGeneratePhraseQueries = autoGeneratePhraseQueries);

		public QueryStringQueryDescriptor<T> MinimumShouldMatchPercentage(int minimumShouldMatchPercentage) =>
			Assign(a => a.MinimumShouldMatchPercentage = "{0}%".F(minimumShouldMatchPercentage));

		public QueryStringQueryDescriptor<T> UseDisMax(bool useDismax = true) => Assign(a => a.UseDisMax = useDismax);

		public QueryStringQueryDescriptor<T> TieBreaker(double tieBreaker) => Assign(a => a.TieBreaker = tieBreaker);

		public QueryStringQueryDescriptor<T> MaximumDeterminizedStates(int maxDeterminizedStates) =>
			Assign(a => a.MaximumDeterminizedStates = maxDeterminizedStates);

	}
}
