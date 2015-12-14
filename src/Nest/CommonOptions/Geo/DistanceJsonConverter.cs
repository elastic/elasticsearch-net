using System;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	internal class DistanceJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var p = value as Distance;
			if (p == null)
			{
				writer.WriteNull();
				return;
			}

			using (var sw = new StringWriter())
			using (var localWriter = new JsonTextWriter(sw))
			{
				serializer.Serialize(localWriter, p.Precision);
				localWriter.WriteRaw(p.Unit.GetStringValue());
				var s = sw.ToString();
				writer.WriteValue(s);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String) return null;
			var v = reader.Value as string;
			if (v == null) return null;

			return new Distance(v);
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}
