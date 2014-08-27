using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public interface IFacet
    {
    }

    [JsonObject]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public interface IFacet<T> : IFacet where T : FacetItem
    {
        IEnumerable<T> Items { get; }
    }
    [JsonObject]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public abstract class Facet : IFacet
    {
    }
}