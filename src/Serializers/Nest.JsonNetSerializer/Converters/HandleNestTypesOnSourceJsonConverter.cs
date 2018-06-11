using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.JsonNetSerializer.Converters
{
	public class HandleNestTypesOnSourceJsonConverter : JsonConverter
	{
		private readonly IElasticsearchSerializer _builtInSerializer;
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public HandleNestTypesOnSourceJsonConverter(IElasticsearchSerializer builtInSerializer)
		{
			_builtInSerializer = builtInSerializer;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var formatting = serializer.Formatting == Formatting.Indented
				? SerializationFormatting.Indented
				: SerializationFormatting.None;

			using (var ms = new MemoryStream())
			using (var streamReader = new StreamReader(ms, ConnectionSettingsAwareSerializerBase.ExpectedEncoding))
			using (var reader = new JsonTextReader(streamReader))
			{
				_builtInSerializer.Serialize(value, ms, formatting);
				ms.Position = 0;
				var token = reader.ReadTokenWithDateParseHandlingNone();
				writer.WriteToken(token.CreateReader(), true);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var token = reader.ReadTokenWithDateParseHandlingNone();
			//in place because JsonConverter.Deserialize() only works on full json objects.
			//even though we pass type JSON.NET won't try the registered converter for that type
			//even if it can handle string tokens :(
			if (objectType == typeof(JoinField) && token.Type == JTokenType.String)
				return JoinField.Root(token.ToString(Formatting.None));

			using (var ms = token.ToStream())
				return _builtInSerializer.Deserialize(objectType, ms);
		}

		private static readonly HashSet<Type> NestTypesThatCanAppearInSource = new HashSet<Type>
		{
			typeof(JoinField),
			typeof(QueryContainer),
			typeof(CompletionField),
			typeof(Attachment),
			typeof(ILazyDocument),
			typeof(GeoCoordinate)
		};

		public override bool CanConvert(Type objectType) =>
			NestTypesThatCanAppearInSource.Contains(objectType) ||
		    typeof(IGeoShape).IsAssignableFrom(objectType) ||
		    typeof(IGeometryCollection).IsAssignableFrom(objectType);
	}
}
