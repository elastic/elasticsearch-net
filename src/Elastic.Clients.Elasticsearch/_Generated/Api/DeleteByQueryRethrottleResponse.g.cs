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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class DeleteByQueryRethrottleResponseConverter : System.Text.Json.Serialization.JsonConverter<DeleteByQueryRethrottleResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropNodeFailures = System.Text.Json.JsonEncodedText.Encode("node_failures");
	private static readonly System.Text.Json.JsonEncodedText PropNodes = System.Text.Json.JsonEncodedText.Encode("nodes");
	private static readonly System.Text.Json.JsonEncodedText PropTaskFailures = System.Text.Json.JsonEncodedText.Encode("task_failures");
	private static readonly System.Text.Json.JsonEncodedText PropTasks = System.Text.Json.JsonEncodedText.Encode("tasks");

	public override DeleteByQueryRethrottleResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>?> propNodeFailures = default;
		LocalJsonValue<IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>?> propNodes = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>?> propTaskFailures = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Tasks.TaskInfos?> propTasks = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propNodeFailures.TryRead(ref reader, options, PropNodeFailures))
			{
				continue;
			}

			if (propNodes.TryRead(ref reader, options, PropNodes))
			{
				continue;
			}

			if (propTaskFailures.TryRead(ref reader, options, PropTaskFailures))
			{
				continue;
			}

			if (propTasks.TryRead(ref reader, options, PropTasks))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new DeleteByQueryRethrottleResponse
		{
			NodeFailures = propNodeFailures.Value
,
			Nodes = propNodes.Value
,
			TaskFailures = propTaskFailures.Value
,
			Tasks = propTasks.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, DeleteByQueryRethrottleResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropNodeFailures, value.NodeFailures);
		writer.WriteProperty(options, PropNodes, value.Nodes);
		writer.WriteProperty(options, PropTaskFailures, value.TaskFailures);
		writer.WriteProperty(options, PropTasks, value.Tasks);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(DeleteByQueryRethrottleResponseConverter))]
public sealed partial class DeleteByQueryRethrottleResponse : ElasticsearchResponse
{
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? NodeFailures { get; init; }

	/// <summary>
	/// <para>
	/// Task information grouped by node, if <c>group_by</c> was set to <c>node</c> (the default).
	/// </para>
	/// </summary>
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>? Nodes { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>? TaskFailures { get; init; }

	/// <summary>
	/// <para>
	/// Either a flat list of tasks if <c>group_by</c> was set to <c>none</c>, or grouped by parents if
	/// <c>group_by</c> was set to <c>parents</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.TaskInfos? Tasks { get; init; }
}