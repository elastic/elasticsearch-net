using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<QueryStringQueryDescriptor<object>>))]
	public interface IQueryStringQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "timezone")]
		string Timezone { get; set; }

		[JsonProperty(PropertyName = "default_field")]
		PropertyPath DefaultField { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPath> Fields { get; set; }

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

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

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
		public PropertyPath DefaultField { get; set; }
		public IEnumerable<PropertyPath> Fields { get; set; }
		public Operator? DefaultOperator { get; set; }
		public string Analyzer { get; set; }
		public bool? AllowLeadingWildcard { get; set; }
		public bool? LowercaseExpendedTerms { get; set; }
		public bool? EnablePositionIncrements { get; set; }
		public int? FuzzyPrefixLength { get; set; }
		public double? FuzzyMinimumSimilarity { get; set; }
		public double? PhraseSlop { get; set; }
		public double? Boost { get; set; }
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
	public class QueryStringQueryDescriptor<T> : IQueryStringQuery where T : class
	{
		QueryStringQueryDescriptor<T> _assign(Action<IQueryStringQuery> assigner) => Fluent.Assign(this, assigner);

		private IQueryStringQuery Self => this;

		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => QueryStringQuery.IsConditionless(this);
		string IQueryStringQuery.Query { get; set; }
		string IQueryStringQuery.Timezone { get; set; }
		PropertyPath IQueryStringQuery.DefaultField { get; set; }
		IEnumerable<PropertyPath> IQueryStringQuery.Fields { get; set; }
		Operator? IQueryStringQuery.DefaultOperator { get; set; }
		string IQueryStringQuery.Analyzer { get; set; }
		bool? IQueryStringQuery.AllowLeadingWildcard { get; set; }
		bool? IQueryStringQuery.LowercaseExpendedTerms { get; set; }
		bool? IQueryStringQuery.EnablePositionIncrements { get; set; }
		int? IQueryStringQuery.FuzzyPrefixLength { get; set; }
		double? IQueryStringQuery.FuzzyMinimumSimilarity { get; set; }
		double? IQueryStringQuery.PhraseSlop { get; set; }
		double? IQueryStringQuery.Boost { get; set; }
		bool? IQueryStringQuery.Lenient { get; set; }
		bool? IQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? IQueryStringQuery.AutoGeneratePhraseQueries { get; set; }
		string IQueryStringQuery.MinimumShouldMatchPercentage { get; set; }
		bool? IQueryStringQuery.UseDisMax { get; set; }
		double? IQueryStringQuery.TieBreaker { get; set; }
		int? IQueryStringQuery.MaximumDeterminizedStates { get; set; }
		RewriteMultiTerm? IQueryStringQuery.Rewrite { get; set; }

		public QueryStringQueryDescriptor<T> Name(string name) => _assign(a => a.Name = name);

		public QueryStringQueryDescriptor<T> DefaultField(string field) => _assign(a => a.DefaultField = field);

		public QueryStringQueryDescriptor<T> DefaultField(Expression<Func<T, object>> objectPath) =>
			_assign(a => a.DefaultField = objectPath);

		public QueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields) =>
			_assign(a => a.Fields = fields?.Select(f => (PropertyPath) f).ToListOrNullIfEmpty());

		public QueryStringQueryDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths) =>
			_assign(a => a.Fields = objectPaths?.Select(f => (PropertyPath) f).ToListOrNullIfEmpty());

		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(Func<
			FluentDictionary<Expression<Func<T, object>>, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				_assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<Expression<Func<T, object>>, double?>())
					.Select(o => PropertyPath.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(
			Func<FluentDictionary<string, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				_assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<string, double?>())
					.Select(o => PropertyPath.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public QueryStringQueryDescriptor<T> Query(string query) => _assign(a => a.Query = query);

		public QueryStringQueryDescriptor<T> Timezone(string timezone) => _assign(a => a.Timezone = timezone);

		public QueryStringQueryDescriptor<T> DefaultOperator(Operator op) => _assign(a => a.DefaultOperator = op);

		public QueryStringQueryDescriptor<T> Analyzer(string analyzer) => _assign(a => a.Analyzer = analyzer);

		public QueryStringQueryDescriptor<T> AllowLeadingWildcard(bool allowLeadingWildcard = true) =>
			_assign(a => a.AllowLeadingWildcard = allowLeadingWildcard);

		public QueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms = true) =>
			_assign(a => a.LowercaseExpendedTerms = lowercaseExpendedTerms);

		public QueryStringQueryDescriptor<T> EnablePositionIncrements(bool enablePositionIncrements = true) =>
			_assign(a => a.EnablePositionIncrements = enablePositionIncrements);

		public QueryStringQueryDescriptor<T> FuzzyPrefixLength(int fuzzyPrefixLength) =>
			_assign(a => a.FuzzyPrefixLength = fuzzyPrefixLength);

		public QueryStringQueryDescriptor<T> FuzzyMinimumSimilarity(double fuzzyMinimumSimilarity) =>
			_assign(a => a.FuzzyMinimumSimilarity = fuzzyMinimumSimilarity);

		public QueryStringQueryDescriptor<T> PhraseSlop(double phraseSlop) => _assign(a => a.PhraseSlop = phraseSlop);

		public QueryStringQueryDescriptor<T> Boost(double boost) => _assign(a => a.Boost = boost);

		public QueryStringQueryDescriptor<T> Rewrite(RewriteMultiTerm rewriteMultiTerm) => _assign(a => a.Rewrite = rewriteMultiTerm);

		public QueryStringQueryDescriptor<T> Lenient(bool lenient = true) => _assign(a => a.Lenient = lenient);

		public QueryStringQueryDescriptor<T> AnalyzeWildcard(bool analyzeWildcard = true)
			=> _assign(a => a.AnalyzeWildcard = analyzeWildcard);

		public QueryStringQueryDescriptor<T> AutoGeneratePhraseQueries(bool autoGeneratePhraseQueries = true) =>
			_assign(a => a.AutoGeneratePhraseQueries = autoGeneratePhraseQueries);

		public QueryStringQueryDescriptor<T> MinimumShouldMatchPercentage(int minimumShouldMatchPercentage) =>
			_assign(a => a.MinimumShouldMatchPercentage = "{0}%".F(minimumShouldMatchPercentage));

		public QueryStringQueryDescriptor<T> UseDisMax(bool useDismax = true) => _assign(a => a.UseDisMax = useDismax);

		public QueryStringQueryDescriptor<T> TieBreaker(double tieBreaker) => _assign(a => a.TieBreaker = tieBreaker);

		public QueryStringQueryDescriptor<T> MaximumDeterminizedStates(int maxDeterminizedStates) =>
			_assign(a => a.MaximumDeterminizedStates = maxDeterminizedStates);

	}
}
