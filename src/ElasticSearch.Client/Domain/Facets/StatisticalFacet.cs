using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class StatisticalFacet : Facet
    {
        [JsonProperty("count")]
        public int Count { get; internal set; }

        [JsonProperty(PropertyName = "min")]
        public float Min { get; internal set; }

        [JsonProperty(PropertyName = "max")]
        public float Max { get; internal set; }

        [JsonProperty(PropertyName = "total")]
        public float Total { get; internal set; }

        [JsonProperty(PropertyName = "sum_of_squares")]
        public float SumOfSquares { get; internal set; }

        [JsonProperty(PropertyName = "variance")]
        public float Variance { get; internal set; }

        [JsonProperty(PropertyName = "std_deviation")]
        public float StandardDeviation { get; internal set; }

        [JsonProperty(PropertyName = "mean")]
        public float Mean { get; internal set; }
    }
}