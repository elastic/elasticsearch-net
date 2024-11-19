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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Security;

public sealed partial class Role
{
	[JsonInclude, JsonPropertyName("applications")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Security.ApplicationPrivileges> Applications { get; init; }
	[JsonInclude, JsonPropertyName("cluster")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Security.ClusterPrivilege> Cluster { get; init; }
	[JsonInclude, JsonPropertyName("global")]
	public IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyCollection<string>>>>? Global { get; init; }
	[JsonInclude, JsonPropertyName("indices")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Security.IndicesPrivileges> Indices { get; init; }
	[JsonInclude, JsonPropertyName("metadata")]
	public IReadOnlyDictionary<string, object> Metadata { get; init; }
	[JsonInclude, JsonPropertyName("role_templates")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Security.RoleTemplate>? RoleTemplates { get; init; }
	[JsonInclude, JsonPropertyName("run_as")]
	public IReadOnlyCollection<string> RunAs { get; init; }
	[JsonInclude, JsonPropertyName("transient_metadata")]
	public IReadOnlyDictionary<string, object>? TransientMetadata { get; init; }
}