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

public sealed partial class DatafeedAuthorization
{
	/// <summary>
	/// <para>
	/// If an API key was used for the most recent update to the datafeed, its name and identifier are listed in the response.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("api_key")]
	public Elastic.Clients.Elasticsearch.MachineLearning.ApiKeyAuthorization? ApiKey { get; init; }

	/// <summary>
	/// <para>
	/// If a user ID was used for the most recent update to the datafeed, its roles at the time of the update are listed in the response.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("roles")]
	public IReadOnlyCollection<string>? Roles { get; init; }

	/// <summary>
	/// <para>
	/// If a service account was used for the most recent update to the datafeed, the account name is listed in the response.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("service_account")]
	public string? ServiceAccount { get; init; }
}