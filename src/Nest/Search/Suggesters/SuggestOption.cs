using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class SuggestOption
	{
		[JsonProperty("freq")]
		public int? Frequency { get; internal set; }

		[JsonProperty("score")]
		public double Score { get; internal set; }

		[JsonProperty("text")]
		public string Text { get; internal set; }

		[JsonProperty("payload")]
		public object Payload { get; internal set; }
	}
}
