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

internal sealed partial class BucketInfluencerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnomalyScore = System.Text.Json.JsonEncodedText.Encode("anomaly_score");
	private static readonly System.Text.Json.JsonEncodedText PropBucketSpan = System.Text.Json.JsonEncodedText.Encode("bucket_span");
	private static readonly System.Text.Json.JsonEncodedText PropInfluencerFieldName = System.Text.Json.JsonEncodedText.Encode("influencer_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropInitialAnomalyScore = System.Text.Json.JsonEncodedText.Encode("initial_anomaly_score");
	private static readonly System.Text.Json.JsonEncodedText PropIsInterim = System.Text.Json.JsonEncodedText.Encode("is_interim");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropProbability = System.Text.Json.JsonEncodedText.Encode("probability");
	private static readonly System.Text.Json.JsonEncodedText PropRawAnomalyScore = System.Text.Json.JsonEncodedText.Encode("raw_anomaly_score");
	private static readonly System.Text.Json.JsonEncodedText PropResultType = System.Text.Json.JsonEncodedText.Encode("result_type");
	private static readonly System.Text.Json.JsonEncodedText PropTimestamp = System.Text.Json.JsonEncodedText.Encode("timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropTimestampString = System.Text.Json.JsonEncodedText.Encode("timestamp_string");

	public override Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double> propAnomalyScore = default;
		LocalJsonValue<System.TimeSpan> propBucketSpan = default;
		LocalJsonValue<string> propInfluencerFieldName = default;
		LocalJsonValue<double> propInitialAnomalyScore = default;
		LocalJsonValue<bool> propIsInterim = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<double> propProbability = default;
		LocalJsonValue<double> propRawAnomalyScore = default;
		LocalJsonValue<string> propResultType = default;
		LocalJsonValue<System.DateTimeOffset> propTimestamp = default;
		LocalJsonValue<System.DateTimeOffset?> propTimestampString = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnomalyScore.TryReadProperty(ref reader, options, PropAnomalyScore, null))
			{
				continue;
			}

			if (propBucketSpan.TryReadProperty(ref reader, options, PropBucketSpan, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker))))
			{
				continue;
			}

			if (propInfluencerFieldName.TryReadProperty(ref reader, options, PropInfluencerFieldName, null))
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

			if (propProbability.TryReadProperty(ref reader, options, PropProbability, null))
			{
				continue;
			}

			if (propRawAnomalyScore.TryReadProperty(ref reader, options, PropRawAnomalyScore, null))
			{
				continue;
			}

			if (propResultType.TryReadProperty(ref reader, options, PropResultType, null))
			{
				continue;
			}

			if (propTimestamp.TryReadProperty(ref reader, options, PropTimestamp, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propTimestampString.TryReadProperty(ref reader, options, PropTimestampString, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AnomalyScore = propAnomalyScore.Value,
			BucketSpan = propBucketSpan.Value,
			InfluencerFieldName = propInfluencerFieldName.Value,
			InitialAnomalyScore = propInitialAnomalyScore.Value,
			IsInterim = propIsInterim.Value,
			JobId = propJobId.Value,
			Probability = propProbability.Value,
			RawAnomalyScore = propRawAnomalyScore.Value,
			ResultType = propResultType.Value,
			Timestamp = propTimestamp.Value,
			TimestampString = propTimestampString.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnomalyScore, value.AnomalyScore, null, null);
		writer.WriteProperty(options, PropBucketSpan, value.BucketSpan, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker)));
		writer.WriteProperty(options, PropInfluencerFieldName, value.InfluencerFieldName, null, null);
		writer.WriteProperty(options, PropInitialAnomalyScore, value.InitialAnomalyScore, null, null);
		writer.WriteProperty(options, PropIsInterim, value.IsInterim, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropProbability, value.Probability, null, null);
		writer.WriteProperty(options, PropRawAnomalyScore, value.RawAnomalyScore, null, null);
		writer.WriteProperty(options, PropResultType, value.ResultType, null, null);
		writer.WriteProperty(options, PropTimestamp, value.Timestamp, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropTimestampString, value.TimestampString, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteNullableValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.BucketInfluencerConverter))]
public sealed partial class BucketInfluencer
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public BucketInfluencer(double anomalyScore, System.TimeSpan bucketSpan, string influencerFieldName, double initialAnomalyScore, bool isInterim, string jobId, double probability, double rawAnomalyScore, string resultType, System.DateTimeOffset timestamp)
	{
		AnomalyScore = anomalyScore;
		BucketSpan = bucketSpan;
		InfluencerFieldName = influencerFieldName;
		InitialAnomalyScore = initialAnomalyScore;
		IsInterim = isInterim;
		JobId = jobId;
		Probability = probability;
		RawAnomalyScore = rawAnomalyScore;
		ResultType = resultType;
		Timestamp = timestamp;
	}
#if NET7_0_OR_GREATER
	public BucketInfluencer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public BucketInfluencer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal BucketInfluencer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A normalized score between 0-100, which is calculated for each bucket influencer. This score might be updated as
	/// newer data is analyzed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double AnomalyScore { get; set; }

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
	/// The field name of the influencer.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string InfluencerFieldName { get; set; }

	/// <summary>
	/// <para>
	/// The score between 0-100 for each bucket influencer. This score is the initial value that was calculated at the
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
	/// The probability that the bucket has this behavior, in the range 0 to 1. This value can be held to a high precision
	/// of over 300 decimal places, so the <c>anomaly_score</c> is provided as a human-readable and friendly interpretation of
	/// this.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Probability { get; set; }

	/// <summary>
	/// <para>
	/// Internal.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double RawAnomalyScore { get; set; }

	/// <summary>
	/// <para>
	/// Internal. This value is always set to <c>bucket_influencer</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ResultType { get; set; }

	/// <summary>
	/// <para>
	/// The start time of the bucket for which these results were calculated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTimeOffset Timestamp { get; set; }

	/// <summary>
	/// <para>
	/// The start time of the bucket for which these results were calculated.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? TimestampString { get; set; }
}