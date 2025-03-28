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

namespace Elastic.Clients.Elasticsearch.Snapshot;

public sealed partial class CloneSnapshotRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Explicit operation timeout for connection to master node
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Clone a snapshot.
/// Clone part of all of a snapshot into another snapshot in the same repository.
/// </para>
/// </summary>
public sealed partial class CloneSnapshotRequest : PlainRequest<CloneSnapshotRequestParameters>
{
	public CloneSnapshotRequest(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot, Elastic.Clients.Elasticsearch.Name targetSnapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot).Required("target_snapshot", targetSnapshot))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotClone;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "snapshot.clone";

	/// <summary>
	/// <para>
	/// Explicit operation timeout for connection to master node
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
	[JsonInclude, JsonPropertyName("indices")]
	public string Indices { get; set; }
}

/// <summary>
/// <para>
/// Clone a snapshot.
/// Clone part of all of a snapshot into another snapshot in the same repository.
/// </para>
/// </summary>
public sealed partial class CloneSnapshotRequestDescriptor : RequestDescriptor<CloneSnapshotRequestDescriptor, CloneSnapshotRequestParameters>
{
	internal CloneSnapshotRequestDescriptor(Action<CloneSnapshotRequestDescriptor> configure) => configure.Invoke(this);

	public CloneSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot, Elastic.Clients.Elasticsearch.Name targetSnapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot).Required("target_snapshot", targetSnapshot))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotClone;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "snapshot.clone";

	public CloneSnapshotRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public CloneSnapshotRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public CloneSnapshotRequestDescriptor Repository(Elastic.Clients.Elasticsearch.Name repository)
	{
		RouteValues.Required("repository", repository);
		return Self;
	}

	public CloneSnapshotRequestDescriptor Snapshot(Elastic.Clients.Elasticsearch.Name snapshot)
	{
		RouteValues.Required("snapshot", snapshot);
		return Self;
	}

	public CloneSnapshotRequestDescriptor TargetSnapshot(Elastic.Clients.Elasticsearch.Name targetSnapshot)
	{
		RouteValues.Required("target_snapshot", targetSnapshot);
		return Self;
	}

	private string IndicesValue { get; set; }

	public CloneSnapshotRequestDescriptor Indices(string indices)
	{
		IndicesValue = indices;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("indices");
		writer.WriteStringValue(IndicesValue);
		writer.WriteEndObject();
	}
}