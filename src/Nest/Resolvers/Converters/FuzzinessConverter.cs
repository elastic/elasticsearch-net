using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	public class FuzzinessConverter : JsonConverter
	{
		public override bool CanWrite { get { return true; } }

		public override bool CanRead { get { return true; } }

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
			if (reader.TokenType == JsonToken.None) return null;

			var jsonPath = reader.Path.Split('.').Last();
			return FuzzinessConverterHelper.ReadJson(new JObject(new JProperty(jsonPath, reader.Value)));
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}