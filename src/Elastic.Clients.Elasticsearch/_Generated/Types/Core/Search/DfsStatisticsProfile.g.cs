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

public sealed partial class DfsStatisticsProfile
{
	[JsonInclude, JsonPropertyName("breakdown")]
	public Elastic.Clients.Elasticsearch.Core.Search.DfsStatisticsBreakdown Breakdown { get; init; }
	[JsonInclude, JsonPropertyName("children")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.DfsStatisticsProfile>? Children { get; init; }
	[JsonInclude, JsonPropertyName("debug")]
	public IReadOnlyDictionary<string, object>? Debug { get; init; }
	[JsonInclude, JsonPropertyName("description")]
	public string Description { get; init; }
	[JsonInclude, JsonPropertyName("time")]
	public Elastic.Clients.Elasticsearch.Duration? Time { get; init; }
	[JsonInclude, JsonPropertyName("time_in_nanos")]
	public long TimeInNanos { get; init; }
	[JsonInclude, JsonPropertyName("type")]
	public string Type { get; init; }
}