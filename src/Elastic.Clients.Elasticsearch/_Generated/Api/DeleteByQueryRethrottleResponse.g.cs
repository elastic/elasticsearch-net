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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class DeleteByQueryRethrottleResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("node_failures")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? NodeFailures { get; init; }

	/// <summary>
	/// <para>
	/// Task information grouped by node, if <c>group_by</c> was set to <c>node</c> (the default).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("nodes")]
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>? Nodes { get; init; }
	[JsonInclude, JsonPropertyName("task_failures")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>? TaskFailures { get; init; }

	/// <summary>
	/// <para>
	/// Either a flat list of tasks if <c>group_by</c> was set to <c>none</c>, or grouped by parents if
	/// <c>group_by</c> was set to <c>parents</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tasks")]
	public Elastic.Clients.Elasticsearch.Tasks.TaskInfos? Tasks { get; init; }
}