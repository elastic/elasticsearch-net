using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class TermFacet : Facet, IFacet<TermItem>
    {
        [JsonProperty("missing")]
        public long Missing { get; internal set; }

        [JsonProperty("other")]
        public long Other { get; internal set; }

        [JsonProperty("total")]
        public long Total { get; internal set; }

        [JsonProperty("terms")]
        public IEnumerable<TermItem> Items { get; internal set; }
    }
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class TermItem : FacetItem
    {
        [JsonProperty(PropertyName = "term")]
        public string Term { get; internal set; }
    }
}