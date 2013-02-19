using System;
using Newtonsoft.Json.Linq;
using Nest.Dsl.Factory;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// A facet builder of histogram facets.
    /// </summary>
    public class HistogramFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.HistogramFacetBuilder;
        private string _keyFieldName;
        private string _valueFieldName;
        private long? _interval;
        private HistogramComparatorType? _comparatorType;
        private object _from;
        private object _to;

        /// <summary>
        /// Constructs a new histogram facet with the provided facet logical name.
        /// </summary>
        /// <param name="name"></param>
        public HistogramFacetBuilder(string name) : base(name) { }

        /// <summary>
        /// The field name to perform the histogram facet. Translates to perform the histogram facet
        /// using the provided field as both the {@link #keyField(string)} and {@link #valueField(string)}.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public HistogramFacetBuilder Field(string field)
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
        public HistogramFacetBuilder KeyField(string keyField)
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
        public HistogramFacetBuilder ValueField(string valueField)
        {
            _valueFieldName = valueField;
            return this;
        }

        /// <summary>
        /// The interval used to control the bucket "size" where each key value of a hit will fall into.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public HistogramFacetBuilder Interval(long interval)
        {
            _interval = interval;
            return this;
        }

        /// <summary>
        /// The interval used to control the bucket "size" where each key value of a hit will fall into.
        /// </summary>
        /// <returns></returns>
        public HistogramFacetBuilder Interval(TimeSpan span)
        {
            return Interval((long)span.TotalMilliseconds);
        }

        /// <summary>
        /// Sets the bounds from and to for the facet. Both performs bounds check and includes only
        /// values within the bounds, and improves performance.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public HistogramFacetBuilder Bounds(object from, object to)
        {
            _from = from;
            _to = to;
            return this;
        }

        public HistogramFacetBuilder Comparator(HistogramComparatorType comparatorType)
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
        public new HistogramFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new HistogramFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new HistogramFacetBuilder FacetFilter(IFilterBuilder filter)
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
        public new HistogramFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_keyFieldName == null)
            {
                throw new SearchBuilderException("field must be set on histogram facet for facet [" + _name + "]");    
            }

            if(_interval < 0)
            {
                throw new SearchBuilderException("interval must be set on histogram facet for facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            if(_valueFieldName != null)
            {
                content[NAME]["key_field"] = _keyFieldName;
                content[NAME]["value_field"] = _valueFieldName;
            }
            else
            {
                content[NAME]["field"] = _keyFieldName;    
            }

            content[NAME]["interval"] = _interval;

            if(_from != null && _to != null)
            {
                content[NAME]["from"] = new JValue(_from);    
                content[NAME]["to"] = new JValue(_to);    
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
