using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    public class FuzzyLikeThisQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.FuzzyLikeThisQueryBuilder;
        private readonly string[] _fields;
        private float? _boost;
        private string _likeText;
        private float? _minSimilarity;
        private int? _prefixLength;
        private int? _maxQueryTerms;
        private bool _ignoreTf;
        private string _analyzer;

        /// <summary>
        /// Constructs a new fuzzy like this query which uses the "_all" field.
        /// </summary>
        public FuzzyLikeThisQueryBuilder()
        {
            _fields = null;
        }

        /// <summary>
        /// Sets the field names that will be used when generating the 'Fuzzy Like This' query.
        /// </summary>
        /// <param name="fields">The field names that will be used when generating the 'Fuzzy Like This' query.</param>
        public FuzzyLikeThisQueryBuilder(params string[] fields)
        {
            _fields = fields;
        }

        /// <summary>
        /// The text to use in order to find documents that are "like" this.
        /// </summary>
        /// <param name="likeText"></param>
        /// <returns></returns>
        public FuzzyLikeThisQueryBuilder LikeText(string likeText)
        {
            _likeText = likeText;
            return this;
        }

        public FuzzyLikeThisQueryBuilder MinSimilarity(float minSimilarity)
        {
            _minSimilarity = minSimilarity;
            return this;
        }

        public FuzzyLikeThisQueryBuilder PrefixLength(int prefixLength)
        {
            _prefixLength = prefixLength;
            return this;
        }

        public FuzzyLikeThisQueryBuilder MaxQueryTerms(int maxQueryTerms)
        {
            _maxQueryTerms = maxQueryTerms;
            return this;
        }

        public FuzzyLikeThisQueryBuilder IgnoreTf(bool ignoreTF)
        {
            _ignoreTf = ignoreTF;
            return this;
        }

        /// <summary>
        /// The analyzer that will be used to analyze the text. Defaults to the analyzer associated with the fied.
        /// </summary>
        /// <param name="analyzer"></param>
        /// <returns></returns>
        public FuzzyLikeThisQueryBuilder Analyzer(string analyzer)
        {
            _analyzer = analyzer;
            return this;
        }

        public FuzzyLikeThisQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if (_fields != null)
            {
                content[NAME]["fields"] = new JArray(_fields);
            }

            if (string.IsNullOrEmpty(_likeText))
            {
                throw new QueryBuilderException("fuzzyLikeThis requires 'likeText' to be provided");
            }

            content[NAME]["like_text"] = _likeText;

            if (_maxQueryTerms != null)
            {
                content[NAME]["max_query_terms"] = _maxQueryTerms;
            }

            if (_minSimilarity != null)
            {
                content[NAME]["min_similarity"] = _minSimilarity;
            }

            if (_prefixLength != null)
            {
                content[NAME]["prefix_length"] = _prefixLength;
            }

            if (_ignoreTf)
            {
                content[NAME]["ignore_tf"] = _ignoreTf;
            }

            if (_boost != null)
            {
                content[NAME]["boost"] = _boost;
            }

            if (_analyzer != null)
            {
                content[NAME]["analyzer"] = _analyzer;
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
