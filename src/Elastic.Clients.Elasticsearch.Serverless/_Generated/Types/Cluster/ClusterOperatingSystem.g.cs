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

namespace Elastic.Clients.Elasticsearch.Serverless.Cluster;

public sealed partial class ClusterOperatingSystem
{
	/// <summary>
	/// <para>
	/// Number of processors used to calculate thread pool size across all selected nodes.
	/// This number can be set with the processors setting of a node and defaults to the number of processors reported by the operating system.
	/// In both cases, this number will never be larger than 32.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("allocated_processors")]
	public int AllocatedProcessors { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about processor architectures (for example, x86_64 or aarch64) used by selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("architectures")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Cluster.ClusterOperatingSystemArchitecture>? Architectures { get; init; }

	/// <summary>
	/// <para>
	/// Number of processors available to JVM across all selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("available_processors")]
	public int AvailableProcessors { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about memory used by selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("mem")]
	public Elastic.Clients.Elasticsearch.Serverless.Cluster.OperatingSystemMemoryInfo Mem { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about operating systems used by selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("names")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Cluster.ClusterOperatingSystemName> Names { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about operating systems used by selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pretty_names")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Cluster.ClusterOperatingSystemPrettyName> PrettyNames { get; init; }
}