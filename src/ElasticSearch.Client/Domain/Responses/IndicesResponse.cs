using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class IndicesResponse
	{
		[JsonProperty(PropertyName = "ok")]
		public bool Success { get; private set; }
		public ConnectionStatus ConnectionStatus { get; internal set; }
		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }
	}
}
