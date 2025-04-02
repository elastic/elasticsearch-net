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

internal sealed partial class SlmIndicatorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.HealthReport.SlmIndicator>
{
	private static readonly System.Text.Json.JsonEncodedText PropDetails = System.Text.Json.JsonEncodedText.Encode("details");
	private static readonly System.Text.Json.JsonEncodedText PropDiagnosis = System.Text.Json.JsonEncodedText.Encode("diagnosis");
	private static readonly System.Text.Json.JsonEncodedText PropImpacts = System.Text.Json.JsonEncodedText.Encode("impacts");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropSymptom = System.Text.Json.JsonEncodedText.Encode("symptom");

	public override Elastic.Clients.Elasticsearch.Core.HealthReport.SlmIndicator Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.HealthReport.SlmIndicatorDetails?> propDetails = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.Diagnosis>?> propDiagnosis = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.Impact>?> propImpacts = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorHealthStatus> propStatus = default;
		LocalJsonValue<string> propSymptom = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDetails.TryReadProperty(ref reader, options, PropDetails, null))
			{
				continue;
			}

			if (propDiagnosis.TryReadProperty(ref reader, options, PropDiagnosis, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.Diagnosis>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.HealthReport.Diagnosis>(o, null)))
			{
				continue;
			}

			if (propImpacts.TryReadProperty(ref reader, options, PropImpacts, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.Impact>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.HealthReport.Impact>(o, null)))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propSymptom.TryReadProperty(ref reader, options, PropSymptom, null))
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
		return new Elastic.Clients.Elasticsearch.Core.HealthReport.SlmIndicator(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Details = propDetails.Value,
			Diagnosis = propDiagnosis.Value,
			Impacts = propImpacts.Value,
			Status = propStatus.Value,
			Symptom = propSymptom.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.HealthReport.SlmIndicator value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDetails, value.Details, null, null);
		writer.WriteProperty(options, PropDiagnosis, value.Diagnosis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.Diagnosis>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.HealthReport.Diagnosis>(o, v, null));
		writer.WriteProperty(options, PropImpacts, value.Impacts, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.Impact>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.HealthReport.Impact>(o, v, null));
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropSymptom, value.Symptom, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// SLM
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.HealthReport.SlmIndicatorConverter))]
public sealed partial class SlmIndicator
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SlmIndicator(Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorHealthStatus status, string symptom)
	{
		Status = status;
		Symptom = symptom;
	}
#if NET7_0_OR_GREATER
	public SlmIndicator()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SlmIndicator()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SlmIndicator(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Core.HealthReport.SlmIndicatorDetails? Details { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.Diagnosis>? Diagnosis { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.Impact>? Impacts { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorHealthStatus Status { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Symptom { get; set; }
}