using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonObject]
	public class SuggestOption<T>
	{
		public SuggestOption(SuggestOption<object> original)
		{
			Frequency = original.Frequency;
			Score = original.Score;
			Text = original.Text;

			var payload = original.Payload as JObject;
			Payload = payload == null ? default(T) : payload.ToObject<T>();
		}

		[JsonProperty("freq")]
		public int? Frequency { get; internal set; }

		[JsonProperty("score")]
		public double Score { get; internal set; }

		[JsonProperty("text")]
		public string Text { get; internal set; }

		[JsonProperty("payload")]
		public T Payload { get; internal set; }
	}
}
