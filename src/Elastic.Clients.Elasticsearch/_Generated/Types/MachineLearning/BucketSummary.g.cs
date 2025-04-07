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

internal sealed partial class BucketSummaryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.BucketSummary>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnomalyScore = System.Text.Json.JsonEncodedText.Encode("anomaly_score");
	private static readonly System.Text.Json.JsonEncodedText PropBucketInfluencers = System.Text.Json.JsonEncodedText.Encode("bucket_influencers");
	private static readonly System.Text.Json.JsonEncodedText PropBucketSpan = System.Text.Json.JsonEncodedText.Encode("bucket_span");
	private static readonly System.Text.Json.JsonEncodedText PropEventCount = System.Text.Json.JsonEncodedText.Encode("event_count");
	private static readonly System.Text.Json.JsonEncodedText PropInitialAnomalyScore = System.Text.Json.JsonEncodedText.Encode("initial_anomaly_score");
	private static readonly System.Text.Json.JsonEncodedText PropIsInterim = System.Text.Json.JsonEncodedText.Encode("is_interim");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropProcessingTimeMs = System.Text.Json.JsonEncodedText.Encode("processing_time_ms");
	private static readonly System.Text.Json.JsonEncodedText PropResultType = System.Text.Json.JsonEncodedText.Encode("result_type");
	private static readonly System.Text.Json.JsonEncodedText PropTimestamp = System.Text.Json.JsonEncodedText.Encode("timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropTimestampString = System.Text.Json.JsonEncodedText.Encode("timestamp_string");

	public override Elastic.Clients.Elasticsearch.MachineLearning.BucketSummary Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double> propAnomalyScore = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer>> propBucketInfluencers = default;
		LocalJsonValue<System.TimeSpan> propBucketSpan = default;
		LocalJsonValue<long> propEventCount = default;
		LocalJsonValue<double> propInitialAnomalyScore = default;
		LocalJsonValue<bool> propIsInterim = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<System.TimeSpan> propProcessingTimeMs = default;
		LocalJsonValue<string> propResultType = default;
		LocalJsonValue<System.DateTime> propTimestamp = default;
		LocalJsonValue<System.DateTime?> propTimestampString = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnomalyScore.TryReadProperty(ref reader, options, PropAnomalyScore, null))
			{
				continue;
			}

			if (propBucketInfluencers.TryReadProperty(ref reader, options, PropBucketInfluencers, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer>(o, null)!))
			{
				continue;
			}

			if (propBucketSpan.TryReadProperty(ref reader, options, PropBucketSpan, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker))))
			{
				continue;
			}

			if (propEventCount.TryReadProperty(ref reader, options, PropEventCount, null))
			{
				continue;
			}

			if (propInitialAnomalyScore.TryReadProperty(ref reader, options, PropInitialAnomalyScore, null))
			{
				continue;
			}

			if (propIsInterim.TryReadProperty(ref reader, options, PropIsInterim, null))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propProcessingTimeMs.TryReadProperty(ref reader, options, PropProcessingTimeMs, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propResultType.TryReadProperty(ref reader, options, PropResultType, null))
			{
				continue;
			}

			if (propTimestamp.TryReadProperty(ref reader, options, PropTimestamp, static System.DateTime (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propTimestampString.TryReadProperty(ref reader, options, PropTimestampString, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.BucketSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AnomalyScore = propAnomalyScore.Value,
			BucketInfluencers = propBucketInfluencers.Value,
			BucketSpan = propBucketSpan.Value,
			EventCount = propEventCount.Value,
			InitialAnomalyScore = propInitialAnomalyScore.Value,
			IsInterim = propIsInterim.Value,
			JobId = propJobId.Value,
			ProcessingTimeMs = propProcessingTimeMs.Value,
			ResultType = propResultType.Value,
			Timestamp = propTimestamp.Value,
			TimestampString = propTimestampString.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.BucketSummary value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnomalyScore, value.AnomalyScore, null, null);
		writer.WriteProperty(options, PropBucketInfluencers, value.BucketInfluencers, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer>(o, v, null));
		writer.WriteProperty(options, PropBucketSpan, value.BucketSpan, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker)));
		writer.WriteProperty(options, PropEventCount, value.EventCount, null, null);
		writer.WriteProperty(options, PropInitialAnomalyScore, value.InitialAnomalyScore, null, null);
		writer.WriteProperty(options, PropIsInterim, value.IsInterim, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropProcessingTimeMs, value.ProcessingTimeMs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropResultType, value.ResultType, null, null);
		writer.WriteProperty(options, PropTimestamp, value.Timestamp, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime v) => w.WriteValueEx<System.DateTime>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropTimestampString, value.TimestampString, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.BucketSummaryConverter))]
public sealed partial class BucketSummary
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public BucketSummary(double anomalyScore, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer> bucketInfluencers, System.TimeSpan bucketSpan, long eventCount, double initialAnomalyScore, bool isInterim, string jobId, System.TimeSpan processingTimeMs, string resultType, System.DateTime timestamp)
	{
		AnomalyScore = anomalyScore;
		BucketInfluencers = bucketInfluencers;
		BucketSpan = bucketSpan;
		EventCount = eventCount;
		InitialAnomalyScore = initialAnomalyScore;
		IsInterim = isInterim;
		JobId = jobId;
		ProcessingTimeMs = processingTimeMs;
		ResultType = resultType;
		Timestamp = timestamp;
	}
#if NET7_0_OR_GREATER
	public BucketSummary()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public BucketSummary()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal BucketSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The maximum anomaly score, between 0-100, for any of the bucket influencers. This is an overall, rate-limited
	/// score for the job. All the anomaly records in the bucket contribute to this score. This value might be updated as
	/// new data is analyzed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double AnomalyScore { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer> BucketInfluencers { get; set; }

	/// <summary>
	/// <para>
	/// The length of the bucket in seconds. This value matches the bucket span that is specified in the job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan BucketSpan { get; set; }

	/// <summary>
	/// <para>
	/// The number of input data records processed in this bucket.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long EventCount { get; set; }

	/// <summary>
	/// <para>
	/// The maximum anomaly score for any of the bucket influencers. This is the initial value that was calculated at the
	/// time the bucket was processed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double InitialAnomalyScore { get; set; }

	/// <summary>
	/// <para>
	/// If true, this is an interim result. In other words, the results are calculated based on partial input data.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool IsInterim { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JobId { get; set; }

	/// <summary>
	/// <para>
	/// The amount of time, in milliseconds, that it took to analyze the bucket contents and calculate results.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan ProcessingTimeMs { get; set; }

	/// <summary>
	/// <para>
	/// Internal. This value is always set to bucket.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ResultType { get; set; }

	/// <summary>
	/// <para>
	/// The start time of the bucket. This timestamp uniquely identifies the bucket. Events that occur exactly at the
	/// timestamp of the bucket are included in the results for the bucket.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTime Timestamp { get; set; }

	/// <summary>
	/// <para>
	/// The start time of the bucket. This timestamp uniquely identifies the bucket. Events that occur exactly at the
	/// timestamp of the bucket are included in the results for the bucket.
	/// </para>
	/// </summary>
	public System.DateTime? TimestampString { get; set; }
}