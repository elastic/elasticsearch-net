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

internal sealed partial class TransformIndexerStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.TransformIndexerStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropDeleteTimeInMs = System.Text.Json.JsonEncodedText.Encode("delete_time_in_ms");
	private static readonly System.Text.Json.JsonEncodedText PropDocumentsDeleted = System.Text.Json.JsonEncodedText.Encode("documents_deleted");
	private static readonly System.Text.Json.JsonEncodedText PropDocumentsIndexed = System.Text.Json.JsonEncodedText.Encode("documents_indexed");
	private static readonly System.Text.Json.JsonEncodedText PropDocumentsProcessed = System.Text.Json.JsonEncodedText.Encode("documents_processed");
	private static readonly System.Text.Json.JsonEncodedText PropExponentialAvgCheckpointDurationMs = System.Text.Json.JsonEncodedText.Encode("exponential_avg_checkpoint_duration_ms");
	private static readonly System.Text.Json.JsonEncodedText PropExponentialAvgDocumentsIndexed = System.Text.Json.JsonEncodedText.Encode("exponential_avg_documents_indexed");
	private static readonly System.Text.Json.JsonEncodedText PropExponentialAvgDocumentsProcessed = System.Text.Json.JsonEncodedText.Encode("exponential_avg_documents_processed");
	private static readonly System.Text.Json.JsonEncodedText PropIndexFailures = System.Text.Json.JsonEncodedText.Encode("index_failures");
	private static readonly System.Text.Json.JsonEncodedText PropIndexTimeInMs = System.Text.Json.JsonEncodedText.Encode("index_time_in_ms");
	private static readonly System.Text.Json.JsonEncodedText PropIndexTotal = System.Text.Json.JsonEncodedText.Encode("index_total");
	private static readonly System.Text.Json.JsonEncodedText PropPagesProcessed = System.Text.Json.JsonEncodedText.Encode("pages_processed");
	private static readonly System.Text.Json.JsonEncodedText PropProcessingTimeInMs = System.Text.Json.JsonEncodedText.Encode("processing_time_in_ms");
	private static readonly System.Text.Json.JsonEncodedText PropProcessingTotal = System.Text.Json.JsonEncodedText.Encode("processing_total");
	private static readonly System.Text.Json.JsonEncodedText PropSearchFailures = System.Text.Json.JsonEncodedText.Encode("search_failures");
	private static readonly System.Text.Json.JsonEncodedText PropSearchTimeInMs = System.Text.Json.JsonEncodedText.Encode("search_time_in_ms");
	private static readonly System.Text.Json.JsonEncodedText PropSearchTotal = System.Text.Json.JsonEncodedText.Encode("search_total");
	private static readonly System.Text.Json.JsonEncodedText PropTriggerCount = System.Text.Json.JsonEncodedText.Encode("trigger_count");

	public override Elastic.Clients.Elasticsearch.TransformManagement.TransformIndexerStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.DateTime?> propDeleteTimeInMs = default;
		LocalJsonValue<long?> propDocumentsDeleted = default;
		LocalJsonValue<long> propDocumentsIndexed = default;
		LocalJsonValue<long> propDocumentsProcessed = default;
		LocalJsonValue<System.TimeSpan> propExponentialAvgCheckpointDurationMs = default;
		LocalJsonValue<double> propExponentialAvgDocumentsIndexed = default;
		LocalJsonValue<double> propExponentialAvgDocumentsProcessed = default;
		LocalJsonValue<long> propIndexFailures = default;
		LocalJsonValue<System.TimeSpan> propIndexTimeInMs = default;
		LocalJsonValue<long> propIndexTotal = default;
		LocalJsonValue<long> propPagesProcessed = default;
		LocalJsonValue<System.TimeSpan> propProcessingTimeInMs = default;
		LocalJsonValue<long> propProcessingTotal = default;
		LocalJsonValue<long> propSearchFailures = default;
		LocalJsonValue<System.TimeSpan> propSearchTimeInMs = default;
		LocalJsonValue<long> propSearchTotal = default;
		LocalJsonValue<long> propTriggerCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDeleteTimeInMs.TryReadProperty(ref reader, options, PropDeleteTimeInMs, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propDocumentsDeleted.TryReadProperty(ref reader, options, PropDocumentsDeleted, null))
			{
				continue;
			}

			if (propDocumentsIndexed.TryReadProperty(ref reader, options, PropDocumentsIndexed, null))
			{
				continue;
			}

			if (propDocumentsProcessed.TryReadProperty(ref reader, options, PropDocumentsProcessed, null))
			{
				continue;
			}

			if (propExponentialAvgCheckpointDurationMs.TryReadProperty(ref reader, options, PropExponentialAvgCheckpointDurationMs, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propExponentialAvgDocumentsIndexed.TryReadProperty(ref reader, options, PropExponentialAvgDocumentsIndexed, null))
			{
				continue;
			}

			if (propExponentialAvgDocumentsProcessed.TryReadProperty(ref reader, options, PropExponentialAvgDocumentsProcessed, null))
			{
				continue;
			}

			if (propIndexFailures.TryReadProperty(ref reader, options, PropIndexFailures, null))
			{
				continue;
			}

			if (propIndexTimeInMs.TryReadProperty(ref reader, options, PropIndexTimeInMs, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propIndexTotal.TryReadProperty(ref reader, options, PropIndexTotal, null))
			{
				continue;
			}

			if (propPagesProcessed.TryReadProperty(ref reader, options, PropPagesProcessed, null))
			{
				continue;
			}

			if (propProcessingTimeInMs.TryReadProperty(ref reader, options, PropProcessingTimeInMs, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propProcessingTotal.TryReadProperty(ref reader, options, PropProcessingTotal, null))
			{
				continue;
			}

			if (propSearchFailures.TryReadProperty(ref reader, options, PropSearchFailures, null))
			{
				continue;
			}

			if (propSearchTimeInMs.TryReadProperty(ref reader, options, PropSearchTimeInMs, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propSearchTotal.TryReadProperty(ref reader, options, PropSearchTotal, null))
			{
				continue;
			}

			if (propTriggerCount.TryReadProperty(ref reader, options, PropTriggerCount, null))
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.TransformIndexerStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DeleteTimeInMs = propDeleteTimeInMs.Value,
			DocumentsDeleted = propDocumentsDeleted.Value,
			DocumentsIndexed = propDocumentsIndexed.Value,
			DocumentsProcessed = propDocumentsProcessed.Value,
			ExponentialAvgCheckpointDurationMs = propExponentialAvgCheckpointDurationMs.Value,
			ExponentialAvgDocumentsIndexed = propExponentialAvgDocumentsIndexed.Value,
			ExponentialAvgDocumentsProcessed = propExponentialAvgDocumentsProcessed.Value,
			IndexFailures = propIndexFailures.Value,
			IndexTimeInMs = propIndexTimeInMs.Value,
			IndexTotal = propIndexTotal.Value,
			PagesProcessed = propPagesProcessed.Value,
			ProcessingTimeInMs = propProcessingTimeInMs.Value,
			ProcessingTotal = propProcessingTotal.Value,
			SearchFailures = propSearchFailures.Value,
			SearchTimeInMs = propSearchTimeInMs.Value,
			SearchTotal = propSearchTotal.Value,
			TriggerCount = propTriggerCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.TransformIndexerStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDeleteTimeInMs, value.DeleteTimeInMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropDocumentsDeleted, value.DocumentsDeleted, null, null);
		writer.WriteProperty(options, PropDocumentsIndexed, value.DocumentsIndexed, null, null);
		writer.WriteProperty(options, PropDocumentsProcessed, value.DocumentsProcessed, null, null);
		writer.WriteProperty(options, PropExponentialAvgCheckpointDurationMs, value.ExponentialAvgCheckpointDurationMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropExponentialAvgDocumentsIndexed, value.ExponentialAvgDocumentsIndexed, null, null);
		writer.WriteProperty(options, PropExponentialAvgDocumentsProcessed, value.ExponentialAvgDocumentsProcessed, null, null);
		writer.WriteProperty(options, PropIndexFailures, value.IndexFailures, null, null);
		writer.WriteProperty(options, PropIndexTimeInMs, value.IndexTimeInMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropIndexTotal, value.IndexTotal, null, null);
		writer.WriteProperty(options, PropPagesProcessed, value.PagesProcessed, null, null);
		writer.WriteProperty(options, PropProcessingTimeInMs, value.ProcessingTimeInMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropProcessingTotal, value.ProcessingTotal, null, null);
		writer.WriteProperty(options, PropSearchFailures, value.SearchFailures, null, null);
		writer.WriteProperty(options, PropSearchTimeInMs, value.SearchTimeInMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropSearchTotal, value.SearchTotal, null, null);
		writer.WriteProperty(options, PropTriggerCount, value.TriggerCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.TransformIndexerStatsConverter))]
public sealed partial class TransformIndexerStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TransformIndexerStats(long documentsIndexed, long documentsProcessed, System.TimeSpan exponentialAvgCheckpointDurationMs, double exponentialAvgDocumentsIndexed, double exponentialAvgDocumentsProcessed, long indexFailures, System.TimeSpan indexTimeInMs, long indexTotal, long pagesProcessed, System.TimeSpan processingTimeInMs, long processingTotal, long searchFailures, System.TimeSpan searchTimeInMs, long searchTotal, long triggerCount)
	{
		DocumentsIndexed = documentsIndexed;
		DocumentsProcessed = documentsProcessed;
		ExponentialAvgCheckpointDurationMs = exponentialAvgCheckpointDurationMs;
		ExponentialAvgDocumentsIndexed = exponentialAvgDocumentsIndexed;
		ExponentialAvgDocumentsProcessed = exponentialAvgDocumentsProcessed;
		IndexFailures = indexFailures;
		IndexTimeInMs = indexTimeInMs;
		IndexTotal = indexTotal;
		PagesProcessed = pagesProcessed;
		ProcessingTimeInMs = processingTimeInMs;
		ProcessingTotal = processingTotal;
		SearchFailures = searchFailures;
		SearchTimeInMs = searchTimeInMs;
		SearchTotal = searchTotal;
		TriggerCount = triggerCount;
	}
#if NET7_0_OR_GREATER
	public TransformIndexerStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TransformIndexerStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TransformIndexerStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.DateTime? DeleteTimeInMs { get; set; }
	public long? DocumentsDeleted { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long DocumentsIndexed { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long DocumentsProcessed { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan ExponentialAvgCheckpointDurationMs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double ExponentialAvgDocumentsIndexed { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double ExponentialAvgDocumentsProcessed { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long IndexFailures { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan IndexTimeInMs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long IndexTotal { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long PagesProcessed { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan ProcessingTimeInMs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ProcessingTotal { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long SearchFailures { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan SearchTimeInMs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long SearchTotal { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TriggerCount { get; set; }
}