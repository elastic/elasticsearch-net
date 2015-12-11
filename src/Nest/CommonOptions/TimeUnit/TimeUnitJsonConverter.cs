using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class TimeUnitJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as TimeUnit;
			if (v.Factor.HasValue)
				writer.WriteValue(v.ToString());
			else writer.WriteValue(v.Milliseconds); 
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
				return new TimeUnit(reader.Value as string);

			if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
			{
				var milliseconds = Convert.ToInt64(reader.Value);
				return new TimeUnit(milliseconds);
			}

			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}