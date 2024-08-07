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

namespace Elastic.Clients.Elasticsearch.Nodes;

public sealed partial class JvmMemoryStats
{
	/// <summary>
	/// <para>
	/// Amount of memory, in bytes, available for use by the heap.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("heap_committed_in_bytes")]
	public long? HeapCommittedInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Maximum amount of memory, in bytes, available for use by the heap.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("heap_max_in_bytes")]
	public long? HeapMaxInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Memory, in bytes, currently in use by the heap.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("heap_used_in_bytes")]
	public long? HeapUsedInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Percentage of memory currently in use by the heap.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("heap_used_percent")]
	public long? HeapUsedPercent { get; init; }

	/// <summary>
	/// <para>
	/// Amount of non-heap memory available, in bytes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("non_heap_committed_in_bytes")]
	public long? NonHeapCommittedInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Non-heap memory used, in bytes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("non_heap_used_in_bytes")]
	public long? NonHeapUsedInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about heap memory usage for the node.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pools")]
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.Pool>? Pools { get; init; }
}