using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class TimeUnitExpressionJsonConverter : JsonConverter
	{
		public override bool CanWrite => true;

		public override bool CanRead => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as TimeUnitExpression;
			if (v.Factor.HasValue)
				writer.WriteValue(v.ToString());
			else writer.WriteValue(v.Milliseconds); 
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
				return new TimeUnitExpression(reader.Value as string);
			if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
			{
				var milliseconds = Convert.ToInt64(reader.Value);
				return new TimeUnitExpression(milliseconds);
			}
			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}