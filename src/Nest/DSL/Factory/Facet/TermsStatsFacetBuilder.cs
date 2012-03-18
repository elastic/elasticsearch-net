using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Facet
{
    public class TermsStatsFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.TermsStatsFacetBuilder;
        private string _keyField;
        private string _valueField;
        private int? _size;
        private TermsStatsComparatorType? _comparatorType;
        private string _script;
        private string _lang;
        private Dictionary<string, object> _params;

        /// <summary>
        /// Constructs a new terms stats facet builder under the provided facet name.
        /// </summary>
        /// <param name="name"></param>
        public TermsStatsFacetBuilder(string name) : base(name) { }

        public TermsStatsFacetBuilder KeyField(string keyField)
        {
            _keyField = keyField;
            return this;
        }

        public TermsStatsFacetBuilder ValueField(string valueField)
        {
            _valueField = valueField;
            return this;
        }

        /// <summary>
        /// The order by which to return the facets by. Defaults to COUNT.
        /// </summary>
        /// <param name="comparatorType"></param>
        /// <returns></returns>
        public TermsStatsFacetBuilder Order(TermsStatsComparatorType comparatorType)
        {
            _comparatorType = comparatorType;
            return this;
        }

        /// <summary>
        /// Sets the size of the result.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public TermsStatsFacetBuilder Size(int size)
        {
            _size = size;
            return this;
        }

        /// <summary>
        /// Marks all terms to be returned, even ones with 0 counts.
        /// </summary>
        /// <returns></returns>
        public TermsStatsFacetBuilder AllTerms()
        {
            _size = 0;
            return this;
        }

        /// <summary>
        /// A value script to be executed (instead of value field) which results (numeric) will be used
        /// to compute the totals.
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public TermsStatsFacetBuilder ValueScript(string script)
        {
            _script = script;
            return this;
        }

        /// <summary>
        /// The language of the script.
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public TermsStatsFacetBuilder Script(string script)
        {
            _script = script;
            return this;
        }

        ///<summary>
        /// A parameter that will be passed to the script.
        /// </summary>
        /// <param name="name">The name of the script parameter.</param>
        /// <param name="value">The value of the script parameter.</param>
        /// <returns></returns>
        public TermsStatsFacetBuilder Param(string name, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }
            _params.Add(name, value);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_keyField == null)
            {
                throw new SearchBuilderException("key field must be set on terms facet for facet [" + _name + "]");
            }

            if(_valueField == null && _script == null)
            {
                throw new SearchBuilderException("value field or value script must be set on terms facet for facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["key_field"] = _keyField;

            if(_valueField != null)
            {
                content[NAME]["value_field"] = _valueField;    
            }

            if(_script != null)
            {
                content[NAME]["value_script"] = _script;

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
            }

            if(_comparatorType != null)
            {
                content[NAME]["order"] = _comparatorType.Value.ToString().ToLower();
            }

            if(_size != null)
            {
                content[NAME]["size"] = _size;
            }

            content = (JObject)AddFilterFacetAndGlobal(content);

            return content;
        }
    }
}