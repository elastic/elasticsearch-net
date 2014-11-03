using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest.DSL.Filter
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IGeoHashCellFilter : IFilter
    {
        PropertyPathMarker Field { get; set; }
        string Location { get; set; }

        [JsonProperty("distance")]
        object Precision { get; set; }

        [JsonProperty("unit")]
        [JsonConverter(typeof(StringEnumConverter))]
        GeoUnit? Unit { get; set; }

        [JsonProperty("neighbors")]
        bool Neighbors { get; set; }
    }

    public class GeoHashCellFilter : PlainFilter, IGeoHashCellFilter
    {
        protected internal override void WrapInContainer(IFilterContainer container)
        {
            container.GeoHashCell = this;
        }

        public PropertyPathMarker Field { get; set; }
        public string Location { get; set; }
        public object Precision { get; set; }
        public GeoUnit? Unit { get; set; }
        public bool Neighbors { get; set; }
    }

    public class GeoHashCellFilterDescriptor : FilterBase, IGeoHashCellFilter
    {
        PropertyPathMarker IGeoHashCellFilter.Field { get; set; }
        string IGeoHashCellFilter.Location { get; set; }
        object IGeoHashCellFilter.Precision { get; set; }
        GeoUnit? IGeoHashCellFilter.Unit { get; set; }
        bool IGeoHashCellFilter.Neighbors { get; set; }

        bool IFilter.IsConditionless
        {
            get
            {
                var isConditionless = ((IGeoHashCellFilter) this).Location.IsNullOrEmpty();

                return isConditionless;
            }
        }

        public GeoHashCellFilterDescriptor Location(double lat, double lon)
        {
            var c = CultureInfo.InvariantCulture;
            ((IGeoHashCellFilter)this).Location = "{{ lat: {0}, lon: {1} }}".F(lat.ToString(c), lon.ToString(c));
            return this;
        }

        public GeoHashCellFilterDescriptor Location(string geoHash)
        {
            ((IGeoHashCellFilter)this).Location = geoHash;
            return this;
        }

        public GeoHashCellFilterDescriptor Precision(int precision)
        {
            ((IGeoHashCellFilter)this).Precision = precision;
            return this;
        }

        public GeoHashCellFilterDescriptor Distance(double distance, GeoUnit unit)
        {
            ((IGeoHashCellFilter)this).Precision = distance;
            ((IGeoHashCellFilter)this).Unit = unit;
            return this;
        }

        public GeoHashCellFilterDescriptor Neighbors(bool neighbors)
        {
            ((IGeoHashCellFilter)this).Neighbors = neighbors;
            return this;
        }

    }
}