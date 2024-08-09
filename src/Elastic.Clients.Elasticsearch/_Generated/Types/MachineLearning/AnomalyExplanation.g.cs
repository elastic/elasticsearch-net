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

public sealed partial class AnomalyExplanation
{
	/// <summary>
	/// <para>
	/// Impact from the duration and magnitude of the detected anomaly relative to the historical average.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("anomaly_characteristics_impact")]
	public int? AnomalyCharacteristicsImpact { get; init; }

	/// <summary>
	/// <para>
	/// Length of the detected anomaly in the number of buckets.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("anomaly_length")]
	public int? AnomalyLength { get; init; }

	/// <summary>
	/// <para>
	/// Type of the detected anomaly: <c>spike</c> or <c>dip</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("anomaly_type")]
	public string? AnomalyType { get; init; }

	/// <summary>
	/// <para>
	/// Indicates reduction of anomaly score for the bucket with large confidence intervals. If a bucket has large confidence intervals, the score is reduced.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("high_variance_penalty")]
	public bool? HighVariancePenalty { get; init; }

	/// <summary>
	/// <para>
	/// If the bucket contains fewer samples than expected, the score is reduced.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("incomplete_bucket_penalty")]
	public bool? IncompleteBucketPenalty { get; init; }

	/// <summary>
	/// <para>
	/// Lower bound of the 95% confidence interval.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("lower_confidence_bound")]
	public double? LowerConfidenceBound { get; init; }

	/// <summary>
	/// <para>
	/// Impact of the deviation between actual and typical values in the past 12 buckets.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("multi_bucket_impact")]
	public int? MultiBucketImpact { get; init; }

	/// <summary>
	/// <para>
	/// Impact of the deviation between actual and typical values in the current bucket.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("single_bucket_impact")]
	public int? SingleBucketImpact { get; init; }

	/// <summary>
	/// <para>
	/// Typical (expected) value for this bucket.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("typical_value")]
	public double? TypicalValue { get; init; }

	/// <summary>
	/// <para>
	/// Upper bound of the 95% confidence interval.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("upper_confidence_bound")]
	public double? UpperConfidenceBound { get; init; }
}