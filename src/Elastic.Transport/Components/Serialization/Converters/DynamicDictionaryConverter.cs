// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Elastic.Transport
{
	public class DynamicDictionaryConverter : JsonConverter<DynamicDictionary>
	{
		public override DynamicDictionary Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.StartArray)
			{
				var array = JsonSerializer.Deserialize<object[]>(ref reader, options);
				var arrayDict = new Dictionary<string, object>();
				for (var i = 0; i < array.Length; i++)
					arrayDict[i.ToString(CultureInfo.InvariantCulture)] = new DynamicValue(array[i]);
				return DynamicDictionary.Create(arrayDict);
			}
			if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

			var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
			return DynamicDictionary.Create(dict);
		}

		public override void Write(Utf8JsonWriter writer, DynamicDictionary dictionary, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			foreach (var kvp in dictionary)
			{
				if (kvp.Value == null) continue;

				writer.WritePropertyName(kvp.Key);

				JsonSerializer.Serialize(writer, kvp.Value?.Value, options);
			}

			writer.WriteEndObject();
		}
	}
}
