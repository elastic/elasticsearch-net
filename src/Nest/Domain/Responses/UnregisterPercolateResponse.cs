using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;


namespace Nest
{
	[JsonObject]
	public class UnregisterPercolateResponse : BaseResponse
	{
		public UnregisterPercolateResponse()
		{
			this.IsValid = true;
		}
		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }
		
		[JsonProperty(PropertyName = "found")]
		public bool Found { get; internal set; }

		//todo: change mapping slightly see #ES-1518

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }

		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }

		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }

		[JsonProperty(PropertyName = "_version")]
		public int Version { get; internal set; }
	}
}