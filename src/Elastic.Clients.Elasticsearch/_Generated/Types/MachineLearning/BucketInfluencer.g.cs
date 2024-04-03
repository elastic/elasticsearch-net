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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class BucketInfluencer
{
	/// <summary>
	/// <para>A normalized score between 0-100, which is calculated for each bucket influencer. This score might be updated as<br/>newer data is analyzed.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("anomaly_score")]
	public double AnomalyScore { get; init; }

	/// <summary>
	/// <para>The length of the bucket in seconds. This value matches the bucket span that is specified in the job.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("bucket_span")]
	public long BucketSpan { get; init; }

	/// <summary>
	/// <para>The field name of the influencer.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("influencer_field_name")]
	public string InfluencerFieldName { get; init; }

	/// <summary>
	/// <para>The score between 0-100 for each bucket influencer. This score is the initial value that was calculated at the<br/>time the bucket was processed.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("initial_anomaly_score")]
	public double InitialAnomalyScore { get; init; }

	/// <summary>
	/// <para>If true, this is an interim result. In other words, the results are calculated based on partial input data.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("is_interim")]
	public bool IsInterim { get; init; }

	/// <summary>
	/// <para>Identifier for the anomaly detection job.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("job_id")]
	public string JobId { get; init; }

	/// <summary>
	/// <para>The probability that the bucket has this behavior, in the range 0 to 1. This value can be held to a high precision<br/>of over 300 decimal places, so the `anomaly_score` is provided as a human-readable and friendly interpretation of<br/>this.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("probability")]
	public double Probability { get; init; }

	/// <summary>
	/// <para>Internal.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("raw_anomaly_score")]
	public double RawAnomalyScore { get; init; }

	/// <summary>
	/// <para>Internal. This value is always set to `bucket_influencer`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("result_type")]
	public string ResultType { get; init; }

	/// <summary>
	/// <para>The start time of the bucket for which these results were calculated.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("timestamp")]
	public long Timestamp { get; init; }

	/// <summary>
	/// <para>The start time of the bucket for which these results were calculated.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("timestamp_string")]
	public DateTimeOffset? TimestampString { get; init; }
}