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

public sealed partial class GetTokenResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("access_token")]
	public string AccessToken { get; init; }
	[JsonInclude, JsonPropertyName("authentication")]
	public Elastic.Clients.Elasticsearch.Security.AuthenticatedUser Authentication { get; init; }
	[JsonInclude, JsonPropertyName("expires_in")]
	public long ExpiresIn { get; init; }
	[JsonInclude, JsonPropertyName("kerberos_authentication_response_token")]
	public string? KerberosAuthenticationResponseToken { get; init; }
	[JsonInclude, JsonPropertyName("refresh_token")]
	public string? RefreshToken { get; init; }
	[JsonInclude, JsonPropertyName("scope")]
	public string? Scope { get; init; }
	[JsonInclude, JsonPropertyName("type")]
	public string Type { get; init; }
}