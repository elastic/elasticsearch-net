using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class IndicesResponse : BaseResponse
	{
		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; private set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }
	}
}
