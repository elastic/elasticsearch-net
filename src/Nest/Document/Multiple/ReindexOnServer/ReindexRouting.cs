using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReindexRoutingJsonConverter))]
	public class ReindexRouting
	{
		private readonly string _newRoutingValue;
		public static ReindexRouting Keep = new ReindexRouting("keep", true);
		public static ReindexRouting Discard = new ReindexRouting("discard", true);

		/// <summary>
		/// Use ReindexRouting.Keep or ReindexRouting.Discard if you want to sent "keep" or "discard", this
		/// constructor always sends <param name="newRoutingValue" /> prefixed with '='
		/// </summary>
		/// <param name="newRoutingValue"></param>
		public ReindexRouting(string newRoutingValue) : this(newRoutingValue, false) { }

		private ReindexRouting(string newRoutingValue, bool noPrefix)
		{
			var routing = newRoutingValue.TrimStart('=');
			var prefix = noPrefix ? "" : "=";
			_newRoutingValue = $"{prefix}{routing}";
		}

		public static implicit operator ReindexRouting(string routing) => new ReindexRouting(routing);

		public override string ToString() => _newRoutingValue;
	}

	public class ReindexRoutingJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as ReindexRouting;
			if (v == null) writer.WriteNull();
			else writer.WriteValue(v.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var s = reader.ReadAsString();
			switch(s)
			{
				case "keep": return ReindexRouting.Keep;
				case "discard": return ReindexRouting.Discard;
				default: return new ReindexRouting(s);
			}
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
