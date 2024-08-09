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

public sealed partial class MemStats
{
	/// <summary>
	/// <para>
	/// If the amount of physical memory has been overridden using the es.total_memory_bytes system property
	/// then this reports the overridden value. Otherwise it reports the same value as total.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("adjusted_total")]
	public Elastic.Clients.Elasticsearch.Serverless.ByteSize? AdjustedTotal { get; init; }

	/// <summary>
	/// <para>
	/// If the amount of physical memory has been overridden using the <c>es.total_memory_bytes</c> system property
	/// then this reports the overridden value in bytes. Otherwise it reports the same value as <c>total_in_bytes</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("adjusted_total_in_bytes")]
	public int AdjustedTotalInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Contains statistics about machine learning use of native memory on the node.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ml")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.MemMlStats Ml { get; init; }

	/// <summary>
	/// <para>
	/// Total amount of physical memory.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total")]
	public Elastic.Clients.Elasticsearch.Serverless.ByteSize? Total { get; init; }

	/// <summary>
	/// <para>
	/// Total amount of physical memory in bytes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_in_bytes")]
	public int TotalInBytes { get; init; }
}