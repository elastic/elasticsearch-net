using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class StatisticalScriptFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.StatisticalScriptFacetBuilder;
        private string _lang;
        private string _script;
        private Dictionary<string, object> _params;

        public StatisticalScriptFacetBuilder(string name) : base(name) { }

        /// <summary>
        /// The language of the script.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public StatisticalScriptFacetBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        public StatisticalScriptFacetBuilder Script(string script)
        {
            _script = script;
            return this;
        }

        ///<summary>
        /// Parameters for {@link #valueScript(string)} to improve performance when executing the same script with different parameters.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public StatisticalScriptFacetBuilder Param(string name, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }
            _params.Add(name, value);
            return this;
        }

        /// <summary>
        /// Should the facet run in global mode (not bounded by the search query) or not (bounded by
        /// the search query). Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="global"></param>
        /// <returns></returns>
        public new StatisticalScriptFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new StatisticalScriptFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new StatisticalScriptFacetBuilder FacetFilter(IFilterBuilder filter)
        {
            base.FacetFilter(filter);
            return this;
        }

        /// <summary>
        /// Sets the nested path the facet will execute on. A match (root object) will then cause all the
        /// nested objects matching the path to be computed into the facet.
        /// </summary>
        /// <param name="nested"></param>
        /// <returns></returns>
        public new StatisticalScriptFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_script == null)
            {
                throw new SearchBuilderException("script must be set on statistical script facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["script"] = _script;

            if(_lang != null)
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

            content = (JObject)AddFilterFacetAndGlobal(content);

            return content;
        }
    }
}
