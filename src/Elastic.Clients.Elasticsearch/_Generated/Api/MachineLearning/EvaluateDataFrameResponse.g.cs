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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class EvaluateDataFrameResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("classification")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeClassificationSummary? Classification { get; init; }
	[JsonInclude, JsonPropertyName("outlier_detection")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeOutlierDetectionSummary? OutlierDetection { get; init; }
	[JsonInclude, JsonPropertyName("regression")]
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeRegressionSummary? Regression { get; init; }
}