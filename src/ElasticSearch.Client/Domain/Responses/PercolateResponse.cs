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
	public class PercolateResponse : BaseResponse
	{
		public PercolateResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }
		[JsonProperty(PropertyName = "matches")]
		public IEnumerable<string> Matches { get; internal set; }
	}
}