using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class RangeScriptFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.RangeScriptFacetBuilder;
        private string _lang;
        private string _keyScript;
        private string _valueScript;
        private Dictionary<string, object> _params;
        private List<Entry> _entries = new List<Entry>();

        public RangeScriptFacetBuilder(string name) : base(name) { }

        /// <summary>
        /// The language of the script.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public RangeScriptFacetBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        public RangeScriptFacetBuilder KeyScript(string keyScript)
        {
            _keyScript = keyScript;
            return this;
        }

        public RangeScriptFacetBuilder ValueScript(string valueScript)
        {
            _valueScript = valueScript;
            return this;
        }

        ///<summary>
        /// A parameter that will be passed to the script.
        /// </summary>
        /// <param name="name">The name of the script parameter.</param>
        /// <param name="value">The value of the script parameter.</param>
        /// <returns></returns>
        public RangeScriptFacetBuilder Param(string name, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }
            _params.Add(name, value);
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit from and to.
        /// </summary>
        /// <param name="from">The from range limit</param>
        /// <param name="to">The to range limit</param>
        /// <returns></returns>
        public RangeScriptFacetBuilder AddRange(double from, double to)
        {
            _entries.Add(new Entry(from, to));
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit from and unbounded to.
        /// </summary>
        /// <param name="from">The from range limit, to is unbounded.</param>
        /// <returns></returns>
        public RangeScriptFacetBuilder AddUnboundedTo(double from)
        {
            _entries.Add(new Entry(from, double.PositiveInfinity));
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit to and unbounded from.
        /// </summary>
        /// <param name="to">The to range limit, from is unbounded.</param>
        /// <returns></returns>
        public RangeScriptFacetBuilder AddUnboundedFrom(double to)
        {
            _entries.Add(new Entry(double.NegativeInfinity, to));
            return this;
        }

        /// <summary>
        /// Should the facet run in global mode (not bounded by the search query) or not (bounded by
        /// the search query). Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="global"></param>
        /// <returns></returns>
        public new RangeScriptFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new RangeScriptFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new RangeScriptFacetBuilder FacetFilter(IFilterBuilder filter)
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
        public new RangeScriptFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_keyScript == null)
            {
                throw new SearchBuilderException("key_script must be set on range script facet for facet [" + _name + "]");
            }

            if(_valueScript == null)
            {
                throw new SearchBuilderException("value_script must be set on range script facet for facet [" + _name + "]");
            }

            if(_entries.Count == 0)
            {
                throw new SearchBuilderException("at least one range must be defined for range facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["key_script"] = _keyScript;
            content[NAME]["value_script"] = _valueScript;

            if (_lang != null)
            {
                content[NAME]["lang"] = _lang;
            }

            var ranges = new List<JObject>();

            foreach (var entry in _entries)
            {
                var range = new JObject();

                if (!double.IsInfinity(entry.From))
                {
                    range["from"] = entry.From;
                }

                if (!double.IsInfinity(entry.To))
                {
                    range["to"] = entry.To;
                }

                ranges.Add(range);
            }

            content[NAME]["ranges"] = new JArray(ranges);

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

        private class Entry
        {
            public readonly double From;
            public readonly double To;

            public Entry(double from, double to)
            {
                From = from;
                To = to;
            }
        }
    }
}
