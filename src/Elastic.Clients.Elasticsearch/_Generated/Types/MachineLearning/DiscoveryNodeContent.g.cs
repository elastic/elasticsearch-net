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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class DiscoveryNodeContent
{
	[JsonInclude, JsonPropertyName("attributes")]
	public IReadOnlyDictionary<string, string> Attributes { get; init; }
	[JsonInclude, JsonPropertyName("ephemeral_id")]
	public string EphemeralId { get; init; }
	[JsonInclude, JsonPropertyName("external_id")]
	public string ExternalId { get; init; }
	[JsonInclude, JsonPropertyName("max_index_version")]
	public int MaxIndexVersion { get; init; }
	[JsonInclude, JsonPropertyName("min_index_version")]
	public int MinIndexVersion { get; init; }
	[JsonInclude, JsonPropertyName("name")]
	public string? Name { get; init; }
	[JsonInclude, JsonPropertyName("roles")]
	public IReadOnlyCollection<string> Roles { get; init; }
	[JsonInclude, JsonPropertyName("transport_address")]
	public string TransportAddress { get; init; }
	[JsonInclude, JsonPropertyName("version")]
	public string Version { get; init; }
}