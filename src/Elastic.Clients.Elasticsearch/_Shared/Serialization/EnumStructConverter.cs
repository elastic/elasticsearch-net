// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Core;
#else
using Elastic.Clients.Elasticsearch.Core;
#endif

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
namespace Elastic.Clients.Elasticsearch.Serialization;
#endif

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
