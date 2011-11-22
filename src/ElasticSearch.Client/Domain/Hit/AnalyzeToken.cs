using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class AnalyzeToken
	{
		[JsonProperty(PropertyName = "token")]
		public string Token { get; internal set; }
		[JsonProperty(PropertyName = "type")]
		public string Type { get; internal set; }
		
		[JsonProperty(PropertyName = "start_offset")]
		public int StartOffset { get; internal set; }
		[JsonProperty(PropertyName = "end_offset")]
		public int EndPostion { get; internal set; }

		[JsonProperty(PropertyName = "position")]
		public int Position { get; internal set; }

	}
}
