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

internal sealed partial class OverallBucketConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.OverallBucket>
{
	private static readonly System.Text.Json.JsonEncodedText PropBucketSpan = System.Text.Json.JsonEncodedText.Encode("bucket_span");
	private static readonly System.Text.Json.JsonEncodedText PropIsInterim = System.Text.Json.JsonEncodedText.Encode("is_interim");
	private static readonly System.Text.Json.JsonEncodedText PropJobs = System.Text.Json.JsonEncodedText.Encode("jobs");
	private static readonly System.Text.Json.JsonEncodedText PropOverallScore = System.Text.Json.JsonEncodedText.Encode("overall_score");
	private static readonly System.Text.Json.JsonEncodedText PropResultType = System.Text.Json.JsonEncodedText.Encode("result_type");
	private static readonly System.Text.Json.JsonEncodedText PropTimestamp = System.Text.Json.JsonEncodedText.Encode("timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropTimestampString = System.Text.Json.JsonEncodedText.Encode("timestamp_string");

	public override Elastic.Clients.Elasticsearch.MachineLearning.OverallBucket Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.TimeSpan> propBucketSpan = default;
		LocalJsonValue<bool> propIsInterim = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.OverallBucketJob>> propJobs = default;
		LocalJsonValue<double> propOverallScore = default;
		LocalJsonValue<string> propResultType = default;
		LocalJsonValue<System.DateTimeOffset> propTimestamp = default;
		LocalJsonValue<System.DateTimeOffset?> propTimestampString = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBucketSpan.TryReadProperty(ref reader, options, PropBucketSpan, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker))))
			{
				continue;
			}

			if (propIsInterim.TryReadProperty(ref reader, options, PropIsInterim, null))
			{
				continue;
			}

			if (propJobs.TryReadProperty(ref reader, options, PropJobs, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.OverallBucketJob> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.OverallBucketJob>(o, null)!))
			{
				continue;
			}

			if (propOverallScore.TryReadProperty(ref reader, options, PropOverallScore, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.OverallBucket(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BucketSpan = propBucketSpan.Value,
			IsInterim = propIsInterim.Value,
			Jobs = propJobs.Value,
			OverallScore = propOverallScore.Value,
			ResultType = propResultType.Value,
			Timestamp = propTimestamp.Value,
			TimestampString = propTimestampString.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.OverallBucket value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBucketSpan, value.BucketSpan, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker)));
		writer.WriteProperty(options, PropIsInterim, value.IsInterim, null, null);
		writer.WriteProperty(options, PropJobs, value.Jobs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.OverallBucketJob> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.OverallBucketJob>(o, v, null));
		writer.WriteProperty(options, PropOverallScore, value.OverallScore, null, null);
		writer.WriteProperty(options, PropResultType, value.ResultType, null, null);
		writer.WriteProperty(options, PropTimestamp, value.Timestamp, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropTimestampString, value.TimestampString, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteNullableValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.OverallBucketConverter))]
public sealed partial class OverallBucket
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public OverallBucket(System.TimeSpan bucketSpan, bool isInterim, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.OverallBucketJob> jobs, double overallScore, string resultType, System.DateTimeOffset timestamp)
	{
		BucketSpan = bucketSpan;
		IsInterim = isInterim;
		Jobs = jobs;
		OverallScore = overallScore;
		ResultType = resultType;
		Timestamp = timestamp;
	}
#if NET7_0_OR_GREATER
	public OverallBucket()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public OverallBucket()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal OverallBucket(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The length of the bucket in seconds. Matches the job with the longest bucket_span value.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan BucketSpan { get; set; }

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
	/// An array of objects that contain the max_anomaly_score per job_id.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.OverallBucketJob> Jobs { get; set; }

	/// <summary>
	/// <para>
	/// The top_n average of the maximum bucket anomaly_score per job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double OverallScore { get; set; }

	/// <summary>
	/// <para>
	/// Internal. This is always set to overall_bucket.
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