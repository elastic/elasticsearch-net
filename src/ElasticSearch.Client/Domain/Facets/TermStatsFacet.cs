using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class TermStatsFacet : Facet
    {
        [JsonProperty("missing")]
        public int Missing { get; internal set; }

        [JsonProperty("terms")]
        public IList<TermStats> Terms { get; internal set; }

        #region Nested type: TermStats

        public class TermStats : TermFacet.TermItem
        {
            [JsonProperty(PropertyName = "min")]
            public double Min { get; internal set; }

            [JsonProperty(PropertyName = "max")]
            public double Max { get; internal set; }

            [JsonProperty(PropertyName = "total")]
            public double Total { get; internal set; }

            [JsonProperty(PropertyName = "mean")]
            public double Mean { get; internal set; }

            [JsonProperty("total_count")]
            public int TotalCount { get; set; }
        }

        #endregion
    }
}