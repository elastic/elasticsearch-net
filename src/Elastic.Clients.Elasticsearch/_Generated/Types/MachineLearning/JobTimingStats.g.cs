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

internal sealed partial class JobTimingStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.JobTimingStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropAverageBucketProcessingTimeMs = System.Text.Json.JsonEncodedText.Encode("average_bucket_processing_time_ms");
	private static readonly System.Text.Json.JsonEncodedText PropBucketCount = System.Text.Json.JsonEncodedText.Encode("bucket_count");
	private static readonly System.Text.Json.JsonEncodedText PropExponentialAverageBucketProcessingTimeMs = System.Text.Json.JsonEncodedText.Encode("exponential_average_bucket_processing_time_ms");
	private static readonly System.Text.Json.JsonEncodedText PropExponentialAverageBucketProcessingTimePerHourMs = System.Text.Json.JsonEncodedText.Encode("exponential_average_bucket_processing_time_per_hour_ms");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropMaximumBucketProcessingTimeMs = System.Text.Json.JsonEncodedText.Encode("maximum_bucket_processing_time_ms");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumBucketProcessingTimeMs = System.Text.Json.JsonEncodedText.Encode("minimum_bucket_processing_time_ms");
	private static readonly System.Text.Json.JsonEncodedText PropTotalBucketProcessingTimeMs = System.Text.Json.JsonEncodedText.Encode("total_bucket_processing_time_ms");

	public override Elastic.Clients.Elasticsearch.MachineLearning.JobTimingStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.TimeSpan?> propAverageBucketProcessingTimeMs = default;
		LocalJsonValue<long> propBucketCount = default;
		LocalJsonValue<System.TimeSpan?> propExponentialAverageBucketProcessingTimeMs = default;
		LocalJsonValue<System.TimeSpan> propExponentialAverageBucketProcessingTimePerHourMs = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<System.TimeSpan?> propMaximumBucketProcessingTimeMs = default;
		LocalJsonValue<System.TimeSpan?> propMinimumBucketProcessingTimeMs = default;
		LocalJsonValue<System.TimeSpan> propTotalBucketProcessingTimeMs = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAverageBucketProcessingTimeMs.TryReadProperty(ref reader, options, PropAverageBucketProcessingTimeMs, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propBucketCount.TryReadProperty(ref reader, options, PropBucketCount, null))
			{
				continue;
			}

			if (propExponentialAverageBucketProcessingTimeMs.TryReadProperty(ref reader, options, PropExponentialAverageBucketProcessingTimeMs, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propExponentialAverageBucketProcessingTimePerHourMs.TryReadProperty(ref reader, options, PropExponentialAverageBucketProcessingTimePerHourMs, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propMaximumBucketProcessingTimeMs.TryReadProperty(ref reader, options, PropMaximumBucketProcessingTimeMs, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propMinimumBucketProcessingTimeMs.TryReadProperty(ref reader, options, PropMinimumBucketProcessingTimeMs, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propTotalBucketProcessingTimeMs.TryReadProperty(ref reader, options, PropTotalBucketProcessingTimeMs, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.JobTimingStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AverageBucketProcessingTimeMs = propAverageBucketProcessingTimeMs.Value,
			BucketCount = propBucketCount.Value,
			ExponentialAverageBucketProcessingTimeMs = propExponentialAverageBucketProcessingTimeMs.Value,
			ExponentialAverageBucketProcessingTimePerHourMs = propExponentialAverageBucketProcessingTimePerHourMs.Value,
			JobId = propJobId.Value,
			MaximumBucketProcessingTimeMs = propMaximumBucketProcessingTimeMs.Value,
			MinimumBucketProcessingTimeMs = propMinimumBucketProcessingTimeMs.Value,
			TotalBucketProcessingTimeMs = propTotalBucketProcessingTimeMs.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.JobTimingStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAverageBucketProcessingTimeMs, value.AverageBucketProcessingTimeMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropBucketCount, value.BucketCount, null, null);
		writer.WriteProperty(options, PropExponentialAverageBucketProcessingTimeMs, value.ExponentialAverageBucketProcessingTimeMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropExponentialAverageBucketProcessingTimePerHourMs, value.ExponentialAverageBucketProcessingTimePerHourMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropMaximumBucketProcessingTimeMs, value.MaximumBucketProcessingTimeMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropMinimumBucketProcessingTimeMs, value.MinimumBucketProcessingTimeMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropTotalBucketProcessingTimeMs, value.TotalBucketProcessingTimeMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.JobTimingStatsConverter))]
public sealed partial class JobTimingStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JobTimingStats(long bucketCount, System.TimeSpan exponentialAverageBucketProcessingTimePerHourMs, string jobId, System.TimeSpan totalBucketProcessingTimeMs)
	{
		BucketCount = bucketCount;
		ExponentialAverageBucketProcessingTimePerHourMs = exponentialAverageBucketProcessingTimePerHourMs;
		JobId = jobId;
		TotalBucketProcessingTimeMs = totalBucketProcessingTimeMs;
	}
#if NET7_0_OR_GREATER
	public JobTimingStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public JobTimingStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal JobTimingStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.TimeSpan? AverageBucketProcessingTimeMs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long BucketCount { get; set; }
	public System.TimeSpan? ExponentialAverageBucketProcessingTimeMs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan ExponentialAverageBucketProcessingTimePerHourMs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JobId { get; set; }
	public System.TimeSpan? MaximumBucketProcessingTimeMs { get; set; }
	public System.TimeSpan? MinimumBucketProcessingTimeMs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TotalBucketProcessingTimeMs { get; set; }
}