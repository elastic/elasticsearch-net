using System;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests.Framework.ManagedElasticsearch.SourceSerializers
{

	internal class RevertBackToBuiltinSerializer : JsonConverter
	{
		private readonly IElasticsearchSerializer _builtInSerializer;
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public RevertBackToBuiltinSerializer(IElasticsearchSerializer builtInSerializer)
		{
			_builtInSerializer = builtInSerializer;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = _builtInSerializer.SerializeToString(value);
			var token = JToken.Parse(v);
			writer.WriteToken(token.CreateReader(), true);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			using (var ms = new MemoryStream(reader.ReadAsBytes()))
				return _builtInSerializer.Deserialize(objectType, ms);
		}

		private static Type[] TypesThatCanAppearInSource = {
			typeof(JoinField), typeof(QueryContainer)
		};

		public override bool CanConvert(Type objectType) => TypesThatCanAppearInSource.Contains(objectType);

	}




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
