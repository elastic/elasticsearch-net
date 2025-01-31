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

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class ExtendedStatsAggregate : IAggregate
{
	[JsonInclude, JsonPropertyName("avg")]
	public double? Avg { get; init; }
	[JsonInclude, JsonPropertyName("avg_as_string")]
	public string? AvgAsString { get; init; }
	[JsonInclude, JsonPropertyName("count")]
	public long Count { get; init; }
	[JsonInclude, JsonPropertyName("max")]
	public double? Max { get; init; }
	[JsonInclude, JsonPropertyName("max_as_string")]
	public string? MaxAsString { get; init; }
	[JsonInclude, JsonPropertyName("meta")]
	public IReadOnlyDictionary<string, object>? Meta { get; init; }
	[JsonInclude, JsonPropertyName("min")]
	public double? Min { get; init; }
	[JsonInclude, JsonPropertyName("min_as_string")]
	public string? MinAsString { get; init; }
	[JsonInclude, JsonPropertyName("std_deviation")]
	public double? StdDeviation { get; init; }
	[JsonInclude, JsonPropertyName("std_deviation_as_string")]
	public string? StdDeviationAsString { get; init; }
	[JsonInclude, JsonPropertyName("std_deviation_bounds")]
	public Elastic.Clients.Elasticsearch.Aggregations.StandardDeviationBounds? StdDeviationBounds { get; init; }
	[JsonInclude, JsonPropertyName("std_deviation_bounds_as_string")]
	public Elastic.Clients.Elasticsearch.Aggregations.StandardDeviationBoundsAsString? StdDeviationBoundsAsString { get; init; }
	[JsonInclude, JsonPropertyName("std_deviation_population")]
	public double? StdDeviationPopulation { get; init; }
	[JsonInclude, JsonPropertyName("std_deviation_sampling")]
	public double? StdDeviationSampling { get; init; }
	[JsonInclude, JsonPropertyName("sum")]
	public double Sum { get; init; }
	[JsonInclude, JsonPropertyName("sum_as_string")]
	public string? SumAsString { get; init; }
	[JsonInclude, JsonPropertyName("sum_of_squares")]
	public double? SumOfSquares { get; init; }
	[JsonInclude, JsonPropertyName("sum_of_squares_as_string")]
	public string? SumOfSquaresAsString { get; init; }
	[JsonInclude, JsonPropertyName("variance")]
	public double? Variance { get; init; }
	[JsonInclude, JsonPropertyName("variance_as_string")]
	public string? VarianceAsString { get; init; }
	[JsonInclude, JsonPropertyName("variance_population")]
	public double? VariancePopulation { get; init; }
	[JsonInclude, JsonPropertyName("variance_population_as_string")]
	public string? VariancePopulationAsString { get; init; }
	[JsonInclude, JsonPropertyName("variance_sampling")]
	public double? VarianceSampling { get; init; }
	[JsonInclude, JsonPropertyName("variance_sampling_as_string")]
	public string? VarianceSamplingAsString { get; init; }

	string IAggregate.Type => "extended_stats";
}