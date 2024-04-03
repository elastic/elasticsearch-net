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

namespace Elastic.Clients.Elasticsearch.Serverless.TransformManagement;

public sealed partial class CheckpointStats
{
	[JsonInclude, JsonPropertyName("checkpoint")]
	public long Checkpoint { get; init; }
	[JsonInclude, JsonPropertyName("checkpoint_progress")]
	public Elastic.Clients.Elasticsearch.Serverless.TransformManagement.TransformProgress? CheckpointProgress { get; init; }
	[JsonInclude, JsonPropertyName("timestamp")]
	public DateTimeOffset? Timestamp { get; init; }
	[JsonInclude, JsonPropertyName("timestamp_millis")]
	public long? TimestampMillis { get; init; }
	[JsonInclude, JsonPropertyName("time_upper_bound")]
	public DateTimeOffset? TimeUpperBound { get; init; }
	[JsonInclude, JsonPropertyName("time_upper_bound_millis")]
	public long? TimeUpperBoundMillis { get; init; }
}