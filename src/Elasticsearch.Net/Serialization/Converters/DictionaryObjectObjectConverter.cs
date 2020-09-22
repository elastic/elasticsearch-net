// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Elasticsearch.Net
{
	public class DictionaryObjectKeyConverterFactory : JsonConverterFactory
	{
		public override bool CanConvert(Type typeToConvert)
		{
			if (!typeToConvert.IsGenericType) return false;

			if (typeToConvert.GetGenericTypeDefinition() != typeof(Dictionary<,>)) return false;

			var genericArgs = typeToConvert.GetGenericArguments();
			if (genericArgs.Length != 2) return false;

			return genericArgs[0] == typeof(object) && genericArgs[1] == typeof(object);
		}

		public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
		{
			var keyType = type.GetGenericArguments()[0];
			var valueType = type.GetGenericArguments()[1];

			return new DictionaryObjectKeyConverter(options);
		}

		private class DictionaryObjectKeyConverter : JsonConverter<Dictionary<object, object>>
		{
			private readonly JsonConverter<object> _valueConverter;

			public DictionaryObjectKeyConverter(JsonSerializerOptions options) =>
				// For performance, use the existing converter if available.
				_valueConverter = (JsonConverter<object>)options.GetConverter(typeof(object));

			public override Dictionary<object, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

				var dictionary = new Dictionary<object, object>();

				while (reader.Read())
				{
					if (reader.TokenType == JsonTokenType.EndObject) return dictionary;

					// Get the key.
					if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();

					var propertyName = reader.GetString();

					// Get the value.
					object v;
					if (_valueConverter != null)
					{
						reader.Read();
						v = _valueConverter.Read(ref reader, typeof(object), options);
					}
					else
						v = JsonSerializer.Deserialize<object>(ref reader, options);

					// Add to dictionary.
					dictionary.Add(propertyName, v);
				}
				return dictionary;
			}

			public override void Write(Utf8JsonWriter writer, Dictionary<object, object> dictionary, JsonSerializerOptions options)
			{
				writer.WriteStartObject();

				foreach (var kvp in dictionary)
				{
					writer.WritePropertyName(kvp.Key.ToString());

					JsonSerializer.Serialize(writer, kvp.Value, options);
				}

				writer.WriteEndObject();
			}
		}
	}
}
