using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// A facet builder of date histogram facets.
    /// </summary>
    public class DateHistogramFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.DateHistogramFacetBuilder;
        private string _keyFieldName;
        private string _valueFieldName;
        private string _interval;
        private string _zone;
        private DateHistogramComparatorType? _comparatorType;
        private string _valueScript;
        private Dictionary<string, object> _params;
        private string _lang;

        /// <summary>
        /// Constructs a new date histogram facet with the provided facet logical name.
        /// </summary>
        /// <param name="name"></param>
        public DateHistogramFacetBuilder(string name) : base(name) { }

        /// <summary>
        /// The field name to perform the histogram facet. Translates to perform the histogram facet
        /// using the provided field as both the {@link #keyField(string)} and {@link #valueField(string)}.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public DateHistogramFacetBuilder Field(string field)
        {
            _keyFieldName = field;
            return this;
        }

        /// <summary>
        /// The field name to use in order to control where the hit will "fall into" within the histogram
        /// entries. Essentially, using the key field numeric value, the hit will be "rounded" into the relevant
        /// bucket controlled by the interval.
        /// </summary>
        /// <param name="keyField"></param>
        /// <returns></returns>
        public DateHistogramFacetBuilder KeyField(string keyField)
        {
            _keyFieldName = keyField;
            return this;
        }

        /// <summary>
        /// The field name to use as the value of the hit to compute data based on values within the interval
        /// (for example, total).
        /// </summary>
        /// <param name="valueField"></param>
        /// <returns></returns>
        public DateHistogramFacetBuilder ValueField(string valueField)
        {
            _valueFieldName = valueField;
            return this;
        }

        public DateHistogramFacetBuilder ValueScript(string valueScript)
        {
            _valueScript = valueScript;
            return this;
        }

        public DateHistogramFacetBuilder Param(string name, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }
            _params.Add(name, value);
            return this;
        }

        /// <summary>
        /// The language of the value script.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public DateHistogramFacetBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        /// <summary>
        /// The interval used to control the bucket "size" where each key value of a hit will fall into. Check
        /// the docs for all available values.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public DateHistogramFacetBuilder Interval(string interval)
        {
            _interval = interval;
            return this;
        }

        /// <summary>
        /// Sets the time zone to use when bucketing the values. Can either be in the form of "-10:00" or
        /// one of the values listed here: http://joda-time.sourceforge.net/timezones.html.
        /// </summary>
        /// <param name="zone"></param>
        /// <returns></returns>
        public DateHistogramFacetBuilder Zone(string zone)
        {
            _zone = zone;
            return this;
        }

        public DateHistogramFacetBuilder Comparator(DateHistogramComparatorType comparatorType)
        {
            _comparatorType = comparatorType;
            return this;
        }

        /// <summary>
        /// Should the facet run in global mode (not bounded by the search query) or not (bounded by
        /// the search query). Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="global"></param>
        /// <returns></returns>
        public new DateHistogramFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new DateHistogramFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new DateHistogramFacetBuilder FacetFilter(IFilterBuilder filter)
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
        public new  DateHistogramFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(string.IsNullOrEmpty(_keyFieldName))
            {
                throw new SearchBuilderException("field must be set on date histogram facet for facet [" + _name + "]");
            }

            if(string.IsNullOrEmpty(_interval))
            {
                throw new SearchBuilderException("interval must be set on date histogram facet for facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            if(!string.IsNullOrEmpty(_valueFieldName))
            {
                content[NAME]["key_field"] = _keyFieldName;
                content[NAME]["value_field"] = _valueFieldName;
            }
            else
            {
                content[NAME]["field"] = _keyFieldName;    
            }

            if(!string.IsNullOrEmpty(_valueScript))
            {
                content[NAME]["value_script"] = _valueScript;
                
                if(!string.IsNullOrEmpty(_lang))
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
            }

            content[NAME]["interval"] = _interval;

            if(!string.IsNullOrEmpty(_zone))
            {
                content[NAME]["time_zone"] = _zone;
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

