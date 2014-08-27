using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class HistogramFacet : Facet, IFacet<HistogramFacetItem>
    {
        [JsonProperty("entries")]
        public IEnumerable<HistogramFacetItem> Items { get; internal set; }
    }
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class HistogramFacetItem : FacetItem
    {
        [JsonProperty("key")]
        public double Key { get; set; }
    }

}