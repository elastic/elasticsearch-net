using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using ElasticSearch.Client.DSL;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class QueryResponse<T> where T : class
	{
		internal PropertyNameResolver PropertyNameResolver { get; set; }

		public bool IsValid { get; set; }
		public ConnectionError ConnectionError { get; set; }
		public QueryResponse()
		{
			this.IsValid = true;
		}
		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
		[JsonProperty(PropertyName = "hits")]
		public HitsMetaData<T> HitsMetaData { get; internal set; }

		[JsonProperty(PropertyName = "facets")]
		public Dictionary<string, List<FacetMetaData>> FacetsMetaData { get; internal set; }

		[JsonProperty(PropertyName = "took")]
		public int ElapsedMilliseconds { get; internal set; }


		public int Total
		{
			get
			{
				if (this.HitsMetaData == null)
					return 0;
				return this.HitsMetaData.Total;
			}
		}
		public float MaxScore
		{
			get
			{
				if (this.HitsMetaData == null)
					return 0;
				return this.HitsMetaData.MaxScore;
			}
		}

		public IEnumerable<T> Documents
		{
			get
			{
				if (this.HitsMetaData != null)
				{
					foreach (var hit in this.HitsMetaData.Hits)
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
				if (this.HitsMetaData != null)
				{
					foreach (var hit in this.HitsMetaData.Hits)
					{
						yield return hit;

					}
				}
			}
		}

		public IEnumerable<FacetMetaData> FacetMetaDataAll(string fieldName)
		{
			if (this.FacetsMetaData == null
				|| !this.FacetsMetaData.Any()
				|| !this.FacetsMetaData.ContainsKey(fieldName))
				return null;

			var metaData = this.FacetsMetaData.FirstOrDefault(m => m.Key == fieldName).Value;
			return metaData;
		}
		/* TODO expression based lookup! 
		public IEnumerable<F> Facets<F>(Expression<T, string> propertySelector) where F : Facet
		{
			
			return this.Facets<F>("");
		}*/

		public IEnumerable<F> Facets<F>(Expression<Func<T, object>> expression) where F : Facet
		{
			var fieldName = this.PropertyNameResolver.Resolve(expression);
			return this.Facets<F>(fieldName);
		}

		public IEnumerable<F> Facets<F>(string fieldName) where F : Facet
		{
			if (this.FacetsMetaData == null
				|| !this.FacetsMetaData.Any()
				|| !this.FacetsMetaData.ContainsKey(fieldName))
				return null;

			var typeName = new FacetTypeTranslator().GetFacetTypeNameFor<F>();
			if (typeName.IsNullOrEmpty())
				return null;

			var metaData = this.FacetsMetaData.FirstOrDefault(m => m.Key == fieldName).Value;
			var facetMetaData = metaData.FirstOrDefault(fm=>fm.Type == typeName);
			if (facetMetaData == null)
				return null;
			return facetMetaData.Facets.Cast<F>();
		}
		public F Facet<F>(Expression<Func<T, object>> expression) where F : SingleFacet
		{
			var fieldName = this.PropertyNameResolver.Resolve(expression);
			return this.Facet<F>(fieldName);
		}
		public F Facet<F>(string fieldName) where F : SingleFacet
		{
			if (this.FacetsMetaData == null
				|| !this.FacetsMetaData.Any()
				|| !this.FacetsMetaData.ContainsKey(fieldName))
				return null;

			var typeName = new FacetTypeTranslator().GetFacetTypeNameFor<F>();
			if (typeName.IsNullOrEmpty())
				return null;

			var metaData = this.FacetsMetaData.FirstOrDefault(m => m.Key == fieldName).Value;
			var facetMetaData = metaData.FirstOrDefault(fm => fm.Type == typeName);
			if (facetMetaData == null)
				return null;
			return facetMetaData.Facets.Cast<F>().FirstOrDefault();
		}


		public IEnumerable<Highlight> Highlights
		{
			get
			{
				if (this.HitsMetaData != null)
				{
					foreach (var hit in this.HitsMetaData.Hits)
					{
						foreach (var h in hit.Highlight) { 
							yield return new Highlight() { Field = h.Key };
						}
					}
				}
			}
		}


	}
}


