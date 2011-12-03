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
	public class CountResponse : BaseResponse
	{
		public CountResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "count")]
		public int Count { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}