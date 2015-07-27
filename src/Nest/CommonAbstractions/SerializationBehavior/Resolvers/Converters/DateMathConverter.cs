using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	public class DateMathConverter : JsonConverter
	{
		public override bool CanWrite => true;

		public override bool CanRead => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as DateMath;
			if (v == null) return;
			writer.WriteValue(v.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
				return DateMath.FromString(reader.Value as string);
			if (reader.TokenType == JsonToken.Date)
			{
				var d = reader.Value as DateTime?;
				return d.HasValue ? DateMath.Anchored(d.Value) : null;
			}
			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}