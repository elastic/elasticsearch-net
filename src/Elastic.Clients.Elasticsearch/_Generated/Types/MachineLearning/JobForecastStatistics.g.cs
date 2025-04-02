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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class JobForecastStatisticsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.JobForecastStatistics>
{
	private static readonly System.Text.Json.JsonEncodedText PropForecastedJobs = System.Text.Json.JsonEncodedText.Encode("forecasted_jobs");
	private static readonly System.Text.Json.JsonEncodedText PropMemoryBytes = System.Text.Json.JsonEncodedText.Encode("memory_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropProcessingTimeMs = System.Text.Json.JsonEncodedText.Encode("processing_time_ms");
	private static readonly System.Text.Json.JsonEncodedText PropRecords = System.Text.Json.JsonEncodedText.Encode("records");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");

	public override Elastic.Clients.Elasticsearch.MachineLearning.JobForecastStatistics Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propForecastedJobs = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics?> propMemoryBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics?> propProcessingTimeMs = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics?> propRecords = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, long>?> propStatus = default;
		LocalJsonValue<long> propTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propForecastedJobs.TryReadProperty(ref reader, options, PropForecastedJobs, null))
			{
				continue;
			}

			if (propMemoryBytes.TryReadProperty(ref reader, options, PropMemoryBytes, null))
			{
				continue;
			}

			if (propProcessingTimeMs.TryReadProperty(ref reader, options, PropProcessingTimeMs, null))
			{
				continue;
			}

			if (propRecords.TryReadProperty(ref reader, options, PropRecords, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, static System.Collections.Generic.IReadOnlyDictionary<string, long>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, long>(o, null, null)))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.JobForecastStatistics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ForecastedJobs = propForecastedJobs.Value,
			MemoryBytes = propMemoryBytes.Value,
			ProcessingTimeMs = propProcessingTimeMs.Value,
			Records = propRecords.Value,
			Status = propStatus.Value,
			Total = propTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.JobForecastStatistics value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropForecastedJobs, value.ForecastedJobs, null, null);
		writer.WriteProperty(options, PropMemoryBytes, value.MemoryBytes, null, null);
		writer.WriteProperty(options, PropProcessingTimeMs, value.ProcessingTimeMs, null, null);
		writer.WriteProperty(options, PropRecords, value.Records, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, long>? v) => w.WriteDictionaryValue<string, long>(o, v, null, null));
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.JobForecastStatisticsConverter))]
public sealed partial class JobForecastStatistics
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JobForecastStatistics(int forecastedJobs, long total)
	{
		ForecastedJobs = forecastedJobs;
		Total = total;
	}
#if NET7_0_OR_GREATER
	public JobForecastStatistics()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public JobForecastStatistics()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal JobForecastStatistics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int ForecastedJobs { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics? MemoryBytes { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics? ProcessingTimeMs { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics? Records { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, long>? Status { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Total { get; set; }
}