using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class AnalyzeToken
	{
		[JsonProperty("token")]
		public string Token { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		//TODO change to long in 6.0... RC: (this is int in Elasticsearch codebase)
		[JsonProperty("start_offset")]
		public int StartOffset { get; internal set; }

		[JsonProperty("end_offset")]
		public int EndOffset { get; internal set; }

		[JsonProperty("position")]
		public int Position { get; internal set; }

		[JsonProperty("position_length")]
		public long? PositionLength { get; internal set; }
	}
}
