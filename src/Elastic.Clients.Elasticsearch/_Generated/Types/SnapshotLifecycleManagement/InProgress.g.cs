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

namespace Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement;

internal sealed partial class InProgressConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.InProgress>
{
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropStartTimeMillis = System.Text.Json.JsonEncodedText.Encode("start_time_millis");
	private static readonly System.Text.Json.JsonEncodedText PropState = System.Text.Json.JsonEncodedText.Encode("state");
	private static readonly System.Text.Json.JsonEncodedText PropUuid = System.Text.Json.JsonEncodedText.Encode("uuid");

	public override Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.InProgress Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propName = default;
		LocalJsonValue<System.DateTime> propStartTimeMillis = default;
		LocalJsonValue<string> propState = default;
		LocalJsonValue<string> propUuid = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propStartTimeMillis.TryReadProperty(ref reader, options, PropStartTimeMillis, static System.DateTime (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propState.TryReadProperty(ref reader, options, PropState, null))
			{
				continue;
			}

			if (propUuid.TryReadProperty(ref reader, options, PropUuid, null))
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
		return new Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.InProgress(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Name = propName.Value,
			StartTimeMillis = propStartTimeMillis.Value,
			State = propState.Value,
			Uuid = propUuid.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.InProgress value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropStartTimeMillis, value.StartTimeMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime v) => w.WriteValueEx<System.DateTime>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropState, value.State, null, null);
		writer.WriteProperty(options, PropUuid, value.Uuid, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.InProgressConverter))]
public sealed partial class InProgress
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InProgress(string name, System.DateTime startTimeMillis, string state, string uuid)
	{
		Name = name;
		StartTimeMillis = startTimeMillis;
		State = state;
		Uuid = uuid;
	}
#if NET7_0_OR_GREATER
	public InProgress()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public InProgress()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal InProgress(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTime StartTimeMillis { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string State { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Uuid { get; set; }
}