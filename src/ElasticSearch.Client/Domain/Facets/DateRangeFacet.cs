using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class DateRangeFacet : Facet
    {
        [JsonProperty("ranges")]
        public IList<DateRange> Ranges { get; internal set; }

        #region Nested type: DateRange

        public class DateRange : FacetItem
        {
            [JsonProperty(PropertyName = "to_str")]
            public DateTime? To { get; internal set; }

            [JsonProperty(PropertyName = "from_str")]
            public DateTime? From { get; internal set; }

            [JsonProperty(PropertyName = "min")]
            public float? Min { get; internal set; }

            [JsonProperty(PropertyName = "max")]
            public float? Max { get; internal set; }

            [JsonProperty(PropertyName = "total_count")]
            public int TotalCount { get; internal set; }

            [JsonProperty(PropertyName = "total")]
            public float Total { get; internal set; }

            [JsonProperty(PropertyName = "mean")]
            public float? Mean { get; internal set; }
        }

        #endregion
    }
}