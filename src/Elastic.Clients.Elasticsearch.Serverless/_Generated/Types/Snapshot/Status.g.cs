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

namespace Elastic.Clients.Elasticsearch.Serverless.Snapshot;

public sealed partial class Status
{
	[JsonInclude, JsonPropertyName("include_global_state")]
	public bool IncludeGlobalState { get; init; }
	[JsonInclude, JsonPropertyName("indices")]
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Snapshot.SnapshotIndexStats> Indices { get; init; }
	[JsonInclude, JsonPropertyName("repository")]
	public string Repository { get; init; }
	[JsonInclude, JsonPropertyName("shards_stats")]
	public Elastic.Clients.Elasticsearch.Serverless.Snapshot.ShardsStats ShardsStats { get; init; }
	[JsonInclude, JsonPropertyName("snapshot")]
	public string Snapshot { get; init; }
	[JsonInclude, JsonPropertyName("state")]
	public string State { get; init; }
	[JsonInclude, JsonPropertyName("stats")]
	public Elastic.Clients.Elasticsearch.Serverless.Snapshot.SnapshotStats Stats { get; init; }
	[JsonInclude, JsonPropertyName("uuid")]
	public string Uuid { get; init; }
}