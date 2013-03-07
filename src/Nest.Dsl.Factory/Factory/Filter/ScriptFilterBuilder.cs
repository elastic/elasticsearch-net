using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class ScriptFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.ScriptFilterBuilder;
        private readonly string _script;
        private bool _cache;
        private string _cacheKey;
        private string _filterName;
        private string _lang;
        private Dictionary<string, object> _params;

        public ScriptFilterBuilder(string script)
        {
            _script = script;
        }

        public ScriptFilterBuilder AddParam(string name, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }

            _params.Add(name, value);

            return this;
        }

        public ScriptFilterBuilder Params(Dictionary<string, object> parameters)
        {
            if (_params == null)
            {
                _params = parameters;
            }
            else
            {
                _params = (Dictionary<string, object>) _params.Concat(parameters.Where(kvp => !_params.ContainsKey(kvp.Key)));
            }
            return this;
        }

        /// <summary>
        /// Sets the script language.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public ScriptFilterBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        public ScriptFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        public ScriptFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public ScriptFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject(new JProperty("script", _script))));

            if (_params != null)
            {
                content[NAME]["params"] = new JObject();

                foreach (var param in _params)
                {
                    content[NAME]["params"][param.Key] = new JValue(param.Value);
                }
            }

            if (_lang != null)
            {
                content[NAME]["lang"] = _lang;
            }

            if (_filterName != null)
            {
                content[NAME]["_name"] = _filterName;
            }

            if (_cache)
            {
                content[NAME]["_cache"] = _cache;
            }

            if (_cacheKey != null)
            {
                content[NAME]["_cache_key"] = _cacheKey;
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