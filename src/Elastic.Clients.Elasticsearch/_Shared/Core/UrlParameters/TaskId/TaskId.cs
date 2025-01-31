// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(TaskIdConverter))]
[DebuggerDisplay("{DebugDisplay,nq}")]
public sealed class TaskId : IUrlParameter, IEquatable<TaskId>
{
	/// <summary>
	/// A task id exists in the form [node_id]:[task_id].
	/// </summary>
	/// <param name="taskId">The task identifier as a string.</param>
	public TaskId(string taskId)
	{
		if (string.IsNullOrWhiteSpace(taskId))
			throw new ArgumentException("TaskId can not be an empty string.", nameof(taskId));

		var tokens = taskId.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
		if (tokens.Length != 2)
			throw new ArgumentException($"TaskId:{taskId} not in expected format of <node_id>:<task_id>.", nameof(taskId));

		NodeId = tokens[0];
		FullyQualifiedId = taskId;

		if (!long.TryParse(tokens[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var t) || t < -1 || t == 0)
			throw new ArgumentException($"TaskId task component:{tokens[1]} could not be parsed to long or is out of range.", nameof(taskId));

		TaskNumber = t;
	}

	public string FullyQualifiedId { get; }
	public string NodeId { get; }
	public long TaskNumber { get; }

	private string DebugDisplay => FullyQualifiedId;

	public bool Equals(TaskId other) => EqualsString(other?.FullyQualifiedId);

	string IUrlParameter.GetString(ITransportConfiguration settings) => FullyQualifiedId;

	public override string ToString() => FullyQualifiedId;

	public static implicit operator TaskId(string taskId) => taskId.IsNullOrEmpty() ? null : new TaskId(taskId);

	public static bool operator ==(TaskId left, TaskId right) => Equals(left, right);

	public static bool operator !=(TaskId left, TaskId right) => !Equals(left, right);

	public override bool Equals(object obj) =>
		obj != null && obj is string s ? EqualsString(s) : obj is TaskId i && EqualsString(i.FullyQualifiedId);

	private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == FullyQualifiedId;

	public override int GetHashCode()
	{
		unchecked
		{
			return (NodeId.GetHashCode() * 397) ^ TaskNumber.GetHashCode();
		}
	}
}

internal sealed class TaskIdConverter : JsonConverter<TaskId>
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
