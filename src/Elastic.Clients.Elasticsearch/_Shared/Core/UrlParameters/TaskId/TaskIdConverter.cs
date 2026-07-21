// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class TaskIdConverter : JsonConverter<TaskId>
{
	public override void WriteAsPropertyName(Utf8JsonWriter writer, TaskId value, JsonSerializerOptions options)
	{
		if (options.TryGetClientSettings(out var settings))
		{
			writer.WritePropertyName(((IUrlParameter)value).GetString(settings));
			return;
		}

		throw new JsonException("Unable to retrive client settings during property name serialization.");
	}

	public override TaskId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetString();

	public override TaskId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Null)
			return null;

		if (reader.TokenType == JsonTokenType.String)
		{
			var taskId = reader.GetString();
			return new TaskId(taskId);
		}

		throw new JsonException("Unexpected JSON token");
	}

	public override void Write(Utf8JsonWriter writer, TaskId value, JsonSerializerOptions options)
	{
		if (value is null)
			writer.WriteNullValue();

		writer.WriteStringValue(value.FullyQualifiedId);
	}
}
