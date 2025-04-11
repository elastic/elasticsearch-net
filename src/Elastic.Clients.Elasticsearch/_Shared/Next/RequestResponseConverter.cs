// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Serialization;

public sealed class RequestResponseConverter<T> :
	JsonConverter<T>
{
	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var settings = options.GetContext<IElasticsearchClientSettings>();

		return settings.RequestResponseSerializer.Deserialize<T>(ref reader);
	}

	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		var settings = options.GetContext<IElasticsearchClientSettings>();

		settings.RequestResponseSerializer.Serialize(value, writer);
	}
}
