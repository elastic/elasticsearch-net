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

public sealed partial class GetDataFrameAnalyticsStatsResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("count")]
	public long Count { get; init; }

	/// <summary>
	/// <para>
	/// An array of objects that contain usage information for data frame analytics jobs, which are sorted by the id value in ascending order.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("data_frame_analytics")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics> DataFrameAnalytics { get; init; }
}