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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class Authentication
{
	[JsonInclude, JsonPropertyName("api_key")]
	public IReadOnlyDictionary<string, string>? ApiKey { get; init; }
	[JsonInclude, JsonPropertyName("authentication_realm")]
	public Elastic.Clients.Elasticsearch.Security.AuthenticationRealm AuthenticationRealm { get; init; }
	[JsonInclude, JsonPropertyName("authentication_type")]
	public string AuthenticationType { get; init; }
	[JsonInclude, JsonPropertyName("email")]
	public string? Email { get; init; }
	[JsonInclude, JsonPropertyName("enabled")]
	public bool Enabled { get; init; }
	[JsonInclude, JsonPropertyName("full_name")]
	public string? FullName { get; init; }
	[JsonInclude, JsonPropertyName("lookup_realm")]
	public Elastic.Clients.Elasticsearch.Security.AuthenticationRealm LookupRealm { get; init; }
	[JsonInclude, JsonPropertyName("metadata")]
	public IReadOnlyDictionary<string, object> Metadata { get; init; }
	[JsonInclude, JsonPropertyName("roles")]
	public IReadOnlyCollection<string> Roles { get; init; }
	[JsonInclude, JsonPropertyName("token")]
	public IReadOnlyDictionary<string, string>? Token { get; init; }
	[JsonInclude, JsonPropertyName("username")]
	public string Username { get; init; }
}