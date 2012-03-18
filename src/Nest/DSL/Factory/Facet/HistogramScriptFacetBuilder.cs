using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Filter;

namespace Nest.FactoryDsl.Facet
{
    public class HistogramScriptFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.HistogramScriptFacetBuilder;
        private string _lang;
        private string _keyFieldName;
        private string _keyScript;
        private string _valueScript;
        private Dictionary<string, object> _params;
        private long? _interval;
        private HistogramComparatorType? _comparatorType;
        private object _from;
        private object _to;

        public HistogramScriptFacetBuilder(string name) : base(name) { }

        /// <summary>
        /// The language of the script.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public HistogramScriptFacetBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        public HistogramScriptFacetBuilder KeyField(string keyFieldName)
        {
            _keyFieldName = keyFieldName;
            return this;
        }

        public HistogramScriptFacetBuilder KeyScript(string keyScript)
        {
            _keyScript = keyScript;
            return this;
        }

        public HistogramScriptFacetBuilder ValueScript(string valueScript)
        {
            _valueScript = valueScript;
            return this;
        }

        public HistogramScriptFacetBuilder Interval(long interval)
        {
            _interval = interval;
            return this;
        }

        public HistogramScriptFacetBuilder Comparator(HistogramComparatorType comparatorType)
        {
            _comparatorType = comparatorType;
            return this;
        }
        ///<summary>
        /// Parameters for {@link #valueScript(string)} to improve performance when executing the same script with different parameters.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HistogramScriptFacetBuilder Param(string name, object value)
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
        public new HistogramScriptFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new HistogramScriptFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new HistogramScriptFacetBuilder FacetFilter(IFilterBuilder filter)
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
        public new HistogramScriptFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_keyScript == null && _keyFieldName == null)
            {
                throw new SearchBuilderException("key_script or key_field must be set on histogram script facet for facet [" + _name + "]");
            }

            if(_valueScript == null)
            {
                throw new SearchBuilderException("value_script must be set on histogram script facet for facet [" + _name + "]");    
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            if(_keyFieldName != null)
            {
                content[NAME]["key_field"] = _keyFieldName;
            }
            else if (_keyScript != null)
            {
                content[NAME]["key_script"] = _keyScript;
            }

            content[NAME]["value_script"] = _valueScript;

            if(_from != null && _to != null)
            {
                content[NAME]["from"] = new JValue(_from);
                content[NAME]["to"] = new JValue(_to);
            }

            if(_lang != null)
            {
                content[NAME]["lang"] = _lang;
            }

            if (_interval > 0) // interval is optional in script facet, can be defined by the key script
            {
                content[NAME]["interval"] = _interval;
            }

            if (_params != null)
            {
                content[NAME]["params"] = new JObject();

                foreach (var param in _params)
                {
                    content[NAME]["params"][param.Key] = new JValue(param.Value);
                }
            }

            if(_comparatorType != null)
            {
                content[NAME]["comparator"] = _comparatorType.Value.ToString().ToLower();
            }

            content = (JObject)AddFilterFacetAndGlobal(content);

            return content;
        }
    }
}