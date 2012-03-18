using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Common;

namespace Nest.FactoryDsl.Filter
{
    public class GeoPolygonFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.GeoPolygonFilterBuilder;
        private readonly string _name;
        private readonly List<Point> _points = new List<Point>();
        private bool _cache;
        private string _cacheKey;
        private string _filterName;

        public GeoPolygonFilterBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Adds a point with lat and lon
        /// </summary>
        /// <param name="lat">The latitude</param>
        /// <param name="lon">The longitude</param>
        /// <returns></returns>
        public GeoPolygonFilterBuilder AddPoint(double lat, double lon)
        {
            _points.Add(new Point(lat, lon));
            return this;
        }

        public GeoPolygonFilterBuilder AddPoint(string geohash)
        {
            double[] values = GeoHashUtils.Decode(geohash);
            return AddPoint(values[0], values[1]);
        }

        /// <summary>
        /// Sets the filter name for the filter that can be used when searching for matched_filters per hit.
        /// </summary>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public GeoPolygonFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        /// <summary>
        /// Should the filter be cached or not. Defaults to false.
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        public GeoPolygonFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public GeoPolygonFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME][_name] = new JObject();
            content[NAME][_name]["points"] = new JArray(_points.Select(t => new JArray(t.Lon, t.Lat)));

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