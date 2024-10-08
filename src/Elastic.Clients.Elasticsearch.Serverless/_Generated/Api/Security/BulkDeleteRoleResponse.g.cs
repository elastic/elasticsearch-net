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

public sealed partial class BulkDeleteRoleResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// Array of deleted roles
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("deleted")]
	public IReadOnlyCollection<string>? Deleted { get; init; }

	/// <summary>
	/// <para>
	/// Present if any deletes resulted in errors
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("errors")]
	public Elastic.Clients.Elasticsearch.Serverless.Security.BulkError? Errors { get; init; }

	/// <summary>
	/// <para>
	/// Array of roles that could not be found
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("not_found")]
	public IReadOnlyCollection<string>? NotFound { get; init; }
}