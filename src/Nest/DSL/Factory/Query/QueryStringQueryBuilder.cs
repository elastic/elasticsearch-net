using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A query that parses a query string and runs it. There are two modes that this operates. The first,
    /// when no field is added (using {@link #field(string)}, will run the query once and non prefixed fields
    /// will use the {@link #defaultField(string)} set. The second, when one or more fields are added
    /// (using {@link #field(string)}), will run the parsed query against the provided fields, and combine
    /// them either using DisMax or a plain boolean query (see {@link #useDisMax(boolean)}).
    /// </summary>
    public class QueryStringQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.QuerystringQueryBuilder;
        private readonly string _querystring;
        private string _defaultField;
        private Operator? _defaultOperator;
        private string _analyzer;
        private bool _autoGeneratePhraseQueries;
        private bool _allowLeadingWildcard;
        private bool _lowercaseExpandedTerms;
        private bool _enablePositionIncrements;
        private bool _analyzeWildcard;
        private float? _fuzzyMinSim;
        private float? _boost;
        private int? _fuzzyPrefixLength;
        private int? _phraseSlop;
        private List<string> _fields;
        private Dictionary<string, float?> _fieldsBoosts;
        private bool _useDisMax;
        private float? _tieBreaker;
        private string _rewrite = null;
        private string _minimumShouldMatch;

        public QueryStringQueryBuilder(string querystring)
        {
            _querystring = querystring;
        }

        /// <summary>
        /// The default field to run against when no prefix field is specified. Only relevant when
        /// not explicitly adding fields the query string will run against.
        /// </summary>
        /// <param name="defaultField"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder DefaultField(string defaultField)
        {
            _defaultField = defaultField;
            return this;
        }

        /// <summary>
        /// Adds a field to run the query string against.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder Field(string field)
        {
            if (_fields == null)
            {
                _fields = new List<string>();
            }
            _fields.Add(field);
            return this;
        }

        /// <summary>
        /// Adds a field to run the query string against with a specific boost.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="boost"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder Field(string field, float boost)
        {
            if (_fields == null)
            {
                _fields = new List<string>();
            }
            _fields.Add(field);

            if (_fieldsBoosts == null)
            {
                _fieldsBoosts = new Dictionary<string, float?>();
            }
            _fieldsBoosts.Add(field, boost);

            return this;
        }

        /// <summary>
        /// When more than one field is used with the query string, should queries be combined using
        /// dis max, or boolean query. Defaults to dis max (<tt>true</tt>).
        /// </summary>
        /// <param name="useDisMax"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder UseDisMax(bool useDisMax)
        {
            _useDisMax = useDisMax;
            return this;
        }

        /// <summary>
        /// When more than one field is used with the query string, and combined queries are using
        /// dis max, control the tie breaker for it.
        /// </summary>
        /// <param name="tieBreaker"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder TieBreaker(float tieBreaker)
        {
            _tieBreaker = tieBreaker;
            return this;
        }

        /// <summary>
        /// Sets the boolean operator of the query parser used to parse the query string.
        ///
        /// In default mode ({@link FieldQueryBuilder.Operator#OR}) terms without any modifiers
        /// are considered optional: for example <code>capital of Hungary</code> is equal to
        /// <code>capital OR of OR Hungary</code>.
        ///
        /// In {@link FieldQueryBuilder.Operator#AND} mode terms are considered to be in conjunction: the
        /// above mentioned query is parsed as <code>capital AND of AND Hungary</code>
        /// </summary>
        /// <param name="defaultOperator"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder DefaultOperator(Operator defaultOperator)
        {
            _defaultOperator = defaultOperator;
            return this;
        }

        /// <summary>
        /// The optional analyzer used to analyze the query string. Note, if a field has search analyzer
        /// defined for it, then it will be used automatically. Defaults to the smart search analyzer.
        /// </summary>
        /// <param name="analyzer"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder Analyzer(string analyzer)
        {
            _analyzer = analyzer;
            return this;
        }

        /// <summary>
        /// Set to true if phrase queries will be automatically generated
        /// when the analyzer returns more than one term from whitespace
        /// delimited text.
        /// NOTE: this behavior may not be suitable for all languages.
        /// 
        /// Set to false if phrase queries should only be generated when
        /// surrounded by double quotes.
        /// </summary>
        /// <param name="autoGeneratePhraseQueries"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder AutoGeneratePhraseQueries(bool autoGeneratePhraseQueries)
        {
            _autoGeneratePhraseQueries = autoGeneratePhraseQueries;
            return this;
        }

        /// <summary>
        /// Should leading wildcards be allowed or not. Defaults to <tt>true</tt>.
        /// </summary>
        /// <param name="allowLeadingWildcard"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder AllowLeadingWildcard(bool allowLeadingWildcard)
        {
            _allowLeadingWildcard = allowLeadingWildcard;
            return this;
        }

        /// <summary>
        /// Whether terms of wildcard, prefix, fuzzy and range queries are to be automatically
        /// lower-cased or not.  Default is <tt>true</tt>.
        /// </summary>
        /// <param name="lowercaseExpandedTerms"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder LowercaseExpandedTerms(bool lowercaseExpandedTerms) 
        {
            _lowercaseExpandedTerms = lowercaseExpandedTerms;
            return this;
        }

        /// <summary>
        /// Set to <tt>true</tt> to enable position increments in result query. Defaults to
        /// <tt>true</tt>.
        ///
        /// When set, result phrase and multi-phrase queries will be aware of position increments.
        /// Useful when e.g. a StopFilter increases the position increment of the token that follows an omitted token.
        /// </summary>
        /// <param name="enablePositionIncrements"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder EnablePositionIncrements(bool enablePositionIncrements)
        {
            _enablePositionIncrements = enablePositionIncrements;
            return this;
        }

        /// <summary>
        /// Set the minimum similarity for fuzzy queries. Default is 0.5f.
        /// </summary>
        /// <param name="fuzzyMinSim"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder FuzzyMinSim(float fuzzyMinSim)
        {
            _fuzzyMinSim = fuzzyMinSim;
            return this;
        }

        /// <summary>
        ///  Set the minimum similarity for fuzzy queries. Default is 0.5f.
        /// </summary>
        /// <param name="fuzzyPrefixLength"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder FuzzyPrefixLength(int fuzzyPrefixLength)
        {
            _fuzzyPrefixLength = fuzzyPrefixLength;
            return this;
        }

        /// <summary>
        /// Sets the default slop for phrases.  If zero, then exact phrase matches
        /// are required. Default value is zero.
        /// </summary>
        /// <param name="phraseSlop"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder PhraseSlop(int phraseSlop)
        {
            _phraseSlop = phraseSlop;
            return this;
        }

        /// <summary>
        /// Set to <tt>true</tt> to enable analysis on wildcard and prefix queries.
        /// </summary>
        /// <param name="analyzeWildcard"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder AnalyzeWildcard(bool analyzeWildcard)
        {
            _analyzeWildcard = analyzeWildcard;
            return this;
        }

        public QueryStringQueryBuilder Rewrite(string rewrite)
        {
            _rewrite = rewrite;
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        ///  weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public QueryStringQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        public QueryStringQueryBuilder MinimumShouldMatch(string minimumShouldMatch)
        {
            _minimumShouldMatch = minimumShouldMatch;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            
            content[NAME]["query"] = _querystring;
            
            if(!string.IsNullOrEmpty(_defaultField))
            {
                content[NAME]["default_field"] = _defaultField;
            }

            if(_fields != null)
            {
                var fieldList = new List<string>();

                foreach (var field in _fields)
                {
                    float? boost = null;
                    var fieldFinal = field;

                    if(_fieldsBoosts != null && _fieldsBoosts.ContainsKey(field))
                    {
                        boost = _fieldsBoosts[field];
                    }

                    if(boost != null)
                    {
                        fieldFinal += "^" + boost;
                    }

                    fieldList.Add(fieldFinal);
                }

                content[NAME]["fields"] = new JArray(fieldList);
            }

            if(_useDisMax)
            {
                content[NAME]["use_dis_max"] = _useDisMax;
            }

            if (_tieBreaker != null)
            {
                content[NAME]["tie_breaker"] = _tieBreaker;
            }

            if (_defaultOperator != null)
            {
                content[NAME]["default_operator"] = _defaultOperator.Value.ToString().ToLower();
            }

            if (_analyzer != null)
            {
                content[NAME]["analyzer"] = _analyzer;
            }

            if (_autoGeneratePhraseQueries)
            {
                content[NAME]["auto_generate_phrase_queries"] = _autoGeneratePhraseQueries;
            }

            if (_allowLeadingWildcard)
            {
                content[NAME]["allow_leading_wildcard"] = _allowLeadingWildcard;
            }

            if (_lowercaseExpandedTerms)
            {
                content[NAME]["lowercase_expanded_terms"] = _lowercaseExpandedTerms;
            }

            if (_enablePositionIncrements)
            {
                content[NAME]["enable_position_increments"] = _enablePositionIncrements;
            }

            if (_fuzzyMinSim != null)
            {
                content[NAME]["fuzzy_min_sim"] = _fuzzyMinSim;
            }

            if (_boost != null)
            {
                content[NAME]["boost"] = _boost;
            }

            if (_fuzzyPrefixLength != null)
            {
                content[NAME]["fuzzy_prefix_length"] = _fuzzyPrefixLength;
            }

            if (_phraseSlop != null)
            {
                content[NAME]["phrase_slop"] = _phraseSlop;
            }

            if (_analyzeWildcard)
            {
                content[NAME]["analyze_wildcard"] = _analyzeWildcard;
            }

            if (_rewrite != null)
            {
                content[NAME]["rewrite"] = _rewrite;
            }

            if (_minimumShouldMatch != null)
            {
                content[NAME]["minimum_should_match"] = _minimumShouldMatch;
            }
            
            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}