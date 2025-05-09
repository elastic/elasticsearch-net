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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class TaskFailureConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TaskFailure>
{
	private static readonly System.Text.Json.JsonEncodedText PropNodeId = System.Text.Json.JsonEncodedText.Encode("node_id");
	private static readonly System.Text.Json.JsonEncodedText PropReason = System.Text.Json.JsonEncodedText.Encode("reason");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropTaskId = System.Text.Json.JsonEncodedText.Encode("task_id");

	public override Elastic.Clients.Elasticsearch.TaskFailure Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propNodeId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ErrorCause> propReason = default;
		LocalJsonValue<string> propStatus = default;
		LocalJsonValue<long> propTaskId = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propNodeId.TryReadProperty(ref reader, options, PropNodeId, null))
			{
				continue;
			}

			if (propReason.TryReadProperty(ref reader, options, PropReason, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propTaskId.TryReadProperty(ref reader, options, PropTaskId, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.TaskFailure(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			NodeId = propNodeId.Value,
			Reason = propReason.Value,
			Status = propStatus.Value,
			TaskId = propTaskId.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TaskFailure value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropNodeId, value.NodeId, null, null);
		writer.WriteProperty(options, PropReason, value.Reason, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropTaskId, value.TaskId, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TaskFailureConverter))]
public sealed partial class TaskFailure
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TaskFailure(string nodeId, Elastic.Clients.Elasticsearch.ErrorCause reason, string status, long taskId)
	{
		NodeId = nodeId;
		Reason = reason;
		Status = status;
		TaskId = taskId;
	}
#if NET7_0_OR_GREATER
	public TaskFailure()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TaskFailure()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TaskFailure(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string NodeId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.ErrorCause Reason { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Status { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TaskId { get; set; }
}