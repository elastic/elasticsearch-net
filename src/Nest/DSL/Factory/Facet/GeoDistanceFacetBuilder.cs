using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Filter;

namespace Nest.FactoryDsl.Facet
{
    /// <summary>
    /// A geo distance builder allowing to create a facet of distances from a specific location including the
    /// number of hits within each distance range, and aggregated data (like totals of either the distance or
    /// custom value fields).
    /// </summary>
    public class GeoDistanceFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.GeoDistanceFacetBuilder;
        private string _fieldName;
        private string _valueFieldName;
        private double _lat;
        private double _lon;
        private string _geohash;
        private GeoDistance? _geoDistance;
        private DistanceUnit? _unit;
        private Dictionary<string, object> _params;
        private string _valueScript;
        private string _lang;
        private readonly List<Entry> _entries = new List<Entry>();

        /// <summary>
        /// Constructs a new geo distance with the provided facet name.
        /// </summary>
        /// <param name="name"></param>
        public GeoDistanceFacetBuilder(string name) : base(name) { }

        /// <summary>
        /// The geo point field that will be used to extract the document location(s).
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder Field(string fieldName)
        {
            _fieldName = fieldName;
            return this;
        }

        /// <summary>
        /// A custom value field (numeric) that will be used to provide aggregated data for each facet (for example, total).
        /// </summary>
        /// <param name="valueFieldName"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder ValueField(string valueFieldName)
        {
            _valueFieldName = valueFieldName;
            return this;
        }

        /// <summary>
        /// A custom value script (result is numeric) that will be used to provide aggregated data for each facet (for example, total).
        /// </summary>
        /// <param name="valueScript"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder ValueScript(string valueScript)
        {
            _valueScript = valueScript;
            return this;
        }

        /// <summary>
        /// The language of the {@link #valueScript(string)} script.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        /// <summary>
        /// Parameters for {@link #valueScript(string)} to improve performance when executing the same script with different parameters.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder ScriptParam(string name, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }
            _params.Add(name, value);
            return this;
        }

        /// <summary>
        /// The point to create the range distance facets from.
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder Point(double lat, double lon)
        {
            _lat = lat;
            _lon = lon;
            return this;
        }

        /// <summary>
        /// The latitude to create the range distance facets from.
        /// </summary>
        /// <param name="lat"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder Lat(double lat)
        {
            _lat = lat;
            return this;
        }

        /// <summary>
        /// The longitude to create the range distance facets from.
        /// </summary>
        /// <param name="lon"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder Lon(double lon) 
        {
            _lon = lon;
            return this;
        }

        /// <summary>
        /// The geohash of the geo point to create the range distance facets from.
        /// </summary>
        /// <param name="geohash"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder Geohash(string geohash)
        {
            _geohash = geohash;
            return this;
        }

        /// <summary>
        /// The geo distance type used to compute the distance.
        /// </summary>
        /// <param name="geoDistance"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder GeoDistance(GeoDistance geoDistance)
        {
            _geoDistance = geoDistance;
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit from and to.
        /// </summary>
        /// <param name="from">The from distance limit</param>
        /// <param name="to">The to distance limit</param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder AddRange(double from, double to)
        {
            _entries.Add(new Entry(from, to));
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit from and unbounded to.
        /// </summary>
        /// <param name="from">The from distance limit, to is unbounded.</param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder AddUnboundedTo(double from)
        {
            _entries.Add(new Entry(from, double.PositiveInfinity));
            return this;
        }

        /// <summary>
        /// Adds a range entry with explicit to and unbounded from.
        /// </summary>
        /// <param name="to">The to distance limit, from is unbounded.</param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder AddUnboundedFrom(double to)
        {
            _entries.Add(new Entry(double.NegativeInfinity, to));
            return this;
        }

        /// <summary>
        /// The distance unit to use. Defaults to {@link org.elasticsearch.common.unit.DistanceUnit#KILOMETERS}
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public GeoDistanceFacetBuilder Unit(DistanceUnit unit)
        {
            _unit = unit;
            return this;
        }

        /// <summary>
        /// Should the facet run in global mode (not bounded by the search query) or not (bounded by
        /// the search query). Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="global"></param>
        /// <returns></returns>
        public new GeoDistanceFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new GeoDistanceFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new GeoDistanceFacetBuilder FacetFilter(IFilterBuilder filter)
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
        public new GeoDistanceFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(string.IsNullOrEmpty(_fieldName))
            {
                throw new SearchBuilderException("field must be set on geo_distance facet for facet [" + _name + "]");
            }

            if(_entries.Count == 0)
            {
                throw new SearchBuilderException("at least one range must be defined for geo_distance facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));
            
            if(_geohash != null)
            {
                content[NAME][_fieldName] = _geohash;
            }
            else
            {
                content[NAME][_fieldName] = new JArray(_lon, _lat);
            }

            if(_valueFieldName != null)
            {
                content[NAME]["value_field"] = _valueFieldName;
            }

            if(_valueScript != null)
            {
                content[NAME]["value_script"] = _valueScript;

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

            var ranges = new List<JObject>();

            foreach (var entry in _entries)
            {
                var range = new JObject();

                if(!double.IsInfinity(entry.From))
                {
                    range["from"] = entry.From;
                }

                if(!double.IsInfinity(entry.To))
                {
                    range["to"] = entry.To;
                }

                ranges.Add(range);
            }

            content[NAME]["ranges"] = new JArray(ranges);

            if(_unit != null)
            {
                content[NAME]["unit"] = _unit.Value.ToString().ToLower();
            }

            if(_geoDistance != null)
            {
                content[NAME]["distance_type"] = _geoDistance.Value.ToString().ToLower();
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