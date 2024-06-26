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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.TransformManagement;

public sealed partial class UpdateTransformResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("authorization")]
	public Elastic.Clients.Elasticsearch.MachineLearning.TransformAuthorization? Authorization { get; init; }
	[JsonInclude, JsonPropertyName("create_time")]
	public long CreateTime { get; init; }
	[JsonInclude, JsonPropertyName("description")]
	public string Description { get; init; }
	[JsonInclude, JsonPropertyName("dest")]
	public Elastic.Clients.Elasticsearch.Core.Reindex.Destination Dest { get; init; }
	[JsonInclude, JsonPropertyName("frequency")]
	public Elastic.Clients.Elasticsearch.Duration? Frequency { get; init; }
	[JsonInclude, JsonPropertyName("id")]
	public string Id { get; init; }
	[JsonInclude, JsonPropertyName("latest")]
	public Elastic.Clients.Elasticsearch.TransformManagement.Latest? Latest { get; init; }
	[JsonInclude, JsonPropertyName("_meta")]
	public IReadOnlyDictionary<string, object>? Meta { get; init; }
	[JsonInclude, JsonPropertyName("pivot")]
	public Elastic.Clients.Elasticsearch.TransformManagement.Pivot? Pivot { get; init; }
	[JsonInclude, JsonPropertyName("retention_policy")]
	public Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? RetentionPolicy { get; init; }
	[JsonInclude, JsonPropertyName("settings")]
	public Elastic.Clients.Elasticsearch.TransformManagement.Settings Settings { get; init; }
	[JsonInclude, JsonPropertyName("source")]
	public Elastic.Clients.Elasticsearch.Core.Reindex.Source Source { get; init; }
	[JsonInclude, JsonPropertyName("sync")]
	public Elastic.Clients.Elasticsearch.TransformManagement.Sync? Sync { get; init; }
	[JsonInclude, JsonPropertyName("version")]
	public string Version { get; init; }
}