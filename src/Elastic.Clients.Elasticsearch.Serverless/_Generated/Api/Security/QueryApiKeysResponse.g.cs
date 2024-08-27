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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Security;

public sealed partial class QueryApiKeysResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// The aggregations result, if requested.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("aggregations")]
	public Elastic.Clients.Elasticsearch.Serverless.Aggregations.AggregateDictionary? Aggregations { get; init; }

	/// <summary>
	/// <para>
	/// A list of API key information.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("api_keys")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Security.ApiKey> ApiKeys { get; init; }

	/// <summary>
	/// <para>
	/// The number of API keys returned in the response.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("count")]
	public int Count { get; init; }

	/// <summary>
	/// <para>
	/// The total number of API keys found.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total")]
	public int Total { get; init; }
}