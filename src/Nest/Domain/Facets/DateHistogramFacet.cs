using System;
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    public class DateHistogramFacet : Facet, IFacet<DateEntry>
    {
        [JsonProperty("entries")]
        public IEnumerable<DateEntry> Items { get; internal set; }

    }
    public class DateEntry : FacetItem
    {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Time { get; internal set; }
        [JsonProperty("total")]
        public double? Total { get; internal set; }
        [JsonProperty("total_count")]
        public double? TotalCount { get; internal set; }
        [JsonProperty("min")]
        public double? Min { get; internal set; }
        [JsonProperty("max")]
        public double? Max { get; internal set; }
        [JsonProperty("mean")]
        public double? Mean { get; internal set; }
    }
}