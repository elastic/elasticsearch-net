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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Xpack;

public sealed partial class XpackUsageResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("aggregate_metric")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base AggregateMetric { get; init; }
	[JsonInclude, JsonPropertyName("analytics")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Analytics Analytics { get; init; }
	[JsonInclude, JsonPropertyName("archive")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Archive Archive { get; init; }
	[JsonInclude, JsonPropertyName("ccr")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Ccr Ccr { get; init; }
	[JsonInclude, JsonPropertyName("data_frame")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base? DataFrame { get; init; }
	[JsonInclude, JsonPropertyName("data_science")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base? DataScience { get; init; }
	[JsonInclude, JsonPropertyName("data_streams")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.DataStreams? DataStreams { get; init; }
	[JsonInclude, JsonPropertyName("data_tiers")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.DataTiers DataTiers { get; init; }
	[JsonInclude, JsonPropertyName("enrich")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base? Enrich { get; init; }
	[JsonInclude, JsonPropertyName("eql")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Eql Eql { get; init; }
	[JsonInclude, JsonPropertyName("flattened")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Flattened? Flattened { get; init; }
	[JsonInclude, JsonPropertyName("frozen_indices")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.FrozenIndices FrozenIndices { get; init; }
	[JsonInclude, JsonPropertyName("graph")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base Graph { get; init; }
	[JsonInclude, JsonPropertyName("health_api")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.HealthStatistics? HealthApi { get; init; }
	[JsonInclude, JsonPropertyName("ilm")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Ilm Ilm { get; init; }
	[JsonInclude, JsonPropertyName("logstash")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base Logstash { get; init; }
	[JsonInclude, JsonPropertyName("ml")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.MachineLearning Ml { get; init; }
	[JsonInclude, JsonPropertyName("monitoring")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Monitoring Monitoring { get; init; }
	[JsonInclude, JsonPropertyName("rollup")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base Rollup { get; init; }
	[JsonInclude, JsonPropertyName("runtime_fields")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.RuntimeFieldTypes? RuntimeFields { get; init; }
	[JsonInclude, JsonPropertyName("searchable_snapshots")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.SearchableSnapshots SearchableSnapshots { get; init; }
	[JsonInclude, JsonPropertyName("security")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Security Security { get; init; }
	[JsonInclude, JsonPropertyName("slm")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Slm Slm { get; init; }
	[JsonInclude, JsonPropertyName("spatial")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base Spatial { get; init; }
	[JsonInclude, JsonPropertyName("sql")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Sql Sql { get; init; }
	[JsonInclude, JsonPropertyName("transform")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base Transform { get; init; }
	[JsonInclude, JsonPropertyName("vectors")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Vector? Vectors { get; init; }
	[JsonInclude, JsonPropertyName("voting_only")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Base VotingOnly { get; init; }
	[JsonInclude, JsonPropertyName("watcher")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.Watcher Watcher { get; init; }
}