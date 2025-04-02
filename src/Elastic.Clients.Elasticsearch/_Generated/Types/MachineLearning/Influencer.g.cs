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

internal sealed partial class InfluencerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.Influencer>
{
	private static readonly System.Text.Json.JsonEncodedText PropBucketSpan = System.Text.Json.JsonEncodedText.Encode("bucket_span");
	private static readonly System.Text.Json.JsonEncodedText PropFoo = System.Text.Json.JsonEncodedText.Encode("foo");
	private static readonly System.Text.Json.JsonEncodedText PropInfluencerFieldName = System.Text.Json.JsonEncodedText.Encode("influencer_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropInfluencerFieldValue = System.Text.Json.JsonEncodedText.Encode("influencer_field_value");
	private static readonly System.Text.Json.JsonEncodedText PropInfluencerScore = System.Text.Json.JsonEncodedText.Encode("influencer_score");
	private static readonly System.Text.Json.JsonEncodedText PropInitialInfluencerScore = System.Text.Json.JsonEncodedText.Encode("initial_influencer_score");
	private static readonly System.Text.Json.JsonEncodedText PropIsInterim = System.Text.Json.JsonEncodedText.Encode("is_interim");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropProbability = System.Text.Json.JsonEncodedText.Encode("probability");
	private static readonly System.Text.Json.JsonEncodedText PropResultType = System.Text.Json.JsonEncodedText.Encode("result_type");
	private static readonly System.Text.Json.JsonEncodedText PropTimestamp = System.Text.Json.JsonEncodedText.Encode("timestamp");

	public override Elastic.Clients.Elasticsearch.MachineLearning.Influencer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.TimeSpan> propBucketSpan = default;
		LocalJsonValue<string?> propFoo = default;
		LocalJsonValue<string> propInfluencerFieldName = default;
		LocalJsonValue<string> propInfluencerFieldValue = default;
		LocalJsonValue<double> propInfluencerScore = default;
		LocalJsonValue<double> propInitialInfluencerScore = default;
		LocalJsonValue<bool> propIsInterim = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<double> propProbability = default;
		LocalJsonValue<string> propResultType = default;
		LocalJsonValue<System.DateTime> propTimestamp = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBucketSpan.TryReadProperty(ref reader, options, PropBucketSpan, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker))))
			{
				continue;
			}

			if (propFoo.TryReadProperty(ref reader, options, PropFoo, null))
			{
				continue;
			}

			if (propInfluencerFieldName.TryReadProperty(ref reader, options, PropInfluencerFieldName, null))
			{
				continue;
			}

			if (propInfluencerFieldValue.TryReadProperty(ref reader, options, PropInfluencerFieldValue, null))
			{
				continue;
			}

			if (propInfluencerScore.TryReadProperty(ref reader, options, PropInfluencerScore, null))
			{
				continue;
			}

			if (propInitialInfluencerScore.TryReadProperty(ref reader, options, PropInitialInfluencerScore, null))
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

			if (propResultType.TryReadProperty(ref reader, options, PropResultType, null))
			{
				continue;
			}

			if (propTimestamp.TryReadProperty(ref reader, options, PropTimestamp, static System.DateTime (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.Influencer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BucketSpan = propBucketSpan.Value,
			Foo = propFoo.Value,
			InfluencerFieldName = propInfluencerFieldName.Value,
			InfluencerFieldValue = propInfluencerFieldValue.Value,
			InfluencerScore = propInfluencerScore.Value,
			InitialInfluencerScore = propInitialInfluencerScore.Value,
			IsInterim = propIsInterim.Value,
			JobId = propJobId.Value,
			Probability = propProbability.Value,
			ResultType = propResultType.Value,
			Timestamp = propTimestamp.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.Influencer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBucketSpan, value.BucketSpan, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker)));
		writer.WriteProperty(options, PropFoo, value.Foo, null, null);
		writer.WriteProperty(options, PropInfluencerFieldName, value.InfluencerFieldName, null, null);
		writer.WriteProperty(options, PropInfluencerFieldValue, value.InfluencerFieldValue, null, null);
		writer.WriteProperty(options, PropInfluencerScore, value.InfluencerScore, null, null);
		writer.WriteProperty(options, PropInitialInfluencerScore, value.InitialInfluencerScore, null, null);
		writer.WriteProperty(options, PropIsInterim, value.IsInterim, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropProbability, value.Probability, null, null);
		writer.WriteProperty(options, PropResultType, value.ResultType, null, null);
		writer.WriteProperty(options, PropTimestamp, value.Timestamp, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime v) => w.WriteValueEx<System.DateTime>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.InfluencerConverter))]
public sealed partial class Influencer
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Influencer(System.TimeSpan bucketSpan, string influencerFieldName, string influencerFieldValue, double influencerScore, double initialInfluencerScore, bool isInterim, string jobId, double probability, string resultType, System.DateTime timestamp)
	{
		BucketSpan = bucketSpan;
		InfluencerFieldName = influencerFieldName;
		InfluencerFieldValue = influencerFieldValue;
		InfluencerScore = influencerScore;
		InitialInfluencerScore = initialInfluencerScore;
		IsInterim = isInterim;
		JobId = jobId;
		Probability = probability;
		ResultType = resultType;
		Timestamp = timestamp;
	}
#if NET7_0_OR_GREATER
	public Influencer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Influencer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Influencer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

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
	/// Additional influencer properties are added, depending on the fields being analyzed. For example, if it’s
	/// analyzing <c>user_name</c> as an influencer, a field <c>user_name</c> is added to the result document. This
	/// information enables you to filter the anomaly results more easily.
	/// </para>
	/// </summary>
	public string? Foo { get; set; }

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
	/// The entity that influenced, contributed to, or was to blame for the anomaly.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string InfluencerFieldValue { get; set; }

	/// <summary>
	/// <para>
	/// A normalized score between 0-100, which is based on the probability of the influencer in this bucket aggregated
	/// across detectors. Unlike <c>initial_influencer_score</c>, this value is updated by a re-normalization process as new
	/// data is analyzed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double InfluencerScore { get; set; }

	/// <summary>
	/// <para>
	/// A normalized score between 0-100, which is based on the probability of the influencer aggregated across detectors.
	/// This is the initial value that was calculated at the time the bucket was processed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double InitialInfluencerScore { get; set; }

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
	/// The probability that the influencer has this behavior, in the range 0 to 1. This value can be held to a high
	/// precision of over 300 decimal places, so the <c>influencer_score</c> is provided as a human-readable and friendly
	/// interpretation of this value.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Probability { get; set; }

	/// <summary>
	/// <para>
	/// Internal. This value is always set to <c>influencer</c>.
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
	System.DateTime Timestamp { get; set; }
}