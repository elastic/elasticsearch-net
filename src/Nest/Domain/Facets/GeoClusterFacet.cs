using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest.Domain.Facets
{
    [JsonObject]
    public class GeoClusterFacet : Facet, IFacet<GeoClusterItem>
    {
         [JsonProperty("clusters")]
        public IEnumerable<GeoClusterItem> Items { get; private set; }
    }

    public class GeoClusterItem : FacetItem
    {
        [JsonProperty("top_left")]
        public GeoPoint TopLeft { get; set; }

        [JsonProperty("bottom_right")]
        public GeoPoint BottomRight { get; set; }

        [JsonProperty("center")]
        public GeoPoint Center { get; set; }

        [JsonProperty(PropertyName = "total")]
        public override int Count { get; internal set; }
    }

    public class GeoPoint
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }
    }
}
