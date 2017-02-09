using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class FuzzinessJsonConverter : JsonConverter
	{
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IFuzziness;
			if (v.Auto) writer.WriteValue("AUTO"); 
			else if (v.EditDistance.HasValue) writer.WriteValue(v.EditDistance.Value); 
			else if (v.Ratio.HasValue) writer.WriteValue(v.Ratio.Value);
			else writer.WriteNull(); 
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
				return Fuzziness.Auto;
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
