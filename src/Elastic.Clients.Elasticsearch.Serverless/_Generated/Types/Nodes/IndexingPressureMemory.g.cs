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

namespace Elastic.Clients.Elasticsearch.Serverless.Nodes;

public sealed partial class IndexingPressureMemory
{
	/// <summary>
	/// <para>
	/// Contains statistics for current indexing load.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("current")]
	public Elastic.Clients.Elasticsearch.Serverless.Nodes.PressureMemory? Current { get; init; }

	/// <summary>
	/// <para>
	/// Configured memory limit for the indexing requests.
	/// Replica requests have an automatic limit that is 1.5x this value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("limit")]
	public Elastic.Clients.Elasticsearch.Serverless.ByteSize? Limit { get; init; }

	/// <summary>
	/// <para>
	/// Configured memory limit, in bytes, for the indexing requests.
	/// Replica requests have an automatic limit that is 1.5x this value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("limit_in_bytes")]
	public long? LimitInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics for the cumulative indexing load since the node started.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total")]
	public Elastic.Clients.Elasticsearch.Serverless.Nodes.PressureMemory? Total { get; init; }
}