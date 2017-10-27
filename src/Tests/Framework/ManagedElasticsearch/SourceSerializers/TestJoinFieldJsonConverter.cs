using System;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests.Framework.ManagedElasticsearch.SourceSerializers
{
	/// <summary>
	/// Hack to get our tests to serialize JoinFields correctly even when
	/// using custom source serializer
	/// </summary>
	internal class TestJoinFieldJsonConverter :JsonConverter
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
				p => serializer.Serialize(writer, GetName(p.Name)),
				c =>
				{
					writer.WriteStartObject();
					WriteProperty(writer, serializer, "name", GetName(c.Name));
					WriteProperty(writer, serializer, "parent", GetId(c.Parent));
					writer.WriteEndObject();
				}
			);
		}

		private string GetName(RelationName relation) => TestClient.DefaultInMemoryClient.Infer.RelationName(relation);
		private string GetId(IUrlParameter id) => id.GetString(TestClient.DefaultInMemoryClient.ConnectionSettings);

		public static void WriteProperty(JsonWriter writer, JsonSerializer serializer, string propertyName, object value)
		{
			if (value == null) return;
			writer.WritePropertyName(propertyName);
			serializer.Serialize(writer, value);
		}

		public override bool CanConvert(Type objectType) => typeof(JoinField) == objectType;
	}
}
