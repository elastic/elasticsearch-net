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

namespace Elastic.Clients.Elasticsearch.Serverless.Cluster;

public sealed partial class ShardHealthStats
{
	[JsonInclude, JsonPropertyName("active_shards")]
	public int ActiveShards { get; init; }
	[JsonInclude, JsonPropertyName("initializing_shards")]
	public int InitializingShards { get; init; }
	[JsonInclude, JsonPropertyName("primary_active")]
	public bool PrimaryActive { get; init; }
	[JsonInclude, JsonPropertyName("relocating_shards")]
	public int RelocatingShards { get; init; }
	[JsonInclude, JsonPropertyName("status")]
	public Elastic.Clients.Elasticsearch.Serverless.HealthStatus Status { get; init; }
	[JsonInclude, JsonPropertyName("unassigned_primary_shards")]
	public int UnassignedPrimaryShards { get; init; }
	[JsonInclude, JsonPropertyName("unassigned_shards")]
	public int UnassignedShards { get; init; }
}