using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Sort
{
    /// <summary>
    /// A geo distance based sorting on a geo point like field.
    /// </summary>
    public class GeoDistanceSortBuilder : ISortBuilder
    {
        private const string NAME = NameRegistry.GeoDistanceSortBuilder;
        readonly string _fieldName;
        private double _lat;
        private double _lon;
        private string _geohash;
        private GeoDistance? _geoDistance;
        private DistanceUnit? _unit;
        private SortOrder _order;

        /// <summary>
        /// Constructs a new distance based sort on a geo point like field.
        /// </summary>
        /// <param name="fieldName">The geo point like field name.</param>
        public GeoDistanceSortBuilder(string fieldName)
        {
            _fieldName = fieldName;
        }

        /// <summary>
        /// The point to create the range distance facets from.
        /// </summary>
        /// <param name="lat">Latitude.</param>
        /// <param name="lon">Longitude</param>
        /// <returns></returns>
        public GeoDistanceSortBuilder Point(double lat, double lon)
        {
            _lat = lat;
            _lon = lon;
            return this;
        }

        /// <summary>
        /// The geohash of the geo point to create the range distance facets from.
        /// </summary>
        /// <param name="geohash"></param>
        /// <returns></returns>
        public GeoDistanceSortBuilder Geohash(string geohash)
        {
            _geohash = geohash;
            return this;
        }

        /// <summary>
        /// The geo distance type used to compute the distance.
        /// </summary>
        /// <param name="geoDistance"></param>
        /// <returns></returns>
        public GeoDistanceSortBuilder GeoDistance(GeoDistance geoDistance)
        {
            _geoDistance = geoDistance;
            return this;
        }

        /// <summary>
        /// The distance unit to use. Defaults to KILOMETERS.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public GeoDistanceSortBuilder Unit(DistanceUnit unit)
        {
            _unit = unit;
            return this;
        }

        /// <summary>
        /// The order of sorting. Defaults to ASC.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ISortBuilder Order(SortOrder order)
        {
            _order = order;
            return this;
        }

        /// <summary>
        /// Not relevant.
        /// </summary>
        /// <param name="missing"></param>
        /// <returns></returns>
        public ISortBuilder Missing(object missing)
        {
            return this;
        }

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if(_geohash != null)
            {
                content[NAME][_fieldName] = _geohash;
            }
            else
            {
                content[NAME][_fieldName] = new JArray(_lon, _lat);
            }

            if(_unit != null)
            {
                content[NAME]["unit"] = _unit.Value.ToString().ToLower();
            }

            if(_geoDistance != null)
            {
                content[NAME]["distance_type"] = _geoDistance.Value.ToString().ToLower();
            }

            if(_order == SortOrder.DESC)
            {
                content[NAME]["reverse"] = true;
            }
            
            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }
    }
}