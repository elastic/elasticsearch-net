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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

internal sealed partial class CheckpointingConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.Checkpointing>
{
	private static readonly System.Text.Json.JsonEncodedText PropChangesLastDetectedAt = System.Text.Json.JsonEncodedText.Encode("changes_last_detected_at");
	private static readonly System.Text.Json.JsonEncodedText PropChangesLastDetectedAtString = System.Text.Json.JsonEncodedText.Encode("changes_last_detected_at_string");
	private static readonly System.Text.Json.JsonEncodedText PropLast = System.Text.Json.JsonEncodedText.Encode("last");
	private static readonly System.Text.Json.JsonEncodedText PropLastSearchTime = System.Text.Json.JsonEncodedText.Encode("last_search_time");
	private static readonly System.Text.Json.JsonEncodedText PropLastSearchTimeString = System.Text.Json.JsonEncodedText.Encode("last_search_time_string");
	private static readonly System.Text.Json.JsonEncodedText PropNext = System.Text.Json.JsonEncodedText.Encode("next");
	private static readonly System.Text.Json.JsonEncodedText PropOperationsBehind = System.Text.Json.JsonEncodedText.Encode("operations_behind");

	public override Elastic.Clients.Elasticsearch.TransformManagement.Checkpointing Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propChangesLastDetectedAt = default;
		LocalJsonValue<System.DateTimeOffset?> propChangesLastDetectedAtString = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.CheckpointStats> propLast = default;
		LocalJsonValue<long?> propLastSearchTime = default;
		LocalJsonValue<System.DateTimeOffset?> propLastSearchTimeString = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.CheckpointStats?> propNext = default;
		LocalJsonValue<long?> propOperationsBehind = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propChangesLastDetectedAt.TryReadProperty(ref reader, options, PropChangesLastDetectedAt, null))
			{
				continue;
			}

			if (propChangesLastDetectedAtString.TryReadProperty(ref reader, options, PropChangesLastDetectedAtString, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propLast.TryReadProperty(ref reader, options, PropLast, null))
			{
				continue;
			}

			if (propLastSearchTime.TryReadProperty(ref reader, options, PropLastSearchTime, null))
			{
				continue;
			}

			if (propLastSearchTimeString.TryReadProperty(ref reader, options, PropLastSearchTimeString, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propNext.TryReadProperty(ref reader, options, PropNext, null))
			{
				continue;
			}

			if (propOperationsBehind.TryReadProperty(ref reader, options, PropOperationsBehind, null))
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.Checkpointing(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ChangesLastDetectedAt = propChangesLastDetectedAt.Value,
			ChangesLastDetectedAtString = propChangesLastDetectedAtString.Value,
			Last = propLast.Value,
			LastSearchTime = propLastSearchTime.Value,
			LastSearchTimeString = propLastSearchTimeString.Value,
			Next = propNext.Value,
			OperationsBehind = propOperationsBehind.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.Checkpointing value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropChangesLastDetectedAt, value.ChangesLastDetectedAt, null, null);
		writer.WriteProperty(options, PropChangesLastDetectedAtString, value.ChangesLastDetectedAtString, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropLast, value.Last, null, null);
		writer.WriteProperty(options, PropLastSearchTime, value.LastSearchTime, null, null);
		writer.WriteProperty(options, PropLastSearchTimeString, value.LastSearchTimeString, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropNext, value.Next, null, null);
		writer.WriteProperty(options, PropOperationsBehind, value.OperationsBehind, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.CheckpointingConverter))]
public sealed partial class Checkpointing
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Checkpointing(Elastic.Clients.Elasticsearch.TransformManagement.CheckpointStats last)
	{
		Last = last;
	}
#if NET7_0_OR_GREATER
	public Checkpointing()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Checkpointing()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Checkpointing(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public long? ChangesLastDetectedAt { get; set; }
	public System.DateTimeOffset? ChangesLastDetectedAtString { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.TransformManagement.CheckpointStats Last { get; set; }
	public long? LastSearchTime { get; set; }
	public System.DateTimeOffset? LastSearchTimeString { get; set; }
	public Elastic.Clients.Elasticsearch.TransformManagement.CheckpointStats? Next { get; set; }
	public long? OperationsBehind { get; set; }
}