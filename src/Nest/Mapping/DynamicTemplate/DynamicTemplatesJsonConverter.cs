using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class DynamicTemplatesJsonConverter : JsonConverter
	{
		public override bool CanWrite => true;

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
			var dict = new Dictionary<string, DynamicTemplate>();

			JArray o = JArray.Load(reader);

			foreach (JObject p in o)
			{
				var prop = p.Properties().First();
				var po = prop.Value as JObject;
				var name = prop.Name;
				if (po ==null)
					continue;

				var template = serializer.Deserialize(po.CreateReader(), typeof(DynamicTemplate)) as DynamicTemplate;
				if (template == null)
					continue;

				dict.Add(name, template);
			}
			return dict;
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(IDictionary<string, DynamicTemplate>);
	}
}