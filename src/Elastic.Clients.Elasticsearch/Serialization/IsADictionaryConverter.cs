// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class IsADictionaryConverter<T, TValue> : JsonConverter<T> where T : IsADictionaryBase<string, TValue>
	{
		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			foreach (var item in value)
			{
				writer.WritePropertyName(item.Key);
				JsonSerializer.Serialize(writer, item.Value, item.Value.GetType(), options);
			}

			writer.WriteEndObject();
		}
	}
}
