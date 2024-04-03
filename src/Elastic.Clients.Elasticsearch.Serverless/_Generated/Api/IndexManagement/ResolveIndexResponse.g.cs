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

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class ResolveIndexResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("aliases")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ResolveIndexAliasItem> Aliases { get; init; }
	[JsonInclude, JsonPropertyName("data_streams")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ResolveIndexDataStreamsItem> DataStreams { get; init; }
	[JsonInclude, JsonPropertyName("indices")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.ResolveIndexItem> Indices { get; init; }
}