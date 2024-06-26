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

public sealed partial class CgroupMemory
{
	/// <summary>
	/// <para>The `memory` control group to which the Elasticsearch process belongs.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("control_group")]
	public string? ControlGroup { get; init; }

	/// <summary>
	/// <para>The maximum amount of user memory (including file cache) allowed for all tasks in the same cgroup as the Elasticsearch process.<br/>This value can be too big to store in a `long`, so is returned as a string so that the value returned can exactly match what the underlying operating system interface returns.<br/>Any value that is too large to parse into a `long` almost certainly means no limit has been set for the cgroup.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("limit_in_bytes")]
	public string? LimitInBytes { get; init; }

	/// <summary>
	/// <para>The total current memory usage by processes in the cgroup, in bytes, by all tasks in the same cgroup as the Elasticsearch process.<br/>This value is stored as a string for consistency with `limit_in_bytes`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("usage_in_bytes")]
	public string? UsageInBytes { get; init; }
}