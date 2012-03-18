using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Filter;

namespace Nest.FactoryDsl.Facet
{
    /// <summary>
    /// A facet builder of range facets.
    /// </summary>
    public class RangeFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.RangeFacetBuilder;
        private string _keyFieldName;
        private string _valueFieldName;
        private List<Entry> _entries = new List<Entry>();

        /// <summary>
        /// Constructs a new range facet with the provided facet logical name.
        /// </summary>
        /// <param name="name">The logical name of the facet</param>
        public RangeFacetBuilder(string name) : base(name) { }

        /// <summary>
        /// The field name to perform the range facet. Translates to perform the range facet
        /// using the provided field as both the {@link #keyField(String)} and {@link #valueField(String)}.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public RangeFacetBuilder Field(string field)
        {
            _keyFieldName = field;
            _valueFieldName = field;
            return this;
        }

        /// <summary>
        /// The field name to use in order to control where the hit will "fall into" within the range
        /// entries. Essentially, using the key field numeric value, the hit will be "rounded" into the relevant
        /// bucket controlled by the interval.
        /// </summary>
        /// <param name="keyField"></param>
        /// <returns></returns>
        public RangeFacetBuilder KeyField(string keyField)
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
        public RangeFacetBuilder ValueField(string valueField)
        {
            _valueFieldName = valueField;
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit from and to.
        /// </summary>
        /// <param name="from">The from range limit</param>
        /// <param name="to">The to range limit</param>
        /// <returns></returns>
        public RangeFacetBuilder AddRange(double from, double to)
        {
            _entries.Add(new Entry(from, to));
            return this;
        }

        public RangeFacetBuilder AddRange(string from, string to)
        {
            _entries.Add(new Entry(from, to));
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit from and unbounded to.
        /// </summary>
        /// <param name="from">the from range limit, to is unbounded.</param>
        /// <returns></returns>
        public RangeFacetBuilder AddUnboundedTo(double from)
        {
            _entries.Add(new Entry(from, double.PositiveInfinity));
            return this;
        }

        public RangeFacetBuilder AddUnboundedTo(string from)
        {
            _entries.Add(new Entry(from, null));
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit to and unbounded from.
        /// </summary>
        /// <param name="to">The to range limit, from is unbounded.</param>
        /// <returns></returns>
        public RangeFacetBuilder AddUnboundedFrom(double to)
        {
            _entries.Add(new Entry(double.NegativeInfinity, to));
            return this;
        }

        public RangeFacetBuilder AddUnboundedFrom(string to)
        {
            _entries.Add(new Entry(null, to));
            return this;
        }

        /// <summary>
        /// Should the facet run in global mode (not bounded by the search query) or not (bounded by
        /// the search query). Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="global"></param>
        /// <returns></returns>
        public new RangeFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new RangeFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new RangeFacetBuilder FacetFilter(IFilterBuilder filter)
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
        public new RangeFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_keyFieldName == null)
            {
                throw new SearchBuilderException("field must be set on range facet for facet [" + _name + "]");
            }

            if(_entries.Count == 0)
            {
                throw new SearchBuilderException("at least one range must be defined for range facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            if(_valueFieldName != null && !_keyFieldName.Equals(_valueFieldName))
            {
                content[NAME]["key_field"] = _keyFieldName;
                content[NAME]["value_field"] = _valueFieldName;
            }
            else
            {
                content[NAME]["field"] = _keyFieldName;    
            }

            var ranges = new List<JObject>();

            foreach (var entry in _entries)
            {
                var range = new JObject();

                if(entry.FromAsString != null)
                {
                    range["from"] = entry.FromAsString;    
                }
                else if (!double.IsInfinity(entry.From))
                {
                    range["from"] = entry.From;
                }

                if(entry.ToAsString != null)
                {
                    range["to"] = entry.ToAsString;    
                }
                else if (!double.IsInfinity(entry.To))
                {
                    range["to"] = entry.To;
                }

                ranges.Add(range);
            }

            content[NAME]["ranges"] = new JArray(ranges);

            content = (JObject)AddFilterFacetAndGlobal(content);

            return content;
        }

        private class Entry
        {
            public readonly double From = double.NegativeInfinity;
            public readonly double To = double.PositiveInfinity;

            public string FromAsString;
            public string ToAsString;

            public Entry(string fromAsString, string toAsString)
            {
                FromAsString = fromAsString;
                ToAsString = toAsString;
            }

            public Entry(double from, double to)
            {
                From = from;
                To = to;
            }
        }
    }
}
