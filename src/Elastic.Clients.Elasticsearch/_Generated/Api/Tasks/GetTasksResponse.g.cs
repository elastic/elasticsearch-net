// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Tasks;

internal sealed partial class GetTasksResponseConverter : System.Text.Json.Serialization.JsonConverter<GetTasksResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCompleted = System.Text.Json.JsonEncodedText.Encode("completed");
	private static readonly System.Text.Json.JsonEncodedText PropError = System.Text.Json.JsonEncodedText.Encode("error");
	private static readonly System.Text.Json.JsonEncodedText PropResponse = System.Text.Json.JsonEncodedText.Encode("response");
	private static readonly System.Text.Json.JsonEncodedText PropTask = System.Text.Json.JsonEncodedText.Encode("task");

	public override GetTasksResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propCompleted = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ErrorCause?> propError = default;
		LocalJsonValue<object?> propResponse = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Tasks.TaskInfo> propTask = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCompleted.TryRead(ref reader, options, PropCompleted))
			{
				continue;
			}

			if (propError.TryRead(ref reader, options, PropError))
			{
				continue;
			}

			if (propResponse.TryRead(ref reader, options, PropResponse))
			{
				continue;
			}

			if (propTask.TryRead(ref reader, options, PropTask))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new GetTasksResponse
		{
			Completed = propCompleted.Value
,
			Error = propError.Value
,
			Response = propResponse.Value
,
			Task = propTask.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetTasksResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCompleted, value.Completed);
		writer.WriteProperty(options, PropError, value.Error);
		writer.WriteProperty(options, PropResponse, value.Response);
		writer.WriteProperty(options, PropTask, value.Task);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetTasksResponseConverter))]
public sealed partial class GetTasksResponse : ElasticsearchResponse
{
	public bool Completed { get; init; }
	public Elastic.Clients.Elasticsearch.ErrorCause? Error { get; init; }
	public object? Response { get; init; }
	public Elastic.Clients.Elasticsearch.Tasks.TaskInfo Task { get; init; }
}