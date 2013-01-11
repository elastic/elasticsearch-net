using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{

	public class DynamicTemplatesConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return true; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dict = value  as Dictionary<string, DynamicTemplate>;
			if (dict == null || !dict.HasAny())
				return;

			writer.WriteStartArray();
			foreach (var p in dict)
			{
				writer.WriteStartObject();
				{
					writer.WritePropertyName(p.Key);
					serializer.Serialize(writer, p.Value);
				}
				writer.WriteEndObject();
			}

			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
											JsonSerializer serializer)
		{
			var r = new Dictionary<string, DynamicTemplate>();

			JArray o = JArray.Load(reader);

			foreach (JObject p in o)
			{
				var firstProperty = p.Properties().First();
				var name = firstProperty.Name;
				var po = firstProperty.First as JObject;
				if (po == null)
					continue;

				var dict = serializer.Deserialize(po.CreateReader(), typeof(Dictionary<string, DynamicTemplate>)) as Dictionary<string, DynamicTemplate>;
				if (dict == null || dict.Count < 1)
					continue;

				var onlyMapping = dict.First();

				r.Add(onlyMapping.Key, onlyMapping.Value);

			}
			return r;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IDictionary<string, DynamicTemplate>);
		}

	}
}