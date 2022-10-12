// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Elastic.Clients.JsonNetSerializer.Converters
{
	public class HandleNestTypesOnSourceJsonConverter : JsonConverter
	{
		private static readonly HashSet<Type> NestTypesThatCanAppearInSource = new()
		{
			typeof(JoinField),
			typeof(QueryContainer),
			//typeof(CompletionField),
			//typeof(Attachment),
			typeof(LazyJson),
			//typeof(GeoCoordinate),
			typeof(GeoLocation),
			//typeof(CartesianPoint),
		};

		private readonly Serializer _builtInSerializer;
		private readonly IMemoryStreamFactory _memoryStreamFactory;

		public HandleNestTypesOnSourceJsonConverter(Serializer builtInSerializer, IMemoryStreamFactory memoryStreamFactory
		)
		{
			_builtInSerializer = builtInSerializer;
			_memoryStreamFactory = memoryStreamFactory;
		}

		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var formatting = serializer.Formatting == Formatting.Indented
				? SerializationFormatting.Indented
				: SerializationFormatting.None;

			using (var ms = _memoryStreamFactory.Create())
			using (var streamReader = new StreamReader(ms, ConnectionSettingsAwareSerializer.ExpectedEncoding))
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
				return JoinField.Root(token.Value<string>());

			using (var ms = token.ToStream(_memoryStreamFactory))
				return _builtInSerializer.Deserialize(objectType, ms);
		}

		public override bool CanConvert(Type objectType) =>
			NestTypesThatCanAppearInSource.Contains(objectType);
			//||
			//typeof(GeoShape).IsAssignableFrom(objectType) ||
			//typeof(GeometryCollection).IsAssignableFrom(objectType);
	}
}
