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

public sealed partial class JobTimingStats
{
	[JsonInclude, JsonPropertyName("average_bucket_processing_time_ms")]
	public double? AverageBucketProcessingTimeMs { get; init; }
	[JsonInclude, JsonPropertyName("bucket_count")]
	public long BucketCount { get; init; }
	[JsonInclude, JsonPropertyName("exponential_average_bucket_processing_time_ms")]
	public double? ExponentialAverageBucketProcessingTimeMs { get; init; }
	[JsonInclude, JsonPropertyName("exponential_average_bucket_processing_time_per_hour_ms")]
	public double ExponentialAverageBucketProcessingTimePerHourMs { get; init; }
	[JsonInclude, JsonPropertyName("job_id")]
	public string JobId { get; init; }
	[JsonInclude, JsonPropertyName("maximum_bucket_processing_time_ms")]
	public double? MaximumBucketProcessingTimeMs { get; init; }
	[JsonInclude, JsonPropertyName("minimum_bucket_processing_time_ms")]
	public double? MinimumBucketProcessingTimeMs { get; init; }
	[JsonInclude, JsonPropertyName("total_bucket_processing_time_ms")]
	public double TotalBucketProcessingTimeMs { get; init; }
}