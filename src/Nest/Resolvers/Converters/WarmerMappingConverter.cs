using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	public class WarmerMappingConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);
			var types = ((JArray)jObject.Property("types").Value).Values<string>().ToArray();
			var source = jObject.Property("source").Value.ToString();

			return new WarmerMapping { Types = types, Source = source };
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(WarmerMapping);
		}
	}
}