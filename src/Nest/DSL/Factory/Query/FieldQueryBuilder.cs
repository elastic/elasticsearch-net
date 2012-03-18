
using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A query that executes the query string against a field. It is a simplified
    /// version of {@link QueryStringQueryBuilder} that simply runs against
    /// a single field.
    /// </summary>
    public class FieldQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.FieldQueryBuilder;
        private readonly string _name;
        private readonly object _query;
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
        private bool _extraSet;
        private string _rewrite;
        private string _minimumShouldMatch;

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        ///a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        public FieldQueryBuilder(string name, string query) : this(name, (object)query) { }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        ///a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        public FieldQueryBuilder(string name, int query) : this(name, (object)query) { }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        ///a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        public FieldQueryBuilder(string name, long query) : this(name, (object)query) { }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        ///a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        public FieldQueryBuilder(string name, float query) : this(name, (object)query) { }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        ///a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        public FieldQueryBuilder(string name, double query) : this(name, (object)query) { }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        ///a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        public FieldQueryBuilder(string name, bool query) : this(name, (object)query) { }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        ///a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        public FieldQueryBuilder(string name, object query)
        {
            _name = name;
            _query = query;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public FieldQueryBuilder Boost(float boost)
        {
            _boost = boost;
            _extraSet = true;
            return this;
        }

        /// <summary>
        /// Sets the boolean operator of the query parser used to parse the query string.
        ///
        /// <p>In default mode ({@link FieldQueryBuilder.Operator#OR}) terms without any modifiers
        /// are considered optional: for example <code>capital of Hungary</code> is equal to
        /// <code>capital OR of OR Hungary</code>.</p>
        ///
        /// <p>In {@link FieldQueryBuilder.Operator#AND} mode terms are considered to be in conjunction: the
        /// above mentioned query is parsed as <code>capital AND of AND Hungary</code></p>
        /// </summary>
        /// <param name="defaultOperator"></param>
        /// <returns></returns>
        public FieldQueryBuilder DefaultOperator(Operator defaultOperator)
        {
            _defaultOperator = defaultOperator;
            _extraSet = true;
            return this;
        }

        /// <summary>
        /// The optional analyzer used to analyze the query string. Note, if a field has search analyzer
        /// defined for it, then it will be used automatically. Defaults to the smart search analyzer.
        /// </summary>
        /// <param name="analyzer"></param>
        /// <returns></returns>
        public FieldQueryBuilder Analyzer(string analyzer)
        {
            _analyzer = analyzer;
            _extraSet = true;
            return this;
        }

        /// <summary>
        /// Set to true if phrase queries will be automatically generated
        /// when the analyzer returns more than one term from whitespace
        /// delimited text.
        /// NOTE: this behavior may not be suitable for all languages.
        /// Set to false if phrase queries should only be generated when
        /// surrounded by double quotes.
        /// </summary>
        /// <param name="autoGeneratePhraseQueries"></param>
        public void AutoGeneratePhraseQueries(bool autoGeneratePhraseQueries)
        {
            _autoGeneratePhraseQueries = autoGeneratePhraseQueries;
        }

        /// <summary>
        /// Should leading wildcards be allowed or not. Defaults to <tt>true</tt>.
        /// </summary>
        /// <param name="allowLeadingWildcard"></param>
        /// <returns></returns>
        public FieldQueryBuilder AllowLeadingWildcard(bool allowLeadingWildcard)
        {
            _allowLeadingWildcard = allowLeadingWildcard;
            _extraSet = true;
            return this;
        }

        /// <summary>
        /// Whether terms of wildcard, prefix, fuzzy and range queries are to be automatically
        /// lower-cased or not.  Default is <tt>true</tt>.
        /// </summary>
        /// <param name="lowercaseExpandedTerms"></param>
        /// <returns></returns>
        public FieldQueryBuilder LowercaseExpandedTerms(bool lowercaseExpandedTerms)
        {
            _lowercaseExpandedTerms = lowercaseExpandedTerms;
            _extraSet = true;
            return this;
        }

        /// Set to <tt>true</tt> to enable position increments in result query. Defaults to
        /// <tt>true</tt>.
        ///
        /// <p>When set, result phrase and multi-phrase queries will be aware of position increments.
        /// Useful when e.g. a StopFilter increases the position increment of the token that follows an omitted token.</p>
        /// <param name="enablePositionIncrements"></param>
        /// <returns></returns>
        public FieldQueryBuilder EnablePositionIncrements(bool enablePositionIncrements)
        {
            _enablePositionIncrements = enablePositionIncrements;
            _extraSet = true;
            return this;
        }

        /// <summary>
        /// Set the minimum similarity for fuzzy queries. Default is 0.5f.
        /// </summary>
        /// <param name="fuzzyMinSim"></param>
        /// <returns></returns>
        public FieldQueryBuilder FuzzyMinSim(float fuzzyMinSim)
        {
            _fuzzyMinSim = fuzzyMinSim;
            _extraSet = true;
            return this;
        }

        /// <summary>
        /// Set the prefix length for fuzzy queries. Default is 0.
        /// </summary>
        /// <param name="fuzzyPrefixLength"></param>
        /// <returns></returns>
        public FieldQueryBuilder FuzzyPrefixLength(int fuzzyPrefixLength)
        {
            _fuzzyPrefixLength = fuzzyPrefixLength;
            _extraSet = true;
            return this;
        }

        /// <summary>
        /// Sets the default slop for phrases.  If zero, then exact phrase matches
        /// are required. Default value is zero.
        /// </summary>
        /// <param name="phraseSlop"></param>
        /// <returns></returns>
        public FieldQueryBuilder PhraseSlop(int phraseSlop)
        {
            _phraseSlop = phraseSlop;
            _extraSet = true;
            return this;
        }

        /// <summary>
        /// Set to <tt>true</tt> to enable analysis on wildcard and prefix queries.
        /// </summary>
        /// <param name="analyzeWildcard"></param>
        /// <returns></returns>
        public FieldQueryBuilder AnalyzeWildcard(bool analyzeWildcard)
        {
            _analyzeWildcard = analyzeWildcard;
            _extraSet = true;
            return this;
        }

        public FieldQueryBuilder Rewrite(string rewrite)
        {
            _rewrite = rewrite;
            _extraSet = true;
            return this;
        }


        public FieldQueryBuilder MinimumShouldMatch(string minimumShouldMatch)
        {
            _minimumShouldMatch = minimumShouldMatch;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME), new JObject());

            if (!_extraSet)
            {
                content[NAME][_name] = new JValue(_query);
            }
            else
            {
                content[NAME][_name] = new JObject();
                content[NAME][_name]["query"] = new JValue(_query);

                if (_defaultOperator != null)
                {
                    content[NAME][_name]["default_operator"] = _defaultOperator.Value.ToString().ToLower();
                }

                if (_analyzer != null)
                {
                    content[NAME][_name]["analyzer"] = _analyzer;
                }

                if (_autoGeneratePhraseQueries)
                {
                    content[NAME][_name]["auto_generate_phrase_queries"] = _autoGeneratePhraseQueries;
                }

                if (_allowLeadingWildcard)
                {
                    content[NAME][_name]["allow_leading_wildcard"] = _allowLeadingWildcard;
                }

                if (_lowercaseExpandedTerms)
                {
                    content[NAME][_name]["lowercase_expanded_terms"] = _lowercaseExpandedTerms;
                }

                if (_enablePositionIncrements)
                {
                    content[NAME][_name]["enable_position_increments"] = _enablePositionIncrements;
                }

                if (_fuzzyMinSim != null)
                {
                    content[NAME][_name]["fuzzy_min_sim"] = _fuzzyMinSim;
                }

                if (_fuzzyPrefixLength != null)
                {
                    content[NAME][_name]["fuzzy_prefix_length"] = _fuzzyPrefixLength;
                }

                if (_phraseSlop != null)
                {
                    content[NAME][_name]["phrase_slop"] = _phraseSlop;
                }

                if (_analyzeWildcard)
                {
                    content[NAME][_name]["analyze_wildcard"] = _analyzeWildcard;
                }

                if (_boost != null)
                {
                    content[NAME][_name]["boost"] = _boost;
                }

                if (_rewrite != null)
                {
                    content[NAME][_name]["rewrite"] = _rewrite;
                }

                if (_minimumShouldMatch != null)
                {
                    content[NAME][_name]["minimum_should_match"] = _minimumShouldMatch;
                }
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
