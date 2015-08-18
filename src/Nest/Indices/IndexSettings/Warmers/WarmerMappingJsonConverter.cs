using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	public class WarmerMappingJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(WarmerMapping);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var mapping = (WarmerMapping) value;
			writer.WriteStartObject();

			writer.WritePropertyName("types");
			serializer.Serialize(writer, mapping.Types);

			writer.WritePropertyName("source");
			serializer.Serialize(writer, mapping.Source);

			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);
			var types = ((JArray)jObject.Property("types").Value).Values<string>().ToArray()
				.Select(s=>(TypeName)s);

			var source = serializer.Deserialize<SearchRequest>(jObject["source"].CreateReader());

			return new WarmerMapping
			{
				Types = types, 
				Source = source
			};
		}

	}
}