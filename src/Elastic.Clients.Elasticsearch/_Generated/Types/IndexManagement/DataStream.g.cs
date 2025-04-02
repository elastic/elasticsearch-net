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

internal sealed partial class DataStreamConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.DataStream>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowCustomRouting = System.Text.Json.JsonEncodedText.Encode("allow_custom_routing");
	private static readonly System.Text.Json.JsonEncodedText PropFailureStore = System.Text.Json.JsonEncodedText.Encode("failure_store");
	private static readonly System.Text.Json.JsonEncodedText PropGeneration = System.Text.Json.JsonEncodedText.Encode("generation");
	private static readonly System.Text.Json.JsonEncodedText PropHidden = System.Text.Json.JsonEncodedText.Encode("hidden");
	private static readonly System.Text.Json.JsonEncodedText PropIlmPolicy = System.Text.Json.JsonEncodedText.Encode("ilm_policy");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropLifecycle = System.Text.Json.JsonEncodedText.Encode("lifecycle");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropNextGenerationManagedBy = System.Text.Json.JsonEncodedText.Encode("next_generation_managed_by");
	private static readonly System.Text.Json.JsonEncodedText PropPreferIlm = System.Text.Json.JsonEncodedText.Encode("prefer_ilm");
	private static readonly System.Text.Json.JsonEncodedText PropReplicated = System.Text.Json.JsonEncodedText.Encode("replicated");
	private static readonly System.Text.Json.JsonEncodedText PropRolloverOnWrite = System.Text.Json.JsonEncodedText.Encode("rollover_on_write");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropSystem = System.Text.Json.JsonEncodedText.Encode("system");
	private static readonly System.Text.Json.JsonEncodedText PropTemplate = System.Text.Json.JsonEncodedText.Encode("template");
	private static readonly System.Text.Json.JsonEncodedText PropTimestampField = System.Text.Json.JsonEncodedText.Encode("timestamp_field");

	public override Elastic.Clients.Elasticsearch.IndexManagement.DataStream Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propAllowCustomRouting = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.FailureStore?> propFailureStore = default;
		LocalJsonValue<int> propGeneration = default;
		LocalJsonValue<bool> propHidden = default;
		LocalJsonValue<string?> propIlmPolicy = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamIndex>> propIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover?> propLifecycle = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.ManagedBy> propNextGenerationManagedBy = default;
		LocalJsonValue<bool> propPreferIlm = default;
		LocalJsonValue<bool?> propReplicated = default;
		LocalJsonValue<bool> propRolloverOnWrite = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.HealthStatus> propStatus = default;
		LocalJsonValue<bool?> propSystem = default;
		LocalJsonValue<string> propTemplate = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamTimestampField> propTimestampField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowCustomRouting.TryReadProperty(ref reader, options, PropAllowCustomRouting, null))
			{
				continue;
			}

			if (propFailureStore.TryReadProperty(ref reader, options, PropFailureStore, null))
			{
				continue;
			}

			if (propGeneration.TryReadProperty(ref reader, options, PropGeneration, null))
			{
				continue;
			}

			if (propHidden.TryReadProperty(ref reader, options, PropHidden, null))
			{
				continue;
			}

			if (propIlmPolicy.TryReadProperty(ref reader, options, PropIlmPolicy, null))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamIndex> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamIndex>(o, null)!))
			{
				continue;
			}

			if (propLifecycle.TryReadProperty(ref reader, options, PropLifecycle, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propNextGenerationManagedBy.TryReadProperty(ref reader, options, PropNextGenerationManagedBy, null))
			{
				continue;
			}

			if (propPreferIlm.TryReadProperty(ref reader, options, PropPreferIlm, null))
			{
				continue;
			}

			if (propReplicated.TryReadProperty(ref reader, options, PropReplicated, null))
			{
				continue;
			}

			if (propRolloverOnWrite.TryReadProperty(ref reader, options, PropRolloverOnWrite, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propSystem.TryReadProperty(ref reader, options, PropSystem, null))
			{
				continue;
			}

			if (propTemplate.TryReadProperty(ref reader, options, PropTemplate, null))
			{
				continue;
			}

			if (propTimestampField.TryReadProperty(ref reader, options, PropTimestampField, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.DataStream(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowCustomRouting = propAllowCustomRouting.Value,
			FailureStore = propFailureStore.Value,
			Generation = propGeneration.Value,
			Hidden = propHidden.Value,
			IlmPolicy = propIlmPolicy.Value,
			Indices = propIndices.Value,
			Lifecycle = propLifecycle.Value,
			Meta = propMeta.Value,
			Name = propName.Value,
			NextGenerationManagedBy = propNextGenerationManagedBy.Value,
			PreferIlm = propPreferIlm.Value,
			Replicated = propReplicated.Value,
			RolloverOnWrite = propRolloverOnWrite.Value,
			Status = propStatus.Value,
			System = propSystem.Value,
			Template = propTemplate.Value,
			TimestampField = propTimestampField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.DataStream value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowCustomRouting, value.AllowCustomRouting, null, null);
		writer.WriteProperty(options, PropFailureStore, value.FailureStore, null, null);
		writer.WriteProperty(options, PropGeneration, value.Generation, null, null);
		writer.WriteProperty(options, PropHidden, value.Hidden, null, null);
		writer.WriteProperty(options, PropIlmPolicy, value.IlmPolicy, null, null);
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamIndex> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamIndex>(o, v, null));
		writer.WriteProperty(options, PropLifecycle, value.Lifecycle, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropNextGenerationManagedBy, value.NextGenerationManagedBy, null, null);
		writer.WriteProperty(options, PropPreferIlm, value.PreferIlm, null, null);
		writer.WriteProperty(options, PropReplicated, value.Replicated, null, null);
		writer.WriteProperty(options, PropRolloverOnWrite, value.RolloverOnWrite, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropSystem, value.System, null, null);
		writer.WriteProperty(options, PropTemplate, value.Template, null, null);
		writer.WriteProperty(options, PropTimestampField, value.TimestampField, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamConverter))]
public sealed partial class DataStream
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStream(int generation, bool hidden, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamIndex> indices, string name, Elastic.Clients.Elasticsearch.IndexManagement.ManagedBy nextGenerationManagedBy, bool preferIlm, bool rolloverOnWrite, Elastic.Clients.Elasticsearch.HealthStatus status, string template, Elastic.Clients.Elasticsearch.IndexManagement.DataStreamTimestampField timestampField)
	{
		Generation = generation;
		Hidden = hidden;
		Indices = indices;
		Name = name;
		NextGenerationManagedBy = nextGenerationManagedBy;
		PreferIlm = preferIlm;
		RolloverOnWrite = rolloverOnWrite;
		Status = status;
		Template = template;
		TimestampField = timestampField;
	}
#if NET7_0_OR_GREATER
	public DataStream()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DataStream()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataStream(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the data stream allows custom routing on write request.
	/// </para>
	/// </summary>
	public bool? AllowCustomRouting { get; set; }

	/// <summary>
	/// <para>
	/// Information about failure store backing indices
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.FailureStore? FailureStore { get; set; }

	/// <summary>
	/// <para>
	/// Current generation for the data stream. This number acts as a cumulative count of the stream’s rollovers, starting at 1.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Generation { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the data stream is hidden.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Hidden { get; set; }

	/// <summary>
	/// <para>
	/// Name of the current ILM lifecycle policy in the stream’s matching index template.
	/// This lifecycle policy is set in the <c>index.lifecycle.name</c> setting.
	/// If the template does not include a lifecycle policy, this property is not included in the response.
	/// NOTE: A data stream’s backing indices may be assigned different lifecycle policies. To retrieve the lifecycle policy for individual backing indices, use the get index settings API.
	/// </para>
	/// </summary>
	public string? IlmPolicy { get; set; }

	/// <summary>
	/// <para>
	/// Array of objects containing information about the data stream’s backing indices.
	/// The last item in this array contains information about the stream’s current write index.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamIndex> Indices { get; set; }

	/// <summary>
	/// <para>
	/// Contains the configuration for the data stream lifecycle of this data stream.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover? Lifecycle { get; set; }

	/// <summary>
	/// <para>
	/// Custom metadata for the stream, copied from the <c>_meta</c> object of the stream’s matching index template.
	/// If empty, the response omits this property.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Meta { get; set; }

	/// <summary>
	/// <para>
	/// Name of the data stream.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }

	/// <summary>
	/// <para>
	/// Name of the lifecycle system that'll manage the next generation of the data stream.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.ManagedBy NextGenerationManagedBy { get; set; }

	/// <summary>
	/// <para>
	/// Indicates if ILM should take precedence over DSL in case both are configured to managed this data stream.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool PreferIlm { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the data stream is created and managed by cross-cluster replication and the local cluster can not write into this data stream or change its mappings.
	/// </para>
	/// </summary>
	public bool? Replicated { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the next write to this data stream will trigger a rollover first and the document will be indexed in the new backing index. If the rollover fails the indexing request will fail too.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool RolloverOnWrite { get; set; }

	/// <summary>
	/// <para>
	/// Health status of the data stream.
	/// This health status is based on the state of the primary and replica shards of the stream’s backing indices.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.HealthStatus Status { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the data stream is created and managed by an Elastic stack component and cannot be modified through normal user interaction.
	/// </para>
	/// </summary>
	public bool? System { get; set; }

	/// <summary>
	/// <para>
	/// Name of the index template used to create the data stream’s backing indices.
	/// The template’s index pattern must match the name of this data stream.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Template { get; set; }

	/// <summary>
	/// <para>
	/// Information about the <c>@timestamp</c> field in the data stream.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.DataStreamTimestampField TimestampField { get; set; }
}