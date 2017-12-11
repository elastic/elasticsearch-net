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

		[JsonProperty("start_offset")]
		public long StartOffset { get; internal set; }

		[JsonProperty("end_offset")]
		public long EndOffset { get; internal set; }

		[JsonProperty("position")]
		public long Position { get; internal set; }

		[JsonProperty("position_length")]
		public long? PositionLength { get; internal set; }
	}
}
