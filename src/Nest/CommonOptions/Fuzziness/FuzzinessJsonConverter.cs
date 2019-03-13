using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class FuzzinessJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = (IFuzziness)value;
			if (v.Auto)
			{
				if (!v.Low.HasValue || !v.High.HasValue)
					writer.WriteValue("AUTO");
				else
					writer.WriteValue($"AUTO:{v.Low},{v.High}");
			}
			else if (v.EditDistance.HasValue) writer.WriteValue(v.EditDistance.Value);
			else if (v.Ratio.HasValue) writer.WriteValue(v.Ratio.Value);
			else writer.WriteNull();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				var rawAuto = (string)reader.Value;
				var colonIndex = rawAuto.IndexOf(':');
				var commaIndex = rawAuto.IndexOf(',');
				if (colonIndex == -1 || commaIndex == -1)
					return Fuzziness.Auto;

				var low = int.Parse(rawAuto.Substring(colonIndex + 1, commaIndex - colonIndex - 1));
				var high = int.Parse(rawAuto.Substring(commaIndex + 1));
				return Fuzziness.AutoLength(low, high);
			}

			if (reader.TokenType == JsonToken.Integer)
			{
				var editDistance = Convert.ToInt32(reader.Value);
				return Fuzziness.EditDistance(editDistance);
			}
			if (reader.TokenType == JsonToken.Float)
			{
				var ratio = (reader.Value as double?).GetValueOrDefault(0);
				return Fuzziness.Ratio(ratio);
			}
			return null;
		}
	}
}
