using System;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class JoinFieldJsonConverter :JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				var parent = reader.Value.ToString();
				return new JoinField(new JoinField.Parent(parent));
			}
			var jObject = JObject.Load(reader);
			if (jObject.Properties().Any(p=>p.Name == "parent"))
				using(var childReader =  jObject.CreateReader())
					return (JoinField)serializer.Deserialize<JoinField.Child>(childReader);

			using(var parentReader = jObject.CreateReader())
				return (JoinField)serializer.Deserialize<JoinField.Parent>(parentReader);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var join = value as JoinField;
			if (join == null)
			{
				writer.WriteNull();
				return;
			}
			join.Match(
				p => serializer.Serialize(writer, p.Name),
				c =>
				{
					writer.WriteStartObject();
					writer.WriteProperty(serializer, "name", c.Name);
					var id = (c.Parent as IUrlParameter)?.GetString(serializer.GetConnectionSettings());
					writer.WriteProperty(serializer, "parent", id);
					writer.WriteEndObject();
				}
			);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
