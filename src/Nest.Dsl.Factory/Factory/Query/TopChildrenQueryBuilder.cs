using System;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class TopChildrenQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.TopChildrenQueryBuilder;
        private readonly IQueryBuilder _queryBuilder;
        private readonly string _childType;
        private string _scope;
        private string _score;
        private float? _boost = 1.0f;
        private int? _factor;
        private int? _incrementalFactor;

        public TopChildrenQueryBuilder(string type, IQueryBuilder queryBuilder)
        {
            _childType = type;
            _queryBuilder = queryBuilder;    
        }

        /// <summary>
        /// The scope of the query, which can later be used, for example, to run facets against the child docs that
        /// matches the query.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public TopChildrenQueryBuilder Scope(string scope)
        {
            _scope = scope;
            return this;
        }

        /// <summary>
        /// How to compute the score. Possible values are: <tt>max</tt>, <tt>sum</tt>, or <tt>avg</tt>. Defaults
        /// to <tt>max</tt>.
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public TopChildrenQueryBuilder Score(string score)
        {
            _score = score;
            return this;
        }

        /// <summary>
        /// Controls the multiplication factor of the initial hits required from the child query over the main query request.
        /// Defaults to 5.
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public TopChildrenQueryBuilder Factor(int factor)
        {
            _factor = factor;
            return this;
        }

        /// <summary>
        /// Sets the incremental factor when the query needs to be re-run in order to fetch more results. Defaults to 2.
        /// </summary>
        /// <param name="incrementalFactor"></param>
        /// <returns></returns>
        public TopChildrenQueryBuilder IncrementalFactor(int incrementalFactor)
        {
            _incrementalFactor = incrementalFactor;
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public TopChildrenQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            
            content[NAME]["query"] = _queryBuilder.ToJsonObject() as JObject;

            content[NAME]["type"] = _childType;

            if(_scope != null)
            {
                content[NAME]["_scope"] = _scope;
            }

            if (_score != null)
            {
                content[NAME]["score"] = _score;
            }

            if (_boost != null)
            {
                content[NAME]["boost"] = _boost;
            }

            if (_factor != null)
            {
                content[NAME]["factor"] = _factor;
            }

            if (_incrementalFactor != null)
            {
                content[NAME]["incremental_factor"] = _incrementalFactor;
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