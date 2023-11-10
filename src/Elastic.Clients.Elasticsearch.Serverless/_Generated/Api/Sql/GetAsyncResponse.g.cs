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
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Sql;

public sealed partial class GetAsyncResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>Column headings for the search results. Each object is a column.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("columns")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Sql.Column>? Columns { get; init; }

	/// <summary>
	/// <para>Cursor for the next set of paginated results. For CSV, TSV, and<br/>TXT responses, this value is returned in the `Cursor` HTTP header.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cursor")]
	public string? Cursor { get; init; }

	/// <summary>
	/// <para>Identifier for the search. This value is only returned for async and saved<br/>synchronous searches. For CSV, TSV, and TXT responses, this value is returned<br/>in the `Async-ID` HTTP header.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("id")]
	public string Id { get; init; }

	/// <summary>
	/// <para>If `true`, the response does not contain complete search results. If `is_partial`<br/>is `true` and `is_running` is `true`, the search is still running. If `is_partial`<br/>is `true` but `is_running` is `false`, the results are partial due to a failure or<br/>timeout. This value is only returned for async and saved synchronous searches.<br/>For CSV, TSV, and TXT responses, this value is returned in the `Async-partial` HTTP header.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("is_partial")]
	public bool IsPartial { get; init; }

	/// <summary>
	/// <para>If `true`, the search is still running. If false, the search has finished.<br/>This value is only returned for async and saved synchronous searches. For<br/>CSV, TSV, and TXT responses, this value is returned in the `Async-partial`<br/>HTTP header.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("is_running")]
	public bool IsRunning { get; init; }
}