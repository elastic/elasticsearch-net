using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    public class HasChildQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.HasChildQueryBuilder;
        private readonly IQueryBuilder _queryBuilder;
        private readonly string _childType;
        private string _scope;
        private float _boost = 1.0f;

        public HasChildQueryBuilder(string childType, IQueryBuilder query)
        {
            _childType = childType;
            _queryBuilder = query;
        }

        /// <summary>
        /// The scope of the query, which can later be used, for example, to run facets against the child docs that
        /// matches the query.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public HasChildQueryBuilder Scope(string scope)
        {
            _scope = scope;
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public HasChildQueryBuilder Boost(float boost)
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

            if (Math.Abs(_boost - 1.0f) > float.Epsilon)
            {
                content[NAME]["boost"] = _boost; 
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