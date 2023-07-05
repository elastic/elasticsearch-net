// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Core;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class EnumStructConverter<T> : JsonConverter<T> where T : struct, IEnumStruct<T>
{
	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetString();
		var instance = default(T).Create(value);

		return instance;
	}

	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		var enumValue = value.ToString();

		if (!string.IsNullOrEmpty(enumValue))
			writer.WriteStringValue(enumValue);
		else
			writer.WriteNullValue();
	}
}
