using System;
using System.Globalization;
using Newtonsoft.Json;
using System.IO;

namespace Nest
{
	internal class DistanceUnitJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var p = value as DistanceUnit;
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

			return new DistanceUnit(v);
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}
