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
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class RecoveryRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c>, the response only includes ongoing shard recoveries.
	/// </para>
	/// </summary>
	public bool? ActiveOnly { get => Q<bool?>("active_only"); set => Q("active_only", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about shard recoveries.
	/// </para>
	/// </summary>
	public bool? Detailed { get => Q<bool?>("detailed"); set => Q("detailed", value); }
}

/// <summary>
/// <para>
/// Get index recovery information.
/// Get information about ongoing and completed shard recoveries for one or more indices.
/// For data streams, the API returns information for the stream's backing indices.
/// </para>
/// <para>
/// All recoveries, whether ongoing or complete, are kept in the cluster state and may be reported on at any time.
/// </para>
/// <para>
/// Shard recovery is the process of initializing a shard copy, such as restoring a primary shard from a snapshot or creating a replica shard from a primary shard.
/// When a shard recovery completes, the recovered shard is available for search and indexing.
/// </para>
/// <para>
/// Recovery automatically occurs during the following processes:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// When creating an index for the first time.
/// </para>
/// </item>
/// <item>
/// <para>
/// When a node rejoins the cluster and starts up any missing primary shard copies using the data that it holds in its data path.
/// </para>
/// </item>
/// <item>
/// <para>
/// Creation of new replica shard copies from the primary.
/// </para>
/// </item>
/// <item>
/// <para>
/// Relocation of a shard copy to a different node in the same cluster.
/// </para>
/// </item>
/// <item>
/// <para>
/// A snapshot restore operation.
/// </para>
/// </item>
/// <item>
/// <para>
/// A clone, shrink, or split operation.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can determine the cause of a shard recovery using the recovery or cat recovery APIs.
/// </para>
/// <para>
/// The index recovery API reports information about completed recoveries only for shard copies that currently exist in the cluster.
/// It only reports the last recovery for each shard copy and does not report historical information about earlier recoveries, nor does it report information about the recoveries of shard copies that no longer exist.
/// This means that if a shard copy completes a recovery and then Elasticsearch relocates it onto a different node then the information about the original recovery will not be shown in the recovery API.
/// </para>
/// </summary>
public sealed partial class RecoveryRequest : PlainRequest<RecoveryRequestParameters>
{
	public RecoveryRequest()
	{
	}

	public RecoveryRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementRecovery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.recovery";

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response only includes ongoing shard recoveries.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? ActiveOnly { get => Q<bool?>("active_only"); set => Q("active_only", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about shard recoveries.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Detailed { get => Q<bool?>("detailed"); set => Q("detailed", value); }
}

/// <summary>
/// <para>
/// Get index recovery information.
/// Get information about ongoing and completed shard recoveries for one or more indices.
/// For data streams, the API returns information for the stream's backing indices.
/// </para>
/// <para>
/// All recoveries, whether ongoing or complete, are kept in the cluster state and may be reported on at any time.
/// </para>
/// <para>
/// Shard recovery is the process of initializing a shard copy, such as restoring a primary shard from a snapshot or creating a replica shard from a primary shard.
/// When a shard recovery completes, the recovered shard is available for search and indexing.
/// </para>
/// <para>
/// Recovery automatically occurs during the following processes:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// When creating an index for the first time.
/// </para>
/// </item>
/// <item>
/// <para>
/// When a node rejoins the cluster and starts up any missing primary shard copies using the data that it holds in its data path.
/// </para>
/// </item>
/// <item>
/// <para>
/// Creation of new replica shard copies from the primary.
/// </para>
/// </item>
/// <item>
/// <para>
/// Relocation of a shard copy to a different node in the same cluster.
/// </para>
/// </item>
/// <item>
/// <para>
/// A snapshot restore operation.
/// </para>
/// </item>
/// <item>
/// <para>
/// A clone, shrink, or split operation.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can determine the cause of a shard recovery using the recovery or cat recovery APIs.
/// </para>
/// <para>
/// The index recovery API reports information about completed recoveries only for shard copies that currently exist in the cluster.
/// It only reports the last recovery for each shard copy and does not report historical information about earlier recoveries, nor does it report information about the recoveries of shard copies that no longer exist.
/// This means that if a shard copy completes a recovery and then Elasticsearch relocates it onto a different node then the information about the original recovery will not be shown in the recovery API.
/// </para>
/// </summary>
public sealed partial class RecoveryRequestDescriptor<TDocument> : RequestDescriptor<RecoveryRequestDescriptor<TDocument>, RecoveryRequestParameters>
{
	internal RecoveryRequestDescriptor(Action<RecoveryRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RecoveryRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public RecoveryRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementRecovery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.recovery";

	public RecoveryRequestDescriptor<TDocument> ActiveOnly(bool? activeOnly = true) => Qs("active_only", activeOnly);
	public RecoveryRequestDescriptor<TDocument> Detailed(bool? detailed = true) => Qs("detailed", detailed);

	public RecoveryRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Get index recovery information.
/// Get information about ongoing and completed shard recoveries for one or more indices.
/// For data streams, the API returns information for the stream's backing indices.
/// </para>
/// <para>
/// All recoveries, whether ongoing or complete, are kept in the cluster state and may be reported on at any time.
/// </para>
/// <para>
/// Shard recovery is the process of initializing a shard copy, such as restoring a primary shard from a snapshot or creating a replica shard from a primary shard.
/// When a shard recovery completes, the recovered shard is available for search and indexing.
/// </para>
/// <para>
/// Recovery automatically occurs during the following processes:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// When creating an index for the first time.
/// </para>
/// </item>
/// <item>
/// <para>
/// When a node rejoins the cluster and starts up any missing primary shard copies using the data that it holds in its data path.
/// </para>
/// </item>
/// <item>
/// <para>
/// Creation of new replica shard copies from the primary.
/// </para>
/// </item>
/// <item>
/// <para>
/// Relocation of a shard copy to a different node in the same cluster.
/// </para>
/// </item>
/// <item>
/// <para>
/// A snapshot restore operation.
/// </para>
/// </item>
/// <item>
/// <para>
/// A clone, shrink, or split operation.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can determine the cause of a shard recovery using the recovery or cat recovery APIs.
/// </para>
/// <para>
/// The index recovery API reports information about completed recoveries only for shard copies that currently exist in the cluster.
/// It only reports the last recovery for each shard copy and does not report historical information about earlier recoveries, nor does it report information about the recoveries of shard copies that no longer exist.
/// This means that if a shard copy completes a recovery and then Elasticsearch relocates it onto a different node then the information about the original recovery will not be shown in the recovery API.
/// </para>
/// </summary>
public sealed partial class RecoveryRequestDescriptor : RequestDescriptor<RecoveryRequestDescriptor, RecoveryRequestParameters>
{
	internal RecoveryRequestDescriptor(Action<RecoveryRequestDescriptor> configure) => configure.Invoke(this);

	public RecoveryRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public RecoveryRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementRecovery;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.recovery";

	public RecoveryRequestDescriptor ActiveOnly(bool? activeOnly = true) => Qs("active_only", activeOnly);
	public RecoveryRequestDescriptor Detailed(bool? detailed = true) => Qs("detailed", detailed);

	public RecoveryRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}