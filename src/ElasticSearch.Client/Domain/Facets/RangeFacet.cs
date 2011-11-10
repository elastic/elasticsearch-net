using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class RangeFacet : Facet
    {
        [JsonProperty("ranges")]
        public IList<Range> Ranges { get; internal set; }

        #region Nested type: Range

        public class Range : FacetItem
        {
            [JsonProperty(PropertyName = "to")]
            public double? To { get; internal set; }

            [JsonProperty(PropertyName = "from")]
            public double? From { get; internal set; }

            [JsonProperty(PropertyName = "min")]
            public double Min { get; internal set; }

            [JsonProperty(PropertyName = "max")]
            public double Max { get; internal set; }

            [JsonProperty(PropertyName = "total_count")]
            public int TotalCount { get; internal set; }

            [JsonProperty(PropertyName = "total")]
            public double Total { get; internal set; }

            [JsonProperty(PropertyName = "mean")]
            public double Mean { get; internal set; }
        }

        #endregion
    }
}