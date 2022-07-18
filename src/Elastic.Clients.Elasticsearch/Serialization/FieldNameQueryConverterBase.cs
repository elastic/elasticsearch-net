// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	/// A base for converters handling field name query variants.
	/// </summary>
	internal abstract class FieldNameQueryConverterBase<T> : JsonConverter<T> where T : FieldNameQueryBase
	{
		public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException($"Unexpected JSON response could not be serialized to {typeof(T)}.");

			reader.Read(); // query type
			reader.Read(); // start object
			reader.Read(); // field name
			var fieldName = reader.GetString();
			reader.Read();

			var query = ReadInternal(ref reader, typeToConvert, options);
			query.Field = fieldName;

			if (reader.TokenType != JsonTokenType.EndObject)
				throw new JsonException($"Unexpected JSON response could not be serialized to {typeof(T)}.");

			reader.Read();

			return query;
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			if (value.Field is null)
				writer.WriteNullValue();

			if (options.TryGetClientSettings(out var settings))
			{
				writer.WriteStartObject();
				writer.WritePropertyName(settings.Inferrer.Field(value.Field));
				WriteInternal(writer, value, options);
				writer.WriteEndObject();
				return;
			}

			throw new JsonException("Unable to retrieve client settings to infer field.");
		}

		internal abstract T? ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);
		internal abstract void WriteInternal(Utf8JsonWriter writer, T value, JsonSerializerOptions options);
	}


}
