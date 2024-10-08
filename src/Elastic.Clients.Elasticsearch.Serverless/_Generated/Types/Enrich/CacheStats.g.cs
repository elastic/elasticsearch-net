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

namespace Elastic.Clients.Elasticsearch.Serverless.Enrich;

public sealed partial class CacheStats
{
	[JsonInclude, JsonPropertyName("count")]
	public int Count { get; init; }
	[JsonInclude, JsonPropertyName("evictions")]
	public int Evictions { get; init; }
	[JsonInclude, JsonPropertyName("hits")]
	public int Hits { get; init; }
	[JsonInclude, JsonPropertyName("hits_time_in_millis")]
	public long HitsTimeInMillis { get; init; }
	[JsonInclude, JsonPropertyName("misses")]
	public int Misses { get; init; }
	[JsonInclude, JsonPropertyName("misses_time_in_millis")]
	public long MissesTimeInMillis { get; init; }
	[JsonInclude, JsonPropertyName("node_id")]
	public string NodeId { get; init; }
	[JsonInclude, JsonPropertyName("size_in_bytes")]
	public long SizeInBytes { get; init; }
}