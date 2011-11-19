using System;
using System.Collections.Generic;
using ElasticSearch.Client.Resolvers.Converters;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class DateHistogramFacet : Facet, IFacet<DateEntry>
    {
        [JsonProperty("entries")]
        public IEnumerable<DateEntry> Items { get; internal set; }

    }
    public class DateEntry : FacetItem
    {
        [JsonConverter(typeof (UnixDateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Time { get; internal set; }
    }
}