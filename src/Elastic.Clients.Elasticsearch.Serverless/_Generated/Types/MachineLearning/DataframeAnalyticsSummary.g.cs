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

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class DataframeAnalyticsSummary
{
	[JsonInclude, JsonPropertyName("allow_lazy_start")]
	public bool? AllowLazyStart { get; init; }
	[JsonInclude, JsonPropertyName("analysis")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.DataframeAnalysis Analysis { get; init; }
	[JsonInclude, JsonPropertyName("analyzed_fields")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.DataframeAnalysisAnalyzedFields? AnalyzedFields { get; init; }

	/// <summary>
	/// <para>The security privileges that the job uses to run its queries. If Elastic Stack security features were disabled at the time of the most recent update to the job, this property is omitted.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("authorization")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.DataframeAnalyticsAuthorization? Authorization { get; init; }
	[JsonInclude, JsonPropertyName("create_time")]
	public long? CreateTime { get; init; }
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; init; }
	[JsonInclude, JsonPropertyName("dest")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.DataframeAnalyticsDestination Dest { get; init; }
	[JsonInclude, JsonPropertyName("id")]
	public string Id { get; init; }
	[JsonInclude, JsonPropertyName("max_num_threads")]
	public int? MaxNumThreads { get; init; }
	[JsonInclude, JsonPropertyName("model_memory_limit")]
	public string? ModelMemoryLimit { get; init; }
	[JsonInclude, JsonPropertyName("source")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.DataframeAnalyticsSource Source { get; init; }
	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; init; }
}