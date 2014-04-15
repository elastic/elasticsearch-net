using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IQueryStringQuery
	{
		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

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
		bool? UseDismax { get; set; }

		[JsonProperty(PropertyName = "tie_breaker")]
		double? TieBreaker { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class QueryStringQueryDescriptor<T> : IQuery, IQueryStringQuery where T : class
	{
		string IQueryStringQuery.Query { get; set; }

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
		
		bool? IQueryStringQuery.UseDismax { get; set; }
		
		double? IQueryStringQuery.TieBreaker { get; set; }
		
		RewriteMultiTerm? IQueryStringQuery.Rewrite { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IQueryStringQuery)this).Query.IsNullOrEmpty();
			}
		}


		public QueryStringQueryDescriptor<T> OnField(string field)
		{
			((IQueryStringQuery)this).DefaultField = field;
			return this;
		}
		public QueryStringQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IQueryStringQuery)this).DefaultField = objectPath;
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((IQueryStringQuery)this).Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((IQueryStringQuery)this).Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			((IQueryStringQuery)this).Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector) 
		{
			var d = new FluentDictionary<string, double?>();
			boostableSelector(d);
			((IQueryStringQuery)this).Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public QueryStringQueryDescriptor<T> Query(string query)
		{
			((IQueryStringQuery)this).Query = query;
			return this;
		}
		public QueryStringQueryDescriptor<T> DefaultOperator(Operator op)
		{
			((IQueryStringQuery)this).DefaultOperator = op;
			return this;
		}
		public QueryStringQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IQueryStringQuery)this).Analyzer = analyzer;
			return this;
		}
		public QueryStringQueryDescriptor<T> AllowLeadingWildcard(bool allowLeadingWildcard)
		{
			((IQueryStringQuery)this).AllowLeadingWildcard = allowLeadingWildcard;
			return this;
		}
		public QueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms)
		{
			((IQueryStringQuery)this).LowercaseExpendedTerms = lowercaseExpendedTerms;
			return this;
		}
		public QueryStringQueryDescriptor<T> EnablePositionIncrements(bool enablePositionIncrements)
		{
			((IQueryStringQuery)this).EnablePositionIncrements = enablePositionIncrements;
			return this;
		}
		public QueryStringQueryDescriptor<T> FuzzyPrefixLength(int fuzzyPrefixLength)
		{
			((IQueryStringQuery)this).FuzzyPrefixLength = fuzzyPrefixLength;
			return this;
		}
		public QueryStringQueryDescriptor<T> FuzzyMinimumSimilarity(double fuzzyMinimumSimilarity)
		{
			((IQueryStringQuery)this).FuzzyMinimumSimilarity = fuzzyMinimumSimilarity;
			return this;
		}
		public QueryStringQueryDescriptor<T> PhraseSlop(double phraseSlop)
		{
			((IQueryStringQuery)this).PhraseSlop = phraseSlop;
			return this;
		}
		public QueryStringQueryDescriptor<T> Boost(double boost)
		{
			((IQueryStringQuery)this).Boost = boost;
			return this;
		}
		public QueryStringQueryDescriptor<T> Rewrite(RewriteMultiTerm rewriteMultiTerm)
		{
			((IQueryStringQuery)this).Rewrite = rewriteMultiTerm;
			return this;
		}
		public QueryStringQueryDescriptor<T> Lenient(bool lenient)
		{
			((IQueryStringQuery)this).Lenient = lenient;
			return this;
		}
		public QueryStringQueryDescriptor<T> AnalyzeWildcard(bool analyzeWildcard)
		{
			((IQueryStringQuery)this).AnalyzeWildcard = analyzeWildcard;
			return this;
		}
		public QueryStringQueryDescriptor<T> AutoGeneratePhraseQueries(bool autoGeneratePhraseQueries)
		{
			((IQueryStringQuery)this).AutoGeneratePhraseQueries = autoGeneratePhraseQueries;
			return this;
		}
		public QueryStringQueryDescriptor<T> MinimumShouldMatchPercentage(int minimumShouldMatchPercentage)
		{
			((IQueryStringQuery)this).MinimumShouldMatchPercentage = "{0}%".F(minimumShouldMatchPercentage);
			return this;
		}
		public QueryStringQueryDescriptor<T> UseDisMax(bool useDismax)
		{
			((IQueryStringQuery)this).UseDismax = useDismax;
			return this;
		}
		public QueryStringQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			((IQueryStringQuery)this).TieBreaker = tieBreaker;
			return this;
		}

	}
}
