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

namespace Elastic.Clients.Elasticsearch.Tasks;

public sealed partial class NodeTasks
{
	[JsonInclude, JsonPropertyName("attributes")]
	public IReadOnlyDictionary<string, string>? Attributes { get; init; }
	[JsonInclude, JsonPropertyName("host")]
	public string? Host { get; init; }
	[JsonInclude, JsonPropertyName("ip")]
	public string? Ip { get; init; }
	[JsonInclude, JsonPropertyName("name")]
	public string? Name { get; init; }
	[JsonInclude, JsonPropertyName("roles")]
	public IReadOnlyCollection<string>? Roles { get; init; }
	[JsonInclude, JsonPropertyName("tasks")]
	public IReadOnlyDictionary<Elastic.Clients.Elasticsearch.TaskId, Elastic.Clients.Elasticsearch.Tasks.TaskInfo> Tasks { get; init; }
	[JsonInclude, JsonPropertyName("transport_address")]
	public string? TransportAddress { get; init; }
}