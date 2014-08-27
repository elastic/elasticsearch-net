using System;
using Newtonsoft.Json;

namespace Nest
{
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public abstract class FacetItem
	{
		[JsonProperty("count")]
		public virtual long Count { get; internal set; }
	}
}