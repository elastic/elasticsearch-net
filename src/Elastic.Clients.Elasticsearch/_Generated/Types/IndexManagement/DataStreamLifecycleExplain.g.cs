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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class DataStreamLifecycleExplainConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleExplain>
{
	private static readonly System.Text.Json.JsonEncodedText PropError = System.Text.Json.JsonEncodedText.Encode("error");
	private static readonly System.Text.Json.JsonEncodedText PropGenerationTime = System.Text.Json.JsonEncodedText.Encode("generation_time");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropIndexCreationDateMillis = System.Text.Json.JsonEncodedText.Encode("index_creation_date_millis");
	private static readonly System.Text.Json.JsonEncodedText PropLifecycle = System.Text.Json.JsonEncodedText.Encode("lifecycle");
	private static readonly System.Text.Json.JsonEncodedText PropManagedByLifecycle = System.Text.Json.JsonEncodedText.Encode("managed_by_lifecycle");
	private static readonly System.Text.Json.JsonEncodedText PropRolloverDateMillis = System.Text.Json.JsonEncodedText.Encode("rollover_date_millis");
	private static readonly System.Text.Json.JsonEncodedText PropTimeSinceIndexCreation = System.Text.Json.JsonEncodedText.Encode("time_since_index_creation");
	private static readonly System.Text.Json.JsonEncodedText PropTimeSinceRollover = System.Text.Json.JsonEncodedText.Encode("time_since_rollover");

	public override Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleExplain Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propError = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propGenerationTime = default;
		LocalJsonValue<string> propIndex = default;
		LocalJsonValue<System.DateTimeOffset?> propIndexCreationDateMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover?> propLifecycle = default;
		LocalJsonValue<bool> propManagedByLifecycle = default;
		LocalJsonValue<System.DateTimeOffset?> propRolloverDateMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTimeSinceIndexCreation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTimeSinceRollover = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propError.TryReadProperty(ref reader, options, PropError, null))
			{
				continue;
			}

			if (propGenerationTime.TryReadProperty(ref reader, options, PropGenerationTime, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propIndexCreationDateMillis.TryReadProperty(ref reader, options, PropIndexCreationDateMillis, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propLifecycle.TryReadProperty(ref reader, options, PropLifecycle, null))
			{
				continue;
			}

			if (propManagedByLifecycle.TryReadProperty(ref reader, options, PropManagedByLifecycle, null))
			{
				continue;
			}

			if (propRolloverDateMillis.TryReadProperty(ref reader, options, PropRolloverDateMillis, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propTimeSinceIndexCreation.TryReadProperty(ref reader, options, PropTimeSinceIndexCreation, null))
			{
				continue;
			}

			if (propTimeSinceRollover.TryReadProperty(ref reader, options, PropTimeSinceRollover, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleExplain(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Error = propError.Value,
			GenerationTime = propGenerationTime.Value,
			Index = propIndex.Value,
			IndexCreationDateMillis = propIndexCreationDateMillis.Value,
			Lifecycle = propLifecycle.Value,
			ManagedByLifecycle = propManagedByLifecycle.Value,
			RolloverDateMillis = propRolloverDateMillis.Value,
			TimeSinceIndexCreation = propTimeSinceIndexCreation.Value,
			TimeSinceRollover = propTimeSinceRollover.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleExplain value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropError, value.Error, null, null);
		writer.WriteProperty(options, PropGenerationTime, value.GenerationTime, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropIndexCreationDateMillis, value.IndexCreationDateMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteValueEx<System.DateTimeOffset?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropLifecycle, value.Lifecycle, null, null);
		writer.WriteProperty(options, PropManagedByLifecycle, value.ManagedByLifecycle, null, null);
		writer.WriteProperty(options, PropRolloverDateMillis, value.RolloverDateMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteValueEx<System.DateTimeOffset?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropTimeSinceIndexCreation, value.TimeSinceIndexCreation, null, null);
		writer.WriteProperty(options, PropTimeSinceRollover, value.TimeSinceRollover, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleExplainConverter))]
public sealed partial class DataStreamLifecycleExplain
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamLifecycleExplain(string index, bool managedByLifecycle)
	{
		Index = index;
		ManagedByLifecycle = managedByLifecycle;
	}
#if NET7_0_OR_GREATER
	public DataStreamLifecycleExplain()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DataStreamLifecycleExplain()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataStreamLifecycleExplain(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string? Error { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? GenerationTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Index { get; set; }
	public System.DateTimeOffset? IndexCreationDateMillis { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover? Lifecycle { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool ManagedByLifecycle { get; set; }
	public System.DateTimeOffset? RolloverDateMillis { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? TimeSinceIndexCreation { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? TimeSinceRollover { get; set; }
}