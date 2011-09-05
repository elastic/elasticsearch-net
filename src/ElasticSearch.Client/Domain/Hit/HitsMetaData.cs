using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
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
}
