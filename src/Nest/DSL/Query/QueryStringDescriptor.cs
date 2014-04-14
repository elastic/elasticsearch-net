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
	public interface IQueryStringQuery
	{
		[JsonProperty(PropertyName = "query")]
		string _QueryString { get; set; }

		[JsonProperty(PropertyName = "default_field")]
		PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> _Fields { get; set; }

		[JsonProperty(PropertyName = "default_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? _DefaultOperator { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string _Analyzer { get; set; }

		[JsonProperty(PropertyName = "allow_leading_wildcard")]
		bool? _AllowLeadingWildcard { get; set; }

		[JsonProperty(PropertyName = "lowercase_expanded_terms")]
		bool? _LowercaseExpendedTerms { get; set; }

		[JsonProperty(PropertyName = "enable_position_increments")]
		bool? _EnablePositionIncrements { get; set; }

		[JsonProperty(PropertyName = "fuzzy_prefix_length")]
		int? _FuzzyPrefixLength { get; set; }

		[JsonProperty(PropertyName = "fuzzy_min_sim")]
		double? _FuzzyMinimumSimilarity { get; set; }

		[JsonProperty(PropertyName = "phrase_slop")]
		double? _PhraseSlop { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "lenient")]
		bool? _Lenient { get; set; }

		[JsonProperty(PropertyName = "analyze_wildcard")]
		bool? _AnalyzeWildcard { get; set; }

		[JsonProperty(PropertyName = "auto_generate_phrase_queries")]
		bool? _AutoGeneratePhraseQueries { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		string _MinimumShouldMatchPercentage { get; set; }

		[JsonProperty(PropertyName = "use_dis_max")]
		bool? _UseDismax { get; set; }

		[JsonProperty(PropertyName = "tie_breaker")]
		double? _TieBreaker { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? _Rewrite { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class QueryStringQueryDescriptor<T> : IQuery, IQueryStringQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		string IQueryStringQuery._QueryString { get; set; }

		[JsonProperty(PropertyName = "default_field")]
		PropertyPathMarker IQueryStringQuery._Field { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> IQueryStringQuery._Fields { get; set; }
	
		[JsonProperty(PropertyName = "default_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? IQueryStringQuery._DefaultOperator { get; set; }
		
		[JsonProperty(PropertyName = "analyzer")]
		string IQueryStringQuery._Analyzer { get; set; }
		
		[JsonProperty(PropertyName = "allow_leading_wildcard")]
		bool? IQueryStringQuery._AllowLeadingWildcard { get; set; }
		
		[JsonProperty(PropertyName = "lowercase_expanded_terms")]
		bool? IQueryStringQuery._LowercaseExpendedTerms { get; set; }
		
		[JsonProperty(PropertyName = "enable_position_increments")]
		bool? IQueryStringQuery._EnablePositionIncrements { get; set; }
		
		[JsonProperty(PropertyName = "fuzzy_prefix_length")]
		int? IQueryStringQuery._FuzzyPrefixLength { get; set; }
		
		[JsonProperty(PropertyName = "fuzzy_min_sim")]
		double? IQueryStringQuery._FuzzyMinimumSimilarity { get; set; }
		
		[JsonProperty(PropertyName = "phrase_slop")]
		double? IQueryStringQuery._PhraseSlop { get; set; }
		
		[JsonProperty(PropertyName = "boost")]
		double? IQueryStringQuery._Boost { get; set; }
		
		[JsonProperty(PropertyName = "lenient")]
		bool? IQueryStringQuery._Lenient { get; set; }
		
		[JsonProperty(PropertyName = "analyze_wildcard")]
		bool? IQueryStringQuery._AnalyzeWildcard { get; set; }
		
		[JsonProperty(PropertyName = "auto_generate_phrase_queries")]
		bool? IQueryStringQuery._AutoGeneratePhraseQueries { get; set; }
		
		[JsonProperty(PropertyName = "minimum_should_match")]
		string IQueryStringQuery._MinimumShouldMatchPercentage { get; set; }
		
		[JsonProperty(PropertyName = "use_dis_max")]
		bool? IQueryStringQuery._UseDismax { get; set; }
		
		[JsonProperty(PropertyName = "tie_breaker")]
		double? IQueryStringQuery._TieBreaker { get; set; }
		
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof(StringEnumConverter))]
		RewriteMultiTerm? IQueryStringQuery._Rewrite { get; set; }


		bool IQuery.IsConditionless
		{
			get
			{
				return ((IQueryStringQuery)this)._QueryString.IsNullOrEmpty();
			}
		}


		public QueryStringQueryDescriptor<T> OnField(string field)
		{
			((IQueryStringQuery)this)._Field = field;
			return this;
		}
		public QueryStringQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IQueryStringQuery)this)._Field = objectPath;
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((IQueryStringQuery)this)._Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((IQueryStringQuery)this)._Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			((IQueryStringQuery)this)._Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}
		public QueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector) 
		{
			var d = new FluentDictionary<string, double?>();
			boostableSelector(d);
			((IQueryStringQuery)this)._Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public QueryStringQueryDescriptor<T> Query(string query)
		{
			((IQueryStringQuery)this)._QueryString = query;
			return this;
		}
		public QueryStringQueryDescriptor<T> DefaultOperator(Operator op)
		{
			((IQueryStringQuery)this)._DefaultOperator = op;
			return this;
		}
		public QueryStringQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IQueryStringQuery)this)._Analyzer = analyzer;
			return this;
		}
		public QueryStringQueryDescriptor<T> AllowLeadingWildcard(bool allowLeadingWildcard)
		{
			((IQueryStringQuery)this)._AllowLeadingWildcard = allowLeadingWildcard;
			return this;
		}
		public QueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms)
		{
			((IQueryStringQuery)this)._LowercaseExpendedTerms = lowercaseExpendedTerms;
			return this;
		}
		public QueryStringQueryDescriptor<T> EnablePositionIncrements(bool enablePositionIncrements)
		{
			((IQueryStringQuery)this)._EnablePositionIncrements = enablePositionIncrements;
			return this;
		}
		public QueryStringQueryDescriptor<T> FuzzyPrefixLength(int fuzzyPrefixLength)
		{
			((IQueryStringQuery)this)._FuzzyPrefixLength = fuzzyPrefixLength;
			return this;
		}
		public QueryStringQueryDescriptor<T> FuzzyMinimumSimilarity(double fuzzyMinimumSimilarity)
		{
			((IQueryStringQuery)this)._FuzzyMinimumSimilarity = fuzzyMinimumSimilarity;
			return this;
		}
		public QueryStringQueryDescriptor<T> PhraseSlop(double phraseSlop)
		{
			((IQueryStringQuery)this)._PhraseSlop = phraseSlop;
			return this;
		}
		public QueryStringQueryDescriptor<T> Boost(double boost)
		{
			((IQueryStringQuery)this)._Boost = boost;
			return this;
		}
		public QueryStringQueryDescriptor<T> Rewrite(RewriteMultiTerm rewriteMultiTerm)
		{
			((IQueryStringQuery)this)._Rewrite = rewriteMultiTerm;
			return this;
		}
		public QueryStringQueryDescriptor<T> Lenient(bool lenient)
		{
			((IQueryStringQuery)this)._Lenient = lenient;
			return this;
		}
		public QueryStringQueryDescriptor<T> AnalyzeWildcard(bool analyzeWildcard)
		{
			((IQueryStringQuery)this)._AnalyzeWildcard = analyzeWildcard;
			return this;
		}
		public QueryStringQueryDescriptor<T> AutoGeneratePhraseQueries(bool autoGeneratePhraseQueries)
		{
			((IQueryStringQuery)this)._AutoGeneratePhraseQueries = autoGeneratePhraseQueries;
			return this;
		}
		public QueryStringQueryDescriptor<T> MinimumShouldMatchPercentage(int minimumShouldMatchPercentage)
		{
			((IQueryStringQuery)this)._MinimumShouldMatchPercentage = "{0}%".F(minimumShouldMatchPercentage);
			return this;
		}
		public QueryStringQueryDescriptor<T> UseDisMax(bool useDismax)
		{
			((IQueryStringQuery)this)._UseDismax = useDismax;
			return this;
		}
		public QueryStringQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			((IQueryStringQuery)this)._TieBreaker = tieBreaker;
			return this;
		}

	}
}
