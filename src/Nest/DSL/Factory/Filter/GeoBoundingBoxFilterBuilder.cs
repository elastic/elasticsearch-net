using System;
using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Common;

namespace Nest.FactoryDsl.Filter
{
    public class GeoBoundingBoxFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.GeoBoundingBoxFilterBuilder;
        private readonly string _name;
        private Point _bottomRight;
        private string _bottomRightGeohash;
        private bool _cache;
        private string _cacheKey;
        private string _filterName;
        private Point _topLeft;
        private string _topLeftGeohash;
        private string _type;

        public GeoBoundingBoxFilterBuilder(string name)
        {
            _name = name;
        }

        public GeoBoundingBoxFilterBuilder TopLeft(double lat, double lon)
        {
            _topLeft = new Point {Lat = lat, Lon = lon};
            return this;
        }

        public GeoBoundingBoxFilterBuilder BottomRight(double lat, double lon)
        {
            _bottomRight = new Point {Lat = lat, Lon = lon};
            return this;
        }

        public GeoBoundingBoxFilterBuilder TopLeft(string geohash)
        {
            _topLeftGeohash = geohash;
            return this;
        }

        public GeoBoundingBoxFilterBuilder BottomRight(string geohash)
        {
            _bottomRightGeohash = geohash;
            return this;
        }

        public GeoBoundingBoxFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        public GeoBoundingBoxFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public GeoBoundingBoxFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        public GeoBoundingBoxFilterBuilder Type(string type)
        {
            _type = type;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject(new JProperty(_name, new JObject()))));

            if (_topLeftGeohash != null)
            {
                content[NAME][_name]["top_left"] = _topLeftGeohash;
            }
            else if (_topLeft != null)
            {
                content[NAME][_name]["top_left"] = new JArray(_topLeft.Lon, _topLeft.Lat);
            }
            else
            {
                throw new QueryBuilderException("geo_bounding_box requires 'top_left' to be set");
            }

            if (_bottomRightGeohash != null)
            {
                content[NAME][_name]["bottom_right"]= _bottomRightGeohash;
            }
            else if (_bottomRight != null)
            {
                content[NAME][_name]["bottom_right"] = new JArray(_bottomRight.Lon, _bottomRight.Lat);
            }
            else
            {
                throw new QueryBuilderException("geo_bounding_box requires 'bottom_right' to be set");
            }

            if (_filterName != null)
            {
                content[NAME][_name]["_name"] = _filterName;
            }

            if (_cache)
            {
                content[NAME][_name]["_cache"] = _cache;
            }

            if (_cacheKey != null)
            {
                content[NAME][_name]["_cache_key"] = _cacheKey;
            }

            if (_type != null)
            {
                content[NAME][_name]["type"] = _cacheKey;
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