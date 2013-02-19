using System;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// A query that simply applies the boost factor to another query (multiply it).
    /// </summary>
    public class CustomBoostFactorQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.CustomBoostFactorQueryBuilder;
        private readonly IQueryBuilder _queryBuilder;
        private float? _boostFactor;

        /// <summary>
        /// A query that simply applies the boost factor to another query (multiply it).
        /// </summary>
        /// <param name="queryBuilder">The query to apply the boost factor to.</param>
        public CustomBoostFactorQueryBuilder(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Sets the boost factor for this query.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public CustomBoostFactorQueryBuilder BoostFactor(float boost)
        {
            _boostFactor = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["query"] = _queryBuilder.ToJsonObject() as JObject;

            if(_boostFactor != null)
            {
                content[NAME]["boost_factor"] = _boostFactor;
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