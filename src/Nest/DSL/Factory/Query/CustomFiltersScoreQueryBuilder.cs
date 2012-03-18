using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Filter;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A query that uses a filters with a script associated with them to compute the score.
    /// </summary>
    public class CustomFiltersScoreQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.CustomFilterScoreQueryBuilder;
        private readonly IQueryBuilder _queryBuilder;
        private string _lang;
        private float? _boost;
        private Dictionary<string, object> _params;
        private string _scoreMode;
        private List<IFilterBuilder> _filters = new List<IFilterBuilder>();
        private List<string> _scripts = new List<string>();
        private List<float?> _boosts = new List<float?>();

        public CustomFiltersScoreQueryBuilder(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public CustomFiltersScoreQueryBuilder Add(IFilterBuilder filter, string script)
        {
            _filters.Add(filter);
            _scripts.Add(script);
            _boosts.Add(null);
            return this;
        }

        public CustomFiltersScoreQueryBuilder Add(IFilterBuilder filter, float boost)
        {
            _filters.Add(filter);
            _scripts.Add(null);
            _boosts.Add(boost);
            return this;
        }

        public CustomFiltersScoreQueryBuilder ScoreMode(string scoreMode)
        {
            _scoreMode = scoreMode;
            return this;
        }

        /// <summary>
        /// Sets the language of the script.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public CustomFiltersScoreQueryBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        /// <summary>
        /// Additional parameters that can be provided to the script.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public CustomFiltersScoreQueryBuilder Params(Dictionary<string, object> parameters)
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
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public CustomFiltersScoreQueryBuilder Param(string key, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }
            _params.Add(key, value);
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public CustomFiltersScoreQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            content[NAME]["query"] = _queryBuilder.ToJsonObject() as JObject;
            content[NAME]["filters"] = new JArray();

            for (int index = 0; index < _filters.Count; index++)
            {
                var filter = _filters[index];
                var script = _scripts[index];
                var boost = _boosts[index];

                ((JArray)content.SelectToken(NAME + ".filters")).Add(new JObject(new JProperty("filter", filter.ToJsonObject()))); 

                if(script != null)
                {
                    content[NAME]["filters"][index]["script"] = script;    
                }
                else
                {
                    content[NAME]["filters"][index]["boost"] = boost;    
                }
            }

            if(_scoreMode != null)
            {
                content[NAME]["score_mode"] = _scoreMode;
            }

            if(_lang != null)
            {
                content[NAME]["lang"] = _lang;
            }

            if(_params != null)
            {
                content[NAME]["params"] = new JObject();

                foreach (var param in _params)
                {
                    content[NAME]["params"][param.Key] = new JValue(param.Value);
                }
            }

            if(_boost != null)
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