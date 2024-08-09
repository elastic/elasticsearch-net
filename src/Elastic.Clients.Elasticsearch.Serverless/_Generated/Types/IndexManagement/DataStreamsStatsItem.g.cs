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

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class DataStreamsStatsItem
{
	/// <summary>
	/// <para>
	/// Current number of backing indices for the data stream.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("backing_indices")]
	public int BackingIndices { get; init; }

	/// <summary>
	/// <para>
	/// Name of the data stream.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("data_stream")]
	public string DataStream { get; init; }

	/// <summary>
	/// <para>
	/// The data stream’s highest <c>@timestamp</c> value, converted to milliseconds since the Unix epoch.
	/// NOTE: This timestamp is provided as a best effort.
	/// The data stream may contain <c>@timestamp</c> values higher than this if one or more of the following conditions are met:
	/// The stream contains closed backing indices;
	/// Backing indices with a lower generation contain higher <c>@timestamp</c> values.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("maximum_timestamp")]
	public long MaximumTimestamp { get; init; }

	/// <summary>
	/// <para>
	/// Total size of all shards for the data stream’s backing indices.
	/// This parameter is only returned if the <c>human</c> query parameter is <c>true</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("store_size")]
	public Elastic.Clients.Elasticsearch.Serverless.ByteSize? StoreSize { get; init; }

	/// <summary>
	/// <para>
	/// Total size, in bytes, of all shards for the data stream’s backing indices.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("store_size_bytes")]
	public long StoreSizeBytes { get; init; }
}