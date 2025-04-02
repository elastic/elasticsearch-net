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

namespace Elastic.Clients.Elasticsearch.Core.HealthReport;

internal sealed partial class ShardsAvailabilityIndicatorDetailsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.HealthReport.ShardsAvailabilityIndicatorDetails>
{
	private static readonly System.Text.Json.JsonEncodedText PropCreatingPrimaries = System.Text.Json.JsonEncodedText.Encode("creating_primaries");
	private static readonly System.Text.Json.JsonEncodedText PropCreatingReplicas = System.Text.Json.JsonEncodedText.Encode("creating_replicas");
	private static readonly System.Text.Json.JsonEncodedText PropInitializingPrimaries = System.Text.Json.JsonEncodedText.Encode("initializing_primaries");
	private static readonly System.Text.Json.JsonEncodedText PropInitializingReplicas = System.Text.Json.JsonEncodedText.Encode("initializing_replicas");
	private static readonly System.Text.Json.JsonEncodedText PropRestartingPrimaries = System.Text.Json.JsonEncodedText.Encode("restarting_primaries");
	private static readonly System.Text.Json.JsonEncodedText PropRestartingReplicas = System.Text.Json.JsonEncodedText.Encode("restarting_replicas");
	private static readonly System.Text.Json.JsonEncodedText PropStartedPrimaries = System.Text.Json.JsonEncodedText.Encode("started_primaries");
	private static readonly System.Text.Json.JsonEncodedText PropStartedReplicas = System.Text.Json.JsonEncodedText.Encode("started_replicas");
	private static readonly System.Text.Json.JsonEncodedText PropUnassignedPrimaries = System.Text.Json.JsonEncodedText.Encode("unassigned_primaries");
	private static readonly System.Text.Json.JsonEncodedText PropUnassignedReplicas = System.Text.Json.JsonEncodedText.Encode("unassigned_replicas");

	public override Elastic.Clients.Elasticsearch.Core.HealthReport.ShardsAvailabilityIndicatorDetails Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCreatingPrimaries = default;
		LocalJsonValue<long> propCreatingReplicas = default;
		LocalJsonValue<long> propInitializingPrimaries = default;
		LocalJsonValue<long> propInitializingReplicas = default;
		LocalJsonValue<long> propRestartingPrimaries = default;
		LocalJsonValue<long> propRestartingReplicas = default;
		LocalJsonValue<long> propStartedPrimaries = default;
		LocalJsonValue<long> propStartedReplicas = default;
		LocalJsonValue<long> propUnassignedPrimaries = default;
		LocalJsonValue<long> propUnassignedReplicas = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCreatingPrimaries.TryReadProperty(ref reader, options, PropCreatingPrimaries, null))
			{
				continue;
			}

			if (propCreatingReplicas.TryReadProperty(ref reader, options, PropCreatingReplicas, null))
			{
				continue;
			}

			if (propInitializingPrimaries.TryReadProperty(ref reader, options, PropInitializingPrimaries, null))
			{
				continue;
			}

			if (propInitializingReplicas.TryReadProperty(ref reader, options, PropInitializingReplicas, null))
			{
				continue;
			}

			if (propRestartingPrimaries.TryReadProperty(ref reader, options, PropRestartingPrimaries, null))
			{
				continue;
			}

			if (propRestartingReplicas.TryReadProperty(ref reader, options, PropRestartingReplicas, null))
			{
				continue;
			}

			if (propStartedPrimaries.TryReadProperty(ref reader, options, PropStartedPrimaries, null))
			{
				continue;
			}

			if (propStartedReplicas.TryReadProperty(ref reader, options, PropStartedReplicas, null))
			{
				continue;
			}

			if (propUnassignedPrimaries.TryReadProperty(ref reader, options, PropUnassignedPrimaries, null))
			{
				continue;
			}

			if (propUnassignedReplicas.TryReadProperty(ref reader, options, PropUnassignedReplicas, null))
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
		return new Elastic.Clients.Elasticsearch.Core.HealthReport.ShardsAvailabilityIndicatorDetails(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CreatingPrimaries = propCreatingPrimaries.Value,
			CreatingReplicas = propCreatingReplicas.Value,
			InitializingPrimaries = propInitializingPrimaries.Value,
			InitializingReplicas = propInitializingReplicas.Value,
			RestartingPrimaries = propRestartingPrimaries.Value,
			RestartingReplicas = propRestartingReplicas.Value,
			StartedPrimaries = propStartedPrimaries.Value,
			StartedReplicas = propStartedReplicas.Value,
			UnassignedPrimaries = propUnassignedPrimaries.Value,
			UnassignedReplicas = propUnassignedReplicas.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.HealthReport.ShardsAvailabilityIndicatorDetails value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCreatingPrimaries, value.CreatingPrimaries, null, null);
		writer.WriteProperty(options, PropCreatingReplicas, value.CreatingReplicas, null, null);
		writer.WriteProperty(options, PropInitializingPrimaries, value.InitializingPrimaries, null, null);
		writer.WriteProperty(options, PropInitializingReplicas, value.InitializingReplicas, null, null);
		writer.WriteProperty(options, PropRestartingPrimaries, value.RestartingPrimaries, null, null);
		writer.WriteProperty(options, PropRestartingReplicas, value.RestartingReplicas, null, null);
		writer.WriteProperty(options, PropStartedPrimaries, value.StartedPrimaries, null, null);
		writer.WriteProperty(options, PropStartedReplicas, value.StartedReplicas, null, null);
		writer.WriteProperty(options, PropUnassignedPrimaries, value.UnassignedPrimaries, null, null);
		writer.WriteProperty(options, PropUnassignedReplicas, value.UnassignedReplicas, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.HealthReport.ShardsAvailabilityIndicatorDetailsConverter))]
public sealed partial class ShardsAvailabilityIndicatorDetails
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardsAvailabilityIndicatorDetails(long creatingPrimaries, long creatingReplicas, long initializingPrimaries, long initializingReplicas, long restartingPrimaries, long restartingReplicas, long startedPrimaries, long startedReplicas, long unassignedPrimaries, long unassignedReplicas)
	{
		CreatingPrimaries = creatingPrimaries;
		CreatingReplicas = creatingReplicas;
		InitializingPrimaries = initializingPrimaries;
		InitializingReplicas = initializingReplicas;
		RestartingPrimaries = restartingPrimaries;
		RestartingReplicas = restartingReplicas;
		StartedPrimaries = startedPrimaries;
		StartedReplicas = startedReplicas;
		UnassignedPrimaries = unassignedPrimaries;
		UnassignedReplicas = unassignedReplicas;
	}
#if NET7_0_OR_GREATER
	public ShardsAvailabilityIndicatorDetails()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardsAvailabilityIndicatorDetails()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardsAvailabilityIndicatorDetails(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long CreatingPrimaries { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long CreatingReplicas { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long InitializingPrimaries { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long InitializingReplicas { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long RestartingPrimaries { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long RestartingReplicas { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long StartedPrimaries { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long StartedReplicas { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long UnassignedPrimaries { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long UnassignedReplicas { get; set; }
}