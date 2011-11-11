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
	public class RefreshResponse
	{
		public bool IsValid { get; internal set; }
		public ConnectionError ConnectionError { get; internal set; }
		public RefreshResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}