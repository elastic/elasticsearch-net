using System.Collections.Generic;
using ElasticSearch.Client.DSL;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    [JsonObject]
    public class QueryResponse<T> where T : class
    {
        public QueryResponse()
        {
            this.IsValid = true;
        }

        internal PropertyNameResolver PropertyNameResolver { get; set; }

        public bool IsValid { get; set; }
        public ConnectionError ConnectionError { get; set; }

        [JsonProperty(PropertyName = "_shards")]
        public ShardsMetaData Shards { get; internal set; }

        [JsonProperty(PropertyName = "hits")]
        public HitsMetaData<T> Hits { get; internal set; }

        [JsonProperty(PropertyName = "facets")]
        public IDictionary<string, Facet> Facets { get; internal set; }

        [JsonProperty(PropertyName = "took")]
        public int ElapsedMilliseconds { get; internal set; }

        public int Total
        {
            get
            {
                if (this.Hits == null)
                {
                    return 0;
                }
                return this.Hits.Total;
            }
        }

        public float MaxScore
        {
            get
            {
                if (this.Hits == null)
                {
                    return 0;
                }
                return this.Hits.MaxScore;
            }
        }

        public IEnumerable<T> Documents
        {
            get
            {
                if (this.Hits != null)
                {
                    foreach (var hit in this.Hits.Hits)
                    {
                        yield return hit.Source;
                    }
                }
            }
        }

        public IEnumerable<Hit<T>> DocumentsWithMetaData
        {
            get
            {
                if (this.Hits != null)
                {
                    return this.Hits.Hits;
                }

                return new List<Hit<T>>();
            }
        }

        //public FacetMetaData FacetMetaData(Expression<Func<T, object>> expression)
        //{
        //    var fieldName = this.PropertyNameResolver.Resolve(expression);
        //    return this.FacetMetaData(fieldName);
        //}
        //public FacetMetaData FacetMetaData(string fieldName)
        //{
        //    var allMetaData = this.FacetMetaDataAll(fieldName);
        //    if (allMetaData != null && allMetaData.Any())
        //        return allMetaData.First();
        //    return null;
        //}
        //public IEnumerable<FacetMetaData> FacetMetaDataAll(Expression<Func<T, object>> expression)
        //{
        //    var fieldName = this.PropertyNameResolver.Resolve(expression);
        //    return this.FacetMetaDataAll(fieldName);
        //}

        //public IEnumerable<FacetMetaData> FacetMetaDataAll(string fieldName)
        //{
        //    if (this.FacetsMetaData == null
        //        || !this.FacetsMetaData.Any()
        //        || !this.FacetsMetaData.ContainsKey(fieldName))
        //        return null;

        //    var metaData = this.FacetsMetaData.FirstOrDefault(m => m.Key == fieldName).Value;
        //    return metaData;
        //}

        //public IEnumerable<F> Facets<F>(Expression<Func<T, object>> expression) where F : Facet
        //{
        //    var fieldName = this.PropertyNameResolver.Resolve(expression);
        //    return this.Facets<F>(fieldName);
        //}

        //public IEnumerable<F> Facets<F>(string fieldName) where F : Facet
        //{
        //    if (this.FacetsMetaData == null
        //        || !this.FacetsMetaData.Any()
        //        || !this.FacetsMetaData.ContainsKey(fieldName))
        //        return null;

        //    var typeName = new FacetTypeTranslator().GetFacetTypeNameFor<F>();
        //    if (typeName.IsNullOrEmpty())
        //        return null;

        //    var metaData = this.FacetsMetaData.FirstOrDefault(m => m.Key == fieldName).Value;
        //    var facetMetaData = metaData.FirstOrDefault(fm=>fm.Type == typeName);
        //    if (facetMetaData == null)
        //        return null;
        //    return facetMetaData.Facets.Cast<F>();
        //}
        //public F Facet<F>(Expression<Func<T, object>> expression) where F : SingleFacet
        //{
        //    var fieldName = this.PropertyNameResolver.Resolve(expression);
        //    return this.Facet<F>(fieldName);
        //}
        //public F Facet<F>(string fieldName) where F : SingleFacet
        //{
        //    if (this.FacetsMetaData == null
        //        || !this.FacetsMetaData.Any()
        //        || !this.FacetsMetaData.ContainsKey(fieldName))
        //        return null;

        //    var typeName = new FacetTypeTranslator().GetFacetTypeNameFor<F>();
        //    if (typeName.IsNullOrEmpty())
        //        return null;

        //    var metaData = this.FacetsMetaData.FirstOrDefault(m => m.Key == fieldName).Value;
        //    var facetMetaData = metaData.FirstOrDefault(fm => fm.Type == typeName);
        //    if (facetMetaData == null)
        //        return null;
        //    return facetMetaData.Facets.Cast<F>().FirstOrDefault();
        //}

        public IEnumerable<Highlight> Highlights
        {
            get
            {
                if (this.Hits != null)
                {
                    foreach (var hit in this.Hits.Hits)
                    {
                        foreach (var h in hit.Highlight)
                        {
                            yield return new Highlight {Field = h.Key};
                        }
                    }
                }
            }
        }
    }
}