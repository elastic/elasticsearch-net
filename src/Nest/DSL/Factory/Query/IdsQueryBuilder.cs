using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A query that will return only documents matching specific ids (and a type).
    /// </summary>
    public class IdsQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.IdsQueryBuilder;
        private readonly List<string> _types;
        private List<string> _values = new List<String>();
        private float? _boost;

        public IdsQueryBuilder(params string[] types)
        {
            _types = types == null ? null : new List<string>(types);
        }

        /// <summary>
        /// Adds ids to the filter.
        /// </summary>
        /// <param name="?"></param>
        /// <param name="ids"> </param>
        /// <returns></returns>
        public IdsQueryBuilder AddIds(params string[] ids)
        {
            _values.AddRange(ids);
            return this;
        }

        /// <summary>
        /// Adds ids to the filter.
        /// </summary>
        /// <param name="?"></param>
        /// <param name="ids"> </param>
        /// <returns></returns>
        public IdsQueryBuilder Ids(params string[] ids)
        {
            return AddIds(ids);
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public IdsQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if(_types != null)
            {
                if(_types.Count == 1)
                {
                    content[NAME]["type"] = _types[0];
                }
                else
                {
                    content[NAME]["types"] = new JArray(_types);
                }
            }

            content[NAME]["values"] = new JArray(_values);

            if (_boost != null)
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