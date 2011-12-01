using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Domain
{
	[JsonObject]
	public class StatsContainer
	{
		[JsonProperty(PropertyName = "docs")]
		public DocStats Documents { get; set; }
		[JsonProperty(PropertyName = "store")]
		public StoreStats Store { get; set; }
		[JsonProperty(PropertyName = "indexing")]
		public IndexingStats Indexing { get; set; }
		[JsonProperty(PropertyName = "get")]
		public GetStats Get { get; set; }
		[JsonProperty(PropertyName = "search")]
		public SearchStats Search { get; set; }
	}
}
