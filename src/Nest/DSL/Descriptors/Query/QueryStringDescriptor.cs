using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class QueryStringDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal string _QueryString { get; set; }
		[JsonProperty(PropertyName = "default_field")]
		internal string _Field { get; set; }
		[JsonProperty(PropertyName = "fields")]
		internal IEnumerable<string> _Fields { get; set; }
		[JsonProperty(PropertyName = "default_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal Operator? _DefaultOperator { get; set; }
		[JsonProperty(PropertyName = "analyzer")]
		internal string _Analyzer { get; set; }
		[JsonProperty(PropertyName = "allow_leading_wildcard")]
		internal bool? _AllowLeadingWildcard { get; set; }
		[JsonProperty(PropertyName = "lowercase_expanded_terms")]
		internal bool? _LowercaseExpendedTerms { get; set; }
		[JsonProperty(PropertyName = "enable_position_increments")]
		internal bool? _EnablePositionIncrements { get; set; }
		[JsonProperty(PropertyName = "fuzzy_prefix_length")]
		internal int? _FuzzyPrefixLength { get; set; }
		[JsonProperty(PropertyName = "fuzzy_min_sim")]
		internal double? _FuzzyMinimumSimilarity { get; set; }
		[JsonProperty(PropertyName = "phrase_slop")]
		internal double? _PhraseSlop { get; set; }
		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }
		[JsonProperty(PropertyName = "lenient")]
		internal bool? _Lenient { get; set; }
		[JsonProperty(PropertyName = "analyze_wildcard")]
		internal bool? _AnalyzeWildcard { get; set; }
		[JsonProperty(PropertyName = "auto_generate_phrase_queries")]
		internal bool? _AutoGeneratePhraseQueries { get; set; }
		[JsonProperty(PropertyName = "minimum_should_match")]
		internal string _MinimumShouldMatchPercentage { get; set; }
		[JsonProperty(PropertyName = "use_dis_max")]
		internal bool? _UseDismax { get; set; }
		[JsonProperty(PropertyName = "tie_breaker")]
		internal double? _TieBreaker { get; set; }

		public bool IsConditionless
		{
			get
			{
				return this._QueryString.IsNullOrEmpty();
			}
		}


		public QueryStringDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public QueryStringDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = new PropertyNameResolver().Resolve(objectPath);
			return this.OnField(fieldName);
		}
		public QueryStringDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			this._Fields = fields;
			return this;
		}
		public QueryStringDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			var fieldNames = objectPaths
				.Select(o => new PropertyNameResolver().Resolve(o));
			return this.OnFields(fieldNames);
		}
		public QueryStringDescriptor<T> OnFieldsWithBoost(
			Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			var fieldNames = d
				.Select(o =>
				{
					var field = new PropertyNameResolver().Resolve(o.Key);
					var boost = o.Value.GetValueOrDefault(1.0);
					return "{0}^{1}".F(field, boost.ToString(CultureInfo.InvariantCulture));
				});
			return this.OnFields(fieldNames);
		}


		public QueryStringDescriptor<T> Query(string query)
		{
			this._QueryString = query;
			return this;
		}
		public QueryStringDescriptor<T> Operator(Operator op)
		{
			this._DefaultOperator = op;
			return this;
		}
		public QueryStringDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}
		public QueryStringDescriptor<T> AllowLeadingWildcard(bool allowLeadingWildcard)
		{
			this._AllowLeadingWildcard = allowLeadingWildcard;
			return this;
		}
		public QueryStringDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms)
		{
			this._LowercaseExpendedTerms = lowercaseExpendedTerms;
			return this;
		}
		public QueryStringDescriptor<T> EnablePositionIncrements(bool enablePositionIncrements)
		{
			this._EnablePositionIncrements = enablePositionIncrements;
			return this;
		}
		public QueryStringDescriptor<T> FuzzyPrefixLength(int fuzzyPrefixLength)
		{
			this._FuzzyPrefixLength = fuzzyPrefixLength;
			return this;
		}
		public QueryStringDescriptor<T> FuzzyMinimumSimilarity(double fuzzyMinimumSimilarity)
		{
			this._FuzzyMinimumSimilarity = fuzzyMinimumSimilarity;
			return this;
		}
		public QueryStringDescriptor<T> PhraseSlop(double phraseSlop)
		{
			this._PhraseSlop = phraseSlop;
			return this;
		}
		public QueryStringDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public QueryStringDescriptor<T> Lenient(bool lenient)
		{
			this._Lenient = lenient;
			return this;
		}
		public QueryStringDescriptor<T> AnalyzeWildcard(bool analyzeWildcard)
		{
			this._AnalyzeWildcard = analyzeWildcard;
			return this;
		}
		public QueryStringDescriptor<T> AutoGeneratePhraseQueries(bool autoGeneratePhraseQueries)
		{
			this._AutoGeneratePhraseQueries = autoGeneratePhraseQueries;
			return this;
		}
		public QueryStringDescriptor<T> MinimumShouldMatchPercentage(int minimumShouldMatchPercentage)
		{
			this._MinimumShouldMatchPercentage = "{0}%".F(minimumShouldMatchPercentage);
			return this;
		}
		public QueryStringDescriptor<T> UseDisMax(bool useDismax)
		{
			this._UseDismax = useDismax;
			return this;
		}
		public QueryStringDescriptor<T> TieBreaker(double tieBreaker)
		{
			this._TieBreaker = tieBreaker;
			return this;
		}

	}
}
