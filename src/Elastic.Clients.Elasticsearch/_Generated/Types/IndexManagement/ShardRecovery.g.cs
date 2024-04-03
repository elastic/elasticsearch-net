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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class ShardRecovery
{
	[JsonInclude, JsonPropertyName("id")]
	public long Id { get; init; }
	[JsonInclude, JsonPropertyName("index")]
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryIndexStatus Index { get; init; }
	[JsonInclude, JsonPropertyName("primary")]
	public bool Primary { get; init; }
	[JsonInclude, JsonPropertyName("source")]
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryOrigin Source { get; init; }
	[JsonInclude, JsonPropertyName("stage")]
	public string Stage { get; init; }
	[JsonInclude, JsonPropertyName("start")]
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryStartStatus? Start { get; init; }
	[JsonInclude, JsonPropertyName("start_time")]
	public DateTimeOffset? StartTime { get; init; }
	[JsonInclude, JsonPropertyName("start_time_in_millis")]
	public long StartTimeInMillis { get; init; }
	[JsonInclude, JsonPropertyName("stop_time")]
	public DateTimeOffset? StopTime { get; init; }
	[JsonInclude, JsonPropertyName("stop_time_in_millis")]
	public long? StopTimeInMillis { get; init; }
	[JsonInclude, JsonPropertyName("target")]
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryOrigin Target { get; init; }
	[JsonInclude, JsonPropertyName("total_time")]
	public Elastic.Clients.Elasticsearch.Duration? TotalTime { get; init; }
	[JsonInclude, JsonPropertyName("total_time_in_millis")]
	public long TotalTimeInMillis { get; init; }
	[JsonInclude, JsonPropertyName("translog")]
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogStatus Translog { get; init; }
	[JsonInclude, JsonPropertyName("type")]
	public string Type { get; init; }
	[JsonInclude, JsonPropertyName("verify_index")]
	public Elastic.Clients.Elasticsearch.IndexManagement.VerifyIndex VerifyIndex { get; init; }
}