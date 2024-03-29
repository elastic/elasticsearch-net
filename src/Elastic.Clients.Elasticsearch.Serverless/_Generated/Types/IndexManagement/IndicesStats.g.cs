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

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class IndicesStats
{
	[JsonInclude, JsonPropertyName("health")]
	public Elastic.Clients.Elasticsearch.Serverless.HealthStatus? Health { get; init; }
	[JsonInclude, JsonPropertyName("primaries")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexStats? Primaries { get; init; }
	[JsonInclude, JsonPropertyName("shards")]
	public IReadOnlyDictionary<string, IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ShardStats>>? Shards { get; init; }
	[JsonInclude, JsonPropertyName("status")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexMetadataState? Status { get; init; }
	[JsonInclude, JsonPropertyName("total")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexStats? Total { get; init; }
	[JsonInclude, JsonPropertyName("uuid")]
	public string? Uuid { get; init; }
}