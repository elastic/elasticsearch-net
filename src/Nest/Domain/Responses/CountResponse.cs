using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;


namespace Nest
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