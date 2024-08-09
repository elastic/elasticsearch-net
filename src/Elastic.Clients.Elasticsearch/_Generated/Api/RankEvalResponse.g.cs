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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class RankEvalResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// The details section contains one entry for every query in the original requests section, keyed by the search request id
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("details")]
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDetail> Details { get; init; }
	[JsonInclude, JsonPropertyName("failures")]
	public IReadOnlyDictionary<string, object> Failures { get; init; }

	/// <summary>
	/// <para>
	/// The overall evaluation quality calculated by the defined metric
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("metric_score")]
	public double MetricScore { get; init; }
}