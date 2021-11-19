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
			
			reader.Read();
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

			writer.WriteStartObject();
			writer.WritePropertyName(value.Field.ToString());
			WriteInternal(writer, value, options);
			writer.WriteEndObject();
		}

		internal abstract T? ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);
		internal abstract void WriteInternal(Utf8JsonWriter writer, T value, JsonSerializerOptions options);
	}
}
