using Newtonsoft.Json;
using System.Collections.Generic;

namespace ElasticSearch.Client
{
    public interface IFacet
    {
    }
    public interface ISimpleFacet 
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