using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(SuggestOptionJsonConverter))]
	public class SuggestOption
	{
		private readonly JObject _payload;
		private readonly JsonSerializer _defaultSerializer;

		public SuggestOption() { }
		internal SuggestOption(JObject payload, JsonSerializer serializer)
		{
			_payload = payload;
			_defaultSerializer = serializer;
		}

		[JsonProperty("freq")]
		public int? Frequency { get; internal set; }

		[JsonProperty("score")]
		public double Score { get; internal set; }

		[JsonProperty("text")]
		public string Text { get; internal set; }

		public T Payload<T>(JsonSerializer serializer = null) 
			where T : class
		{
			if (_payload == null) return null;
			var s = serializer ?? _defaultSerializer;
			return s != null
				? _payload.ToObject<T>(s)
				: _payload.ToObject<T>();
		}
	}

	public class SuggestOptionJsonConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType) => typeof(SuggestOption) == objectType;
		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var properties = o.Properties().ToDictionary(p => p.Name, p => p.Value);
			var option = properties.ContainsKey("payload")
				? new SuggestOption(properties["payload"].Value<JObject>(), serializer)
				: new SuggestOption();
			if (properties.ContainsKey("freq"))
				option.Frequency = (int)properties["freq"];
			if (properties.ContainsKey("score"))
				option.Score = (double)properties["score"];
			if (properties.ContainsKey("text"))
				option.Text = (string)properties["text"];
			return option;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
