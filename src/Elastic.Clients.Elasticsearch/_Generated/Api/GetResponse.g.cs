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
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class GetResponse<TDocument> : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.FieldValues? Fields { get; init; }
	[JsonInclude, JsonPropertyName("found")]
	public bool Found { get; init; }
	[JsonInclude, JsonPropertyName("_id")]
	public string Id { get; init; }
	[JsonInclude, JsonPropertyName("_index")]
	public string Index { get; init; }
	[JsonInclude, JsonPropertyName("_primary_term")]
	public long? PrimaryTerm { get; init; }
	[JsonInclude, JsonPropertyName("_routing")]
	public string? Routing { get; init; }
	[JsonInclude, JsonPropertyName("_seq_no")]
	public long? SeqNo { get; init; }
	[JsonInclude, JsonPropertyName("_source")]
	[SourceConverter]
	public TDocument? Source { get; init; }
	[JsonInclude, JsonPropertyName("_version")]
	public long? Version { get; init; }
}