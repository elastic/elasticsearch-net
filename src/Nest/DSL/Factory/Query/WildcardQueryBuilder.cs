using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// Implements the wildcard search query. Supported wildcards are <tt>*</tt>, which
    /// matches any character sequence (including the empty one), and <tt>?</tt>,
    /// which matches any single character. Note this query can be slow, as it
    /// needs to iterate over many terms. In order to prevent extremely slow WildcardQueries,
    /// a Wildcard term should not start with one of the wildcards <tt>*</tt> or
    /// <tt>?</tt>.
    /// </summary>
    public class WildcardQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.WildcardQueryBuilder;
        private readonly string _name;
        private readonly string _wildcard;
        private float? _boost;
        private string _rewrite;

        /// <summary>
        /// Implements the wildcard search query. Supported wildcards are <tt>*</tt>, which
        /// matches any character sequence (including the empty one), and <tt>?</tt>,
        /// which matches any single character. Note this query can be slow, as it
        /// needs to iterate over many terms. In order to prevent extremely slow WildcardQueries,
        /// a Wildcard term should not start with one of the wildcards <tt>*</tt> or
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="query">The wildcard query string</param>
        public WildcardQueryBuilder(string name, string query)
        {
            _name = name;
            _wildcard = query;
        }

        public WildcardQueryBuilder Rewrite(string rewrite)
        {
            _rewrite = rewrite;
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public WildcardQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            
            if(_boost == null && _rewrite == null)
            {
                content[NAME][_name] = _wildcard;
            }
            else
            {
                content[NAME][_name] = new JObject();

                content[NAME][_name]["wildcard"] = _wildcard;

                if(_boost != null)
                {
                    content[NAME][_name]["boost"] = _boost;
                }

                if(!string.IsNullOrEmpty(_rewrite))
                {
                    content[NAME][_name]["rewrite"] = _rewrite;
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