using System;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// A Query that does fuzzy matching for a specific value.
    /// </summary>
    public class FuzzyQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.FuzzyQueryBuilder;
        private readonly string _name;
        private readonly object _value;
        private float? _boost;
        private string _minSimilarity;
        private int? _prefixLength;

        /// <summary>
        /// Constructs a new term query.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        public FuzzyQueryBuilder(string name, object value)
        {
            _name = name;
            _value = value;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public FuzzyQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        public FuzzyQueryBuilder MinSimilarity(float defaultMinSimilarity)
        {
            _minSimilarity = defaultMinSimilarity.ToString();
            return this;
        }

        public FuzzyQueryBuilder PrefixLength(int prefixLength)
        {
            _prefixLength = prefixLength;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if (_boost == null && _minSimilarity == null && _prefixLength == null)
            {
                content[NAME][_name] = new JValue(_value);
            }
            else
            {
                content[NAME][_name] = new JObject();
                content[NAME][_name]["value"] = new JValue(_value);

                if (_boost != null)
                {
                    content[NAME][_name]["boost"] = _boost;
                }

                if (_minSimilarity != null)
                {
                    content[NAME][_name]["min_similarity"] = _minSimilarity;
                }

                if (_prefixLength != null)
                {
                    content[NAME][_name]["prefix_length"] = _prefixLength;
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