using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class TermStatsFacet : Facet, IFacet<TermStats>
    {
        [JsonProperty("missing")]
        public long Missing { get; internal set; }

        [JsonProperty("terms")]
        public IEnumerable<TermStats> Items { get; internal set; }

    }
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class TermStats : TermItem
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
        public long TotalCount { get; set; }
    }

}