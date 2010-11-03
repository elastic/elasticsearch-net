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
		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
		[JsonProperty(PropertyName = "hits")]
		public HitsMetaData<T> HitsMetaData { get; internal set; }

		public int Total
		{
			get
			{
				if (this.HitsMetaData == null)
					return 0;
				return this.HitsMetaData.Total;
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
	}
}


