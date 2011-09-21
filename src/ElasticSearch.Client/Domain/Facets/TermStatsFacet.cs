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
            public float Min { get; internal set; }

            [JsonProperty(PropertyName = "max")]
            public float Max { get; internal set; }

            [JsonProperty(PropertyName = "total")]
            public float Total { get; internal set; }

            [JsonProperty(PropertyName = "mean")]
            public float Mean { get; internal set; }

            [JsonProperty("total_count")]
            public int TotalCount { get; set; }
        }

        #endregion
    }
}