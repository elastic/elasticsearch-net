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

namespace Elastic.Clients.Elasticsearch.Serverless.Core.HealthReport;

public sealed partial class ShardsAvailabilityIndicatorDetails
{
	[JsonInclude, JsonPropertyName("creating_primaries")]
	public long CreatingPrimaries { get; init; }
	[JsonInclude, JsonPropertyName("creating_replicas")]
	public long CreatingReplicas { get; init; }
	[JsonInclude, JsonPropertyName("initializing_primaries")]
	public long InitializingPrimaries { get; init; }
	[JsonInclude, JsonPropertyName("initializing_replicas")]
	public long InitializingReplicas { get; init; }
	[JsonInclude, JsonPropertyName("restarting_primaries")]
	public long RestartingPrimaries { get; init; }
	[JsonInclude, JsonPropertyName("restarting_replicas")]
	public long RestartingReplicas { get; init; }
	[JsonInclude, JsonPropertyName("started_primaries")]
	public long StartedPrimaries { get; init; }
	[JsonInclude, JsonPropertyName("started_replicas")]
	public long StartedReplicas { get; init; }
	[JsonInclude, JsonPropertyName("unassigned_primaries")]
	public long UnassignedPrimaries { get; init; }
	[JsonInclude, JsonPropertyName("unassigned_replicas")]
	public long UnassignedReplicas { get; init; }
}