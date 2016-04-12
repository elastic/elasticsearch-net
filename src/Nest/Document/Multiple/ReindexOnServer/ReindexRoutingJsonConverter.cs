using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class ReindexRoutingJsonConverter : JsonConverter
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