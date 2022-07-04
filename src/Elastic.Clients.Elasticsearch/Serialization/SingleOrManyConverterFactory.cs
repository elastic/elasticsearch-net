// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class SingleOrManyConverterFactory : JsonConverterFactory
	{
		public override bool CanConvert(Type typeToConvert)
		{
			var customAttributes = typeToConvert.GetCustomAttributes();

			var canConvert = false;

			foreach (var item in customAttributes)
			{
				var type = item.GetType();
				if (type == typeof(SingleOrManyAttribute))
					canConvert = true;
			}

			return canConvert;
		}

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var att = typeToConvert.GetCustomAttribute<SingleOrManyAttribute>();
			return (JsonConverter)Activator.CreateInstance(typeof(SingleOrManyConverter<,>).MakeGenericType(typeToConvert, att.ItemType));
		}

		private class SingleOrManyConverter<T, TItem> : JsonConverter<T> where T : IList<TItem>, new()
		{
			public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				if (reader.TokenType == JsonTokenType.StartObject)
				{
					var singleItem = JsonSerializer.Deserialize<TItem>(ref reader, options);
					var list = new T();
					list.Add(singleItem);
					return list;
				}

				if (reader.TokenType == JsonTokenType.StartArray)
				{
					var list = new T();
					while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
					{
						var item = JsonSerializer.Deserialize<TItem>(ref reader, options);
						list.Add(item);
					}
					return list;
				}

				throw new JsonException("Unexpected token.");
			}

			public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
			{
				if (value.Count == 0)
				{
					writer.WriteStartObject();
					writer.WriteEndObject();
					return;
				}

				if (value.Count == 1)
				{
					JsonSerializer.Serialize<TItem>(writer, value[0], options);
					return;
				}

				writer.WriteStartArray();
				foreach (var item in value)
				{
					JsonSerializer.Serialize<TItem>(writer, item, options);
				}
				writer.WriteEndArray();
			}
		}
	}
}
