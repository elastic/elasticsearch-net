using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class TimeJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = (Time)value;
			if (v.Factor.HasValue && v.Interval.HasValue) writer.WriteValue(v.ToString());
			else writer.WriteValue((long)v.Milliseconds);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
					return new Time((string)reader.Value);
				case JsonToken.Integer:
				case JsonToken.Float:
					var milliseconds = Convert.ToInt64(reader.Value);
					return new Time(milliseconds);
			}

			return null;
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
