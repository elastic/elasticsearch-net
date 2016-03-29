using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class TimeJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as Time;
			if (v.Factor.HasValue && v.Interval.HasValue)
				writer.WriteValue(v.ToString());
			else writer.WriteValue((long)v.Milliseconds); 
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
				return new Time(reader.Value as string);

			if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
			{
				var milliseconds = Convert.ToInt64(reader.Value);
				return new Time(milliseconds);
			}

			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}