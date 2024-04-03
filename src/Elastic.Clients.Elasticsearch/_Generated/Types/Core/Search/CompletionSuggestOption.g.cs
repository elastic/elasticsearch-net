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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Search;

public sealed partial class CompletionSuggestOption<TDocument>
{
	[JsonInclude, JsonPropertyName("collate_match")]
	public bool? CollateMatch { get; init; }
	[JsonInclude, JsonPropertyName("contexts")]
	public IReadOnlyDictionary<string, IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.Context>>? Contexts { get; init; }
	[JsonInclude, JsonPropertyName("fields")]
	public IReadOnlyDictionary<string, object>? Fields { get; init; }
	[JsonInclude, JsonPropertyName("_id")]
	public string? Id { get; init; }
	[JsonInclude, JsonPropertyName("_index")]
	public string? Index { get; init; }
	[JsonInclude, JsonPropertyName("_routing")]
	public string? Routing { get; init; }
	[JsonInclude, JsonPropertyName("score")]
	public double? Score { get; init; }
	[JsonInclude, JsonPropertyName("_score")]
	public double? Score0 { get; init; }
	[JsonInclude, JsonPropertyName("_source")]
	public TDocument? Source { get; init; }
	[JsonInclude, JsonPropertyName("text")]
	public string Text { get; init; }
}