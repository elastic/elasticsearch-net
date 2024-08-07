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

public sealed partial class Transport
{
	/// <summary>
	/// <para>
	/// The distribution of the time spent handling each inbound message on a transport thread, represented as a histogram.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("inbound_handling_time_histogram")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Nodes.TransportHistogram>? InboundHandlingTimeHistogram { get; init; }

	/// <summary>
	/// <para>
	/// The distribution of the time spent sending each outbound transport message on a transport thread, represented as a histogram.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("outbound_handling_time_histogram")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Nodes.TransportHistogram>? OutboundHandlingTimeHistogram { get; init; }

	/// <summary>
	/// <para>
	/// Total number of RX (receive) packets received by the node during internal cluster communication.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("rx_count")]
	public long? RxCount { get; init; }

	/// <summary>
	/// <para>
	/// Size of RX packets received by the node during internal cluster communication.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("rx_size")]
	public string? RxSize { get; init; }

	/// <summary>
	/// <para>
	/// Size, in bytes, of RX packets received by the node during internal cluster communication.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("rx_size_in_bytes")]
	public long? RxSizeInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Current number of inbound TCP connections used for internal communication between nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("server_open")]
	public int? ServerOpen { get; init; }

	/// <summary>
	/// <para>
	/// The cumulative number of outbound transport connections that this node has opened since it started.
	/// Each transport connection may comprise multiple TCP connections but is only counted once in this statistic.
	/// Transport connections are typically long-lived so this statistic should remain constant in a stable cluster.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_outbound_connections")]
	public long? TotalOutboundConnections { get; init; }

	/// <summary>
	/// <para>
	/// Total number of TX (transmit) packets sent by the node during internal cluster communication.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tx_count")]
	public long? TxCount { get; init; }

	/// <summary>
	/// <para>
	/// Size of TX packets sent by the node during internal cluster communication.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tx_size")]
	public string? TxSize { get; init; }

	/// <summary>
	/// <para>
	/// Size, in bytes, of TX packets sent by the node during internal cluster communication.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tx_size_in_bytes")]
	public long? TxSizeInBytes { get; init; }
}