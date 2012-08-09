using Newtonsoft.Json;
using System.Collections.Generic;
using Nest.Resolvers.Converters;

namespace Nest
{
    public interface IFacet
    {
    }

    [JsonObject]
    public interface IFacet<T> : IFacet where T : FacetItem
    {
        IEnumerable<T> Items { get; }
    }
    [JsonObject]
    public abstract class Facet
    {
    }
}