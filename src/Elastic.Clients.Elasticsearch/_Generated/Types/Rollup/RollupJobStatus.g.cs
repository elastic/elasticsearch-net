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

namespace Elastic.Clients.Elasticsearch.Rollup;

internal sealed partial class RollupJobStatusConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Rollup.RollupJobStatus>
{
	private static readonly System.Text.Json.JsonEncodedText PropCurrentPosition = System.Text.Json.JsonEncodedText.Encode("current_position");
	private static readonly System.Text.Json.JsonEncodedText PropJobState = System.Text.Json.JsonEncodedText.Encode("job_state");
	private static readonly System.Text.Json.JsonEncodedText PropUpgradedDocId = System.Text.Json.JsonEncodedText.Encode("upgraded_doc_id");

	public override Elastic.Clients.Elasticsearch.Rollup.RollupJobStatus Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propCurrentPosition = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Rollup.IndexingJobState> propJobState = default;
		LocalJsonValue<bool?> propUpgradedDocId = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCurrentPosition.TryReadProperty(ref reader, options, PropCurrentPosition, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propJobState.TryReadProperty(ref reader, options, PropJobState, null))
			{
				continue;
			}

			if (propUpgradedDocId.TryReadProperty(ref reader, options, PropUpgradedDocId, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
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
		return new Elastic.Clients.Elasticsearch.Rollup.RollupJobStatus(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CurrentPosition = propCurrentPosition.Value,
			JobState = propJobState.Value,
			UpgradedDocId = propUpgradedDocId.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Rollup.RollupJobStatus value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCurrentPosition, value.CurrentPosition, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropJobState, value.JobState, null, null);
		writer.WriteProperty(options, PropUpgradedDocId, value.UpgradedDocId, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Rollup.RollupJobStatusConverter))]
public sealed partial class RollupJobStatus
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RollupJobStatus(Elastic.Clients.Elasticsearch.Rollup.IndexingJobState jobState)
	{
		JobState = jobState;
	}
#if NET7_0_OR_GREATER
	public RollupJobStatus()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RollupJobStatus()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RollupJobStatus(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyDictionary<string, object>? CurrentPosition { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Rollup.IndexingJobState JobState { get; set; }
	public bool? UpgradedDocId { get; set; }
}