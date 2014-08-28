using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	[JsonObject]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class GeoDistanceFacet : Facet, IFacet<GeoDistanceRange>
	{
		[JsonProperty("ranges")]
		public IEnumerable<GeoDistanceRange> Items { get; internal set; }
	}
	[JsonObject]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class GeoDistanceRange : FacetItem
	{
		[JsonProperty(PropertyName = "from")]
		public float From { get; internal set; }

		[JsonProperty(PropertyName = "to")]
		public float To { get; internal set; }

		[JsonProperty(PropertyName = "min")]
		public float? Min { get; internal set; }

		[JsonProperty(PropertyName = "max")]
		public float? Max { get; internal set; }
		
		[JsonProperty(PropertyName = "total")]
		public float Total { get; internal set; }

		[JsonProperty(PropertyName = "total_count")]
		public long TotalCount { get; internal set; }

		[JsonProperty(PropertyName = "mean")]
		public float? Mean { get; internal set; }
	}
}