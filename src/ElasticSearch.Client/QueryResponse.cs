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
		public QueryResponse ()
		{
			this.IsValid = true;
		}
		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
		[JsonProperty(PropertyName = "hits")]
		public HitsMetaData<T> HitsMetaData { get; internal set; }

		[JsonProperty(PropertyName = "facets")]
		public FacetsMetaData Facets { get; internal set; }

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
        public Highlight Highlight{ get; internal set; }
	}
    [JsonObject]
    public class Highlight
    {
        [JsonProperty(PropertyName = "content")]
        public List<string> Content { get; internal set; }
    }
	[JsonObject]
	public class FacetsMetaData
	{
		public Dictionary<string, List<Facet>> Facets { get; internal set; }
	}
	
	public class Facet
	{
		public bool Global { get; internal set; }
		public int Count { get; internal set; }
		public string Key { get; internal set; }
	}
}


