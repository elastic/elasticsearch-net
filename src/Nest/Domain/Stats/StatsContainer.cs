using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
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
		[JsonProperty(PropertyName = "merges")]
		public MergesStats Merges { get; set; }
		[JsonProperty(PropertyName = "refresh")]
		public RefreshStats Refresh { get; set; }
		[JsonProperty(PropertyName = "flush")]
		public FlushStats Flush { get; set; }


	}
}
