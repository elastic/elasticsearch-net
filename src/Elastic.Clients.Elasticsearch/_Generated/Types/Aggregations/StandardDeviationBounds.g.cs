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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations;
public sealed partial class StandardDeviationBounds
{
	[JsonInclude]
	[JsonPropertyName("lower")]
	public double? Lower { get; init; }

	[JsonInclude]
	[JsonPropertyName("lower_population")]
	public double? LowerPopulation { get; init; }

	[JsonInclude]
	[JsonPropertyName("lower_sampling")]
	public double? LowerSampling { get; init; }

	[JsonInclude]
	[JsonPropertyName("upper")]
	public double? Upper { get; init; }

	[JsonInclude]
	[JsonPropertyName("upper_population")]
	public double? UpperPopulation { get; init; }

	[JsonInclude]
	[JsonPropertyName("upper_sampling")]
	public double? UpperSampling { get; init; }
}