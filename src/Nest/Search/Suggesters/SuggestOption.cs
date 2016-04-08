using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

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

		[JsonProperty("highlighted")]
		public string Highlighted { get; internal set; }

		[JsonProperty("payload")]
		public FieldValues Payload { get; internal set; }

		[JsonProperty("contexts")]
		public IDictionary<string, IEnumerable<Context>> Contexts { get; internal set; }
	}
}
