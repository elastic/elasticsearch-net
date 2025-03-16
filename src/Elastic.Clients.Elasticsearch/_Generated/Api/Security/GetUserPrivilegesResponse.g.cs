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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class GetUserPrivilegesResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("applications")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges> Applications { get; init; }
	[JsonInclude, JsonPropertyName("cluster")]
	public IReadOnlyCollection<string> Cluster { get; init; }
	[JsonInclude, JsonPropertyName("global")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.GlobalPrivilege> Global { get; init; }
	[JsonInclude, JsonPropertyName("indices")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.UserIndicesPrivileges> Indices { get; init; }
	[JsonInclude, JsonPropertyName("remote_cluster")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivileges>? RemoteCluster { get; init; }
	[JsonInclude, JsonPropertyName("remote_indices")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges>? RemoteIndices { get; init; }
	[JsonInclude, JsonPropertyName("run_as")]
	public IReadOnlyCollection<string> RunAs { get; init; }
}