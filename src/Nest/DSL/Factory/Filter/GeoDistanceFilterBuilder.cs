using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Filter
{
    public class GeoDistanceFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.GeoDistanceFilterBuilder;
        private readonly string _name;
        private bool _cache;
        private string _cacheKey;
        private string _distance;
        private string _filterName;
        private GeoDistance _geoDistance;
        private string _geohash;
        private double _lat;
        private double _lon;
        private string _optimizeBbox;

        public GeoDistanceFilterBuilder(string name)
        {
            _name = name;
            _geoDistance = Nest.GeoDistance.arc;
        }

        public GeoDistanceFilterBuilder Point(double lat, double lon)
        {
            _lat = lat;
            _lon = lon;
            return this;
        }

        public GeoDistanceFilterBuilder Lat(double lat)
        {
            _lat = lat;
            return this;
        }

        public GeoDistanceFilterBuilder Lon(double lon)
        {
            _lon = lon;
            return this;
        }

        public GeoDistanceFilterBuilder Distance(string distance)
        {
            _distance = distance;
            return this;
        }

        public GeoDistanceFilterBuilder Distance(double distance, DistanceUnit unit)
        {
            if (unit == DistanceUnit.KILOMETERS)
            {
                _distance = distance + "km";
            }
            else
            {
                _distance = distance + "mi";
            }

            return this;
        }

        public GeoDistanceFilterBuilder Geohash(string geohash)
        {
            _geohash = geohash;
            return this;
        }

        public GeoDistanceFilterBuilder GeoDistance(GeoDistance geoDistance)
        {
            _geoDistance = geoDistance;
            return this;
        }

        public GeoDistanceFilterBuilder OptimizeBbox(string optimizeBbox)
        {
            _optimizeBbox = optimizeBbox;
            return this;
        }

        public GeoDistanceFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        public GeoDistanceFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public GeoDistanceFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if (_geohash != null)
            {
                content[NAME][_name] = _geohash;
            }
            else
            {
                content[NAME][_name] = new JArray(_lon, _lat);
            }

            content[NAME]["distance"] = _distance;

            if (_geoDistance != Nest.GeoDistance.arc)
            {
                 content[NAME]["distance_type"] = _geoDistance.ToString().ToLower();
            }

            if (_optimizeBbox != null)
            {
                 content[NAME]["optimize_bbox"] = _optimizeBbox;
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