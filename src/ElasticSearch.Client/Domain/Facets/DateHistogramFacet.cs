using System;
using System.Collections.Generic;
using ElasticSearch.Client.Resolvers.Converters;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class DateHistogramFacet : Facet
    {
        [JsonProperty("entries")]
        public IList<DateEntry> Entries { get; internal set; }

        #region Nested type: DateEntry

        public class DateEntry : FacetItem
        {
            [JsonConverter(typeof (UnixDateTimeConverter))]
            [JsonProperty("time")]
            public DateTime Time { get; internal set; }
        }

        #endregion
    }
}