using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class QueryResponse<T> where T : class
	{
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
	[JsonObject]
	public class IndicesResponse
	{
		[JsonProperty(PropertyName = "ok")]
		public bool Success { get; private set; }
		public ConnectionStatus ConnectionStatus { get; internal set; }
		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }
	}
	[JsonObject]
	public class ShardsMetaData
	{
		[JsonProperty]
		public int Total { get; internal set; }
		[JsonProperty]
		public int Successful { get; internal set; }
		[JsonProperty]
		public int Failed { get; internal set; }
	}
	[JsonObject]
	public class HitsMetaData<T> where T : class
	{
		[JsonProperty]
		public int Total { get; internal set; }
		[JsonProperty]
		public float MaxScore { get; internal set; }
		[JsonProperty]
		public List<Hit<T>> Hits { get; internal set; }
	}
	[JsonObject]
	public class Hit<T> where T : class
	{
		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }
		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }
		[JsonProperty(PropertyName = "_score")]
		public float Score { get; internal set; }
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }
		[JsonProperty(PropertyName = "highlight")]
		public Dictionary<string, List<string>> Highlight {get; internal set;}
	}
	[JsonObject]
	public class FacetMetaData
	{
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }
		[JsonProperty(PropertyName = "missing")]
		public int Missing { get; internal set; }

		public string Field { get; internal set; }

		public List<Facet>  Facets { get; internal set; }
	}
	[JsonObject]
	public class Facet
	{
		public string Key { get; internal set; }

		public bool Global { get; internal set; }
		[JsonProperty(PropertyName = "count")]
		public int Count { get; internal set; }
	}

	[JsonObject]
	public class TermFacet : Facet
	{
		[JsonProperty(PropertyName = "term")]
		public string Term { get; internal set; } 
	}
	[JsonObject]
	public class HistogramFacet : Facet
	{
		[JsonProperty(PropertyName = "key")]
		public new string Key { get; internal set; }
	}
	[JsonObject]
	public class DateHistogramFacet : Facet
	{
		[JsonProperty(PropertyName = "time")]
		public DateTime Time { get; internal set; }
	}
	[JsonObject]
	public class RangeFacet : Facet
	{
		[JsonProperty(PropertyName = "to")]
		public float? To { get; internal set; }
		[JsonProperty(PropertyName = "from")]
		public float? From { get; internal set; }

		[JsonProperty(PropertyName = "min")]
		public float? Min { get; internal set; }
		[JsonProperty(PropertyName = "max")]
		public float? Max { get; internal set; }
		[JsonProperty(PropertyName = "total_count")]
		public int TotalCount { get; internal set; }
		[JsonProperty(PropertyName = "total")]
		public float Total { get; internal set; }
		[JsonProperty(PropertyName = "mean")]
		public float? Mean { get; internal set; }
	}
	[JsonObject]
	public class DateRangeFacet : Facet
	{
		[JsonProperty(PropertyName = "to_str")]
		public DateTime? To { get; internal set; }
		[JsonProperty(PropertyName = "from_str")]
		public DateTime? From { get; internal set; }

		[JsonProperty(PropertyName = "min")]
		public float? Min { get; internal set; }
		[JsonProperty(PropertyName = "max")]
		public float? Max { get; internal set; }
		[JsonProperty(PropertyName = "total_count")]
		public int TotalCount { get; internal set; }
		[JsonProperty(PropertyName = "total")]
		public float Total { get; internal set; }
		[JsonProperty(PropertyName = "mean")]
		public float? Mean { get; internal set; }
	}

	public class Highlight
	{
		public string Field { get; internal set; }
	}
}


