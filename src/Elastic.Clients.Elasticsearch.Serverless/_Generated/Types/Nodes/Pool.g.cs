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

public sealed partial class Pool
{
	/// <summary>
	/// <para>
	/// Maximum amount of memory, in bytes, available for use by the heap.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_in_bytes")]
	public long? MaxInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Largest amount of memory, in bytes, historically used by the heap.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("peak_max_in_bytes")]
	public long? PeakMaxInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Largest amount of memory, in bytes, historically used by the heap.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("peak_used_in_bytes")]
	public long? PeakUsedInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Memory, in bytes, used by the heap.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("used_in_bytes")]
	public long? UsedInBytes { get; init; }
}