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

namespace Elastic.Clients.Elasticsearch.Rollup;

public sealed partial class RollupJobStats
{
	[JsonInclude, JsonPropertyName("documents_processed")]
	public long DocumentsProcessed { get; init; }
	[JsonInclude, JsonPropertyName("index_failures")]
	public long IndexFailures { get; init; }
	[JsonInclude, JsonPropertyName("index_time_in_ms")]
	public long IndexTimeInMs { get; init; }
	[JsonInclude, JsonPropertyName("index_total")]
	public long IndexTotal { get; init; }
	[JsonInclude, JsonPropertyName("pages_processed")]
	public long PagesProcessed { get; init; }
	[JsonInclude, JsonPropertyName("processing_time_in_ms")]
	public long ProcessingTimeInMs { get; init; }
	[JsonInclude, JsonPropertyName("processing_total")]
	public long ProcessingTotal { get; init; }
	[JsonInclude, JsonPropertyName("rollups_indexed")]
	public long RollupsIndexed { get; init; }
	[JsonInclude, JsonPropertyName("search_failures")]
	public long SearchFailures { get; init; }
	[JsonInclude, JsonPropertyName("search_time_in_ms")]
	public long SearchTimeInMs { get; init; }
	[JsonInclude, JsonPropertyName("search_total")]
	public long SearchTotal { get; init; }
	[JsonInclude, JsonPropertyName("trigger_count")]
	public long TriggerCount { get; init; }
}