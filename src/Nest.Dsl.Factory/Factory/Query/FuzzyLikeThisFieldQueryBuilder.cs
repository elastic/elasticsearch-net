using System;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class FuzzyLikeThisFieldQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.FuzzyLikeThisFieldQueryBuilder;
        private readonly string _name;
        private float? _boost;
        private string _likeText;
        private float? _minSimilarity;
        private int? _prefixLength;
        private int? _maxQueryTerms;
        private bool _ignoreTf;
        private string _analyzer;

        /// <summary>
        /// A fuzzy more like this query on the provided field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        public FuzzyLikeThisFieldQueryBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// The text to use in order to find documents that are "like" this.
        /// </summary>
        /// <param name="likeText"></param>
        /// <returns></returns>
        public FuzzyLikeThisFieldQueryBuilder LikeText(string likeText)
        {
            _likeText = likeText;
            return this;
        }

        public FuzzyLikeThisFieldQueryBuilder MinSimilarity(float minSimilarity)
        {
            _minSimilarity = minSimilarity;
            return this;
        }

        public FuzzyLikeThisFieldQueryBuilder PrefixLength(int prefixLength)
        {
            _prefixLength = prefixLength;
            return this;
        }

        public FuzzyLikeThisFieldQueryBuilder MaxQueryTerms(int maxQueryTerms)
        {
            _maxQueryTerms = maxQueryTerms;
            return this;
        }

        public FuzzyLikeThisFieldQueryBuilder IgnoreTf(bool ignoreTf)
        {
            _ignoreTf = ignoreTf;
            return this;
        }

        /// <summary>
        /// The analyzer that will be used to analyze the text. Defaults to the analyzer associated with the field.
        /// </summary>
        /// <param name="analyzer"></param>
        /// <returns></returns>
        public FuzzyLikeThisFieldQueryBuilder Analyzer(String analyzer)
        {
            _analyzer = analyzer;
            return this;
        }

        public FuzzyLikeThisFieldQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME][_name] = new JObject();

            if (string.IsNullOrEmpty(_likeText))
            {
                throw new QueryBuilderException("fuzzyLikeThis requires 'likeText' to be provided");
            }

            content[NAME][_name]["like_text"] = _likeText;

            if (_maxQueryTerms != null)
            {
                content[NAME][_name]["max_query_terms"] = _maxQueryTerms;
            }

            if (_minSimilarity != null)
            {
                content[NAME][_name]["min_similarity"] = _minSimilarity;
            }

            if (_prefixLength != null)
            {
                content[NAME][_name]["prefix_length"] = _prefixLength;
            }

            if (_ignoreTf)
            {
                content[NAME][_name]["ignore_tf"] = _ignoreTf;
            }

            if (_boost != null)
            {
                content[NAME][_name]["boost"] = _boost;
            }

            if (!string.IsNullOrEmpty(_analyzer))
            {
                content[NAME][_name]["analyzer"] = _analyzer;
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