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

internal sealed partial class ExponentialAverageCalculationContextConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.ExponentialAverageCalculationContext>
{
	private static readonly System.Text.Json.JsonEncodedText PropIncrementalMetricValueMs = System.Text.Json.JsonEncodedText.Encode("incremental_metric_value_ms");
	private static readonly System.Text.Json.JsonEncodedText PropLatestTimestamp = System.Text.Json.JsonEncodedText.Encode("latest_timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropPreviousExponentialAverageMs = System.Text.Json.JsonEncodedText.Encode("previous_exponential_average_ms");

	public override Elastic.Clients.Elasticsearch.MachineLearning.ExponentialAverageCalculationContext Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.TimeSpan> propIncrementalMetricValueMs = default;
		LocalJsonValue<System.DateTime?> propLatestTimestamp = default;
		LocalJsonValue<System.TimeSpan?> propPreviousExponentialAverageMs = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIncrementalMetricValueMs.TryReadProperty(ref reader, options, PropIncrementalMetricValueMs, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propLatestTimestamp.TryReadProperty(ref reader, options, PropLatestTimestamp, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propPreviousExponentialAverageMs.TryReadProperty(ref reader, options, PropPreviousExponentialAverageMs, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.ExponentialAverageCalculationContext(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IncrementalMetricValueMs = propIncrementalMetricValueMs.Value,
			LatestTimestamp = propLatestTimestamp.Value,
			PreviousExponentialAverageMs = propPreviousExponentialAverageMs.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.ExponentialAverageCalculationContext value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIncrementalMetricValueMs, value.IncrementalMetricValueMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropLatestTimestamp, value.LatestTimestamp, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropPreviousExponentialAverageMs, value.PreviousExponentialAverageMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.ExponentialAverageCalculationContextConverter))]
public sealed partial class ExponentialAverageCalculationContext
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExponentialAverageCalculationContext(System.TimeSpan incrementalMetricValueMs)
	{
		IncrementalMetricValueMs = incrementalMetricValueMs;
	}
#if NET7_0_OR_GREATER
	public ExponentialAverageCalculationContext()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ExponentialAverageCalculationContext()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ExponentialAverageCalculationContext(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan IncrementalMetricValueMs { get; set; }
	public System.DateTime? LatestTimestamp { get; set; }
	public System.TimeSpan? PreviousExponentialAverageMs { get; set; }
}