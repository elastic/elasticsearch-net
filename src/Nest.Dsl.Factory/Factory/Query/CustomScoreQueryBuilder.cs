using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// A query that uses a script to compute the score.
    /// </summary>
    public class CustomScoreQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.CustomScoreQueryBuilder;
        private readonly IQueryBuilder _queryBuilder;
        private string _script;
        private string _lang;
        private float? _boost;
        private Dictionary<string, object> _params = null;

        /// <summary>
        /// A query that simply applies the boost factor to another query (multiply it).
        /// </summary>
        /// <param name="queryBuilder">The query to apply the boost factor to.</param>
        public CustomScoreQueryBuilder(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public CustomScoreQueryBuilder Script(string script)
        {
            _script = script;
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public CustomScoreQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        /// <summary>
        /// Sets the language of the script.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public CustomScoreQueryBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        /// <summary>
        /// Additional parameters that can be provided to the script.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public CustomScoreQueryBuilder Params(Dictionary<string, object> parameters)
        {
            if (_params == null)
            {
                _params = parameters;
            }
            else
            {
                _params = (Dictionary<string, object>)_params.Concat(parameters.Where(kvp => !_params.ContainsKey(kvp.Key)));
            }
            return this;
        }

        /// <summary>
        /// Additional parameters that can be provided to the script.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public CustomScoreQueryBuilder Param(string name, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }

            _params.Add(name, value);

            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME), new JObject());
            content[NAME]["query"] = _queryBuilder.ToJsonObject() as JObject;
            content[NAME]["script"] = _script;

            if (_lang != null)
            {
                content[NAME]["lang"] = _lang;
            }

            if (_params != null)
            {
                content[NAME]["params"] = new JObject();

                foreach (var param in _params)
                {
                    content[NAME]["params"][param.Key] = new JValue(param.Value);
                }
            }

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