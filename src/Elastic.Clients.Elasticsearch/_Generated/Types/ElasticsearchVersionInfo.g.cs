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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch;
public sealed partial class ElasticsearchVersionInfo
{
	[JsonInclude, JsonPropertyName("build_date")]
	public DateTimeOffset BuildDate { get; init; }

	[JsonInclude, JsonPropertyName("build_flavor")]
	public string BuildFlavor { get; init; }

	[JsonInclude, JsonPropertyName("build_hash")]
	public string BuildHash { get; init; }

	[JsonInclude, JsonPropertyName("build_snapshot")]
	public bool BuildSnapshot { get; init; }

	[JsonInclude, JsonPropertyName("build_type")]
	public string BuildType { get; init; }

	[JsonInclude, JsonPropertyName("lucene_version")]
	public string LuceneVersion { get; init; }

	[JsonInclude, JsonPropertyName("minimum_index_compatibility_version")]
	public string MinimumIndexCompatibilityVersion { get; init; }

	[JsonInclude, JsonPropertyName("minimum_wire_compatibility_version")]
	public string MinimumWireCompatibilityVersion { get; init; }

	[JsonInclude, JsonPropertyName("number")]
	public string Number { get; init; }
}