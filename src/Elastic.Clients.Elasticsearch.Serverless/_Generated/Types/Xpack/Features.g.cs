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

namespace Elastic.Clients.Elasticsearch.Serverless.Xpack;

public sealed partial class Features
{
	[JsonInclude, JsonPropertyName("aggregate_metric")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature AggregateMetric { get; init; }
	[JsonInclude, JsonPropertyName("analytics")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Analytics { get; init; }
	[JsonInclude, JsonPropertyName("ccr")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Ccr { get; init; }
	[JsonInclude, JsonPropertyName("data_streams")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature DataStreams { get; init; }
	[JsonInclude, JsonPropertyName("data_tiers")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature DataTiers { get; init; }
	[JsonInclude, JsonPropertyName("enrich")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Enrich { get; init; }
	[JsonInclude, JsonPropertyName("eql")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Eql { get; init; }
	[JsonInclude, JsonPropertyName("frozen_indices")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature FrozenIndices { get; init; }
	[JsonInclude, JsonPropertyName("graph")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Graph { get; init; }
	[JsonInclude, JsonPropertyName("ilm")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Ilm { get; init; }
	[JsonInclude, JsonPropertyName("logstash")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Logstash { get; init; }
	[JsonInclude, JsonPropertyName("ml")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Ml { get; init; }
	[JsonInclude, JsonPropertyName("monitoring")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Monitoring { get; init; }
	[JsonInclude, JsonPropertyName("rollup")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Rollup { get; init; }
	[JsonInclude, JsonPropertyName("runtime_fields")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature? RuntimeFields { get; init; }
	[JsonInclude, JsonPropertyName("searchable_snapshots")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature SearchableSnapshots { get; init; }
	[JsonInclude, JsonPropertyName("security")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Security { get; init; }
	[JsonInclude, JsonPropertyName("slm")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Slm { get; init; }
	[JsonInclude, JsonPropertyName("spatial")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Spatial { get; init; }
	[JsonInclude, JsonPropertyName("sql")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Sql { get; init; }
	[JsonInclude, JsonPropertyName("transform")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Transform { get; init; }
	[JsonInclude, JsonPropertyName("voting_only")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature VotingOnly { get; init; }
	[JsonInclude, JsonPropertyName("watcher")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Feature Watcher { get; init; }
}