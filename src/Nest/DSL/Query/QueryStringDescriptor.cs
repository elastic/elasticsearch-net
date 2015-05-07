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
		PropertyPathMarker DefaultField { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }

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

	public class QueryStringQuery : PlainQuery, IQueryStringQuery
	{

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.QueryString = this;
		}

		bool IQuery.IsConditionless { get { return false; } }

		public string Name { get; set; }

		public string Query { get; set; }
		public string Timezone { get; set; }

		public PropertyPathMarker DefaultField { get; set; }

		public IEnumerable<PropertyPathMarker> Fields { get; set; }

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
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class QueryStringQueryDescriptor<T> : IQueryStringQuery where T : class
	{
		private IQueryStringQuery Self { get { return this; }}

		string IQueryStringQuery.Query { get; set; }

		string IQueryStringQuery.Timezone { get; set; }

		PropertyPathMarker IQueryStringQuery.DefaultField { get; set; }

		IEnumerable<PropertyPathMarker> IQueryStringQuery.Fields { get; set; }
	
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

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IQueryStringQuery)this).Query.IsNullOrEmpty();
			}
		}

		string IQuery.Name { get; set; }

		public QueryStringQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public QueryStringQueryDescriptor<T> DefaultField(string field)
		{
			Self.DefaultField = field;
			return this;
		}

		public QueryStringQueryDescriptor<T> DefaultField(Expression<Func<T, object>> objectPath)
		{
			Self.DefaultField = objectPath;
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			Self.Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			Self.Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			Self.Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector) 
		{
			var d = new FluentDictionary<string, double?>();
			boostableSelector(d);
			Self.Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public QueryStringQueryDescriptor<T> Query(string query)
		{
			Self.Query = query;
			return this;
		}

		public QueryStringQueryDescriptor<T> Timezone(string timezone)
		{
			Self.Timezone = timezone;
			return this;
		}

		public QueryStringQueryDescriptor<T> DefaultOperator(Operator op)
		{
			Self.DefaultOperator = op;
			return this;
		}
		public QueryStringQueryDescriptor<T> Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}
		public QueryStringQueryDescriptor<T> AllowLeadingWildcard(bool allowLeadingWildcard = true)
		{
			Self.AllowLeadingWildcard = allowLeadingWildcard;
			return this;
		}
		public QueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms = true)
		{
			Self.LowercaseExpendedTerms = lowercaseExpendedTerms;
			return this;
		}
		public QueryStringQueryDescriptor<T> EnablePositionIncrements(bool enablePositionIncrements = true)
		{
			Self.EnablePositionIncrements = enablePositionIncrements;
			return this;
		}
		public QueryStringQueryDescriptor<T> FuzzyPrefixLength(int fuzzyPrefixLength)
		{
			Self.FuzzyPrefixLength = fuzzyPrefixLength;
			return this;
		}
		public QueryStringQueryDescriptor<T> FuzzyMinimumSimilarity(double fuzzyMinimumSimilarity)
		{
			Self.FuzzyMinimumSimilarity = fuzzyMinimumSimilarity;
			return this;
		}
		public QueryStringQueryDescriptor<T> PhraseSlop(double phraseSlop)
		{
			Self.PhraseSlop = phraseSlop;
			return this;
		}
		public QueryStringQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}
		public QueryStringQueryDescriptor<T> Rewrite(RewriteMultiTerm rewriteMultiTerm)
		{
			Self.Rewrite = rewriteMultiTerm;
			return this;
		}
		public QueryStringQueryDescriptor<T> Lenient(bool lenient = true)
		{
			Self.Lenient = lenient;
			return this;
		}
		public QueryStringQueryDescriptor<T> AnalyzeWildcard(bool analyzeWildcard = true)
		{
			Self.AnalyzeWildcard = analyzeWildcard;
			return this;
		}
		public QueryStringQueryDescriptor<T> AutoGeneratePhraseQueries(bool autoGeneratePhraseQueries = true)
		{
			Self.AutoGeneratePhraseQueries = autoGeneratePhraseQueries;
			return this;
		}
		public QueryStringQueryDescriptor<T> MinimumShouldMatchPercentage(int minimumShouldMatchPercentage)
		{
			Self.MinimumShouldMatchPercentage = "{0}%".F(minimumShouldMatchPercentage);
			return this;
		}
		public QueryStringQueryDescriptor<T> UseDisMax(bool useDismax = true)
		{
			Self.UseDisMax = useDismax;
			return this;
		}
		public QueryStringQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			Self.TieBreaker = tieBreaker;
			return this;
		}
		public QueryStringQueryDescriptor<T> MaximumDeterminizedStates(int maxDeterminizedStates)
		{
			Self.MaximumDeterminizedStates = maxDeterminizedStates;
			return this;
		}
	}
}
