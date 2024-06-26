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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Xpack;

public sealed partial class JobUsage
{
	[JsonInclude, JsonPropertyName("count")]
	public int Count { get; init; }
	[JsonInclude, JsonPropertyName("created_by")]
	public IReadOnlyDictionary<string, long> CreatedBy { get; init; }
	[JsonInclude, JsonPropertyName("detectors")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.JobStatistics Detectors { get; init; }
	[JsonInclude, JsonPropertyName("forecasts")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.MlJobForecasts Forecasts { get; init; }
	[JsonInclude, JsonPropertyName("model_size")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.JobStatistics ModelSize { get; init; }
}