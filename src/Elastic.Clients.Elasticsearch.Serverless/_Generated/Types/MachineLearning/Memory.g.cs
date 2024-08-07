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

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class Memory
{
	[JsonInclude, JsonPropertyName("attributes")]
	public IReadOnlyDictionary<string, string> Attributes { get; init; }
	[JsonInclude, JsonPropertyName("ephemeral_id")]
	public string EphemeralId { get; init; }

	/// <summary>
	/// <para>
	/// Contains Java Virtual Machine (JVM) statistics for the node.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("jvm")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.JvmStats Jvm { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about memory usage for the node.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("mem")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.MemStats Mem { get; init; }

	/// <summary>
	/// <para>
	/// Human-readable identifier for the node. Based on the Node name setting setting.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("name")]
	public string Name { get; init; }

	/// <summary>
	/// <para>
	/// Roles assigned to the node.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("roles")]
	public IReadOnlyCollection<string> Roles { get; init; }

	/// <summary>
	/// <para>
	/// The host and port where transport HTTP connections are accepted.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("transport_address")]
	public string TransportAddress { get; init; }
}