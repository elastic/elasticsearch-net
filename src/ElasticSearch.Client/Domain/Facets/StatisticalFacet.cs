using Newtonsoft.Json;
using System.Collections.Generic;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class StatisticalFacet : Facet, ISimpleFacet
    {
        //Hiding is intentional here
        private new IEnumerable<FacetItem> Items { get; set; }
        


        [JsonProperty("count")]
        public int Count { get; internal set; }

        [JsonProperty(PropertyName = "min")]
        public double Min { get; internal set; }

        [JsonProperty(PropertyName = "max")]
        public double Max { get; internal set; }

        [JsonProperty(PropertyName = "total")]
        public double Total { get; internal set; }

        [JsonProperty(PropertyName = "sum_of_squares")]
        public double SumOfSquares { get; internal set; }

        [JsonProperty(PropertyName = "variance")]
        public double Variance { get; internal set; }

        [JsonProperty(PropertyName = "std_deviation")]
        public double StandardDeviation { get; internal set; }

        [JsonProperty(PropertyName = "mean")]
        public double Mean { get; internal set; }
    }
}