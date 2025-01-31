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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class DeleteRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Only perform the operation if the document has this primary term.
	/// </para>
	/// </summary>
	public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

	/// <summary>
	/// <para>
	/// Only perform the operation if the document has this sequence number.
	/// </para>
	/// </summary>
	public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, Elasticsearch refreshes the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> do nothing with refreshes.
	/// Valid values: <c>true</c>, <c>false</c>, <c>wait_for</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// Period to wait for active shards.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Explicit version number for concurrency control.
	/// The specified version must match the current version of the document for the request to succeed.
	/// </para>
	/// </summary>
	public long? Version { get => Q<long?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>
	/// Specific version type: <c>external</c>, <c>external_gte</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.VersionType?>("version_type"); set => Q("version_type", value); }

	/// <summary>
	/// <para>
	/// The number of shard copies that must be active before proceeding with the operation.
	/// Set to <c>all</c> or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

/// <summary>
/// <para>
/// Delete a document.
/// Removes a JSON document from the specified index.
/// </para>
/// </summary>
public partial class DeleteRequest : PlainRequest<DeleteRequestParameters>
{
	public DeleteRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}

	[JsonConstructor]
	internal DeleteRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceDelete;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "delete";

	/// <summary>
	/// <para>
	/// Unique identifier for the document.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// Name of the target index.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.IndexName Index { get => P<Elastic.Clients.Elasticsearch.IndexName>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// Only perform the operation if the document has this primary term.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

	/// <summary>
	/// <para>
	/// Only perform the operation if the document has this sequence number.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, Elasticsearch refreshes the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> do nothing with refreshes.
	/// Valid values: <c>true</c>, <c>false</c>, <c>wait_for</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// Period to wait for active shards.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Explicit version number for concurrency control.
	/// The specified version must match the current version of the document for the request to succeed.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public long? Version { get => Q<long?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>
	/// Specific version type: <c>external</c>, <c>external_gte</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.VersionType?>("version_type"); set => Q("version_type", value); }

	/// <summary>
	/// <para>
	/// The number of shard copies that must be active before proceeding with the operation.
	/// Set to <c>all</c> or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

/// <summary>
/// <para>
/// Delete a document.
/// Removes a JSON document from the specified index.
/// </para>
/// </summary>
public sealed partial class DeleteRequestDescriptor<TDocument> : RequestDescriptor<DeleteRequestDescriptor<TDocument>, DeleteRequestParameters>
{
	internal DeleteRequestDescriptor(Action<DeleteRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DeleteRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}

	public DeleteRequestDescriptor(TDocument document) : this(typeof(TDocument), Elastic.Clients.Elasticsearch.Id.From(document))
	{
	}

	public DeleteRequestDescriptor(TDocument document, Elastic.Clients.Elasticsearch.IndexName index) : this(index, Elastic.Clients.Elasticsearch.Id.From(document))
	{
	}

	public DeleteRequestDescriptor(TDocument document, Elastic.Clients.Elasticsearch.Id id) : this(typeof(TDocument), id)
	{
	}

	public DeleteRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : this(typeof(TDocument), id)
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceDelete;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "delete";

	public DeleteRequestDescriptor<TDocument> IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
	public DeleteRequestDescriptor<TDocument> IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
	public DeleteRequestDescriptor<TDocument> Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);
	public DeleteRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
	public DeleteRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public DeleteRequestDescriptor<TDocument> Version(long? version) => Qs("version", version);
	public DeleteRequestDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? versionType) => Qs("version_type", versionType);
	public DeleteRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public DeleteRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	public DeleteRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Delete a document.
/// Removes a JSON document from the specified index.
/// </para>
/// </summary>
public sealed partial class DeleteRequestDescriptor : RequestDescriptor<DeleteRequestDescriptor, DeleteRequestParameters>
{
	internal DeleteRequestDescriptor(Action<DeleteRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceDelete;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "delete";

	public DeleteRequestDescriptor IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
	public DeleteRequestDescriptor IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
	public DeleteRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);
	public DeleteRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
	public DeleteRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public DeleteRequestDescriptor Version(long? version) => Qs("version", version);
	public DeleteRequestDescriptor VersionType(Elastic.Clients.Elasticsearch.VersionType? versionType) => Qs("version_type", versionType);
	public DeleteRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public DeleteRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	public DeleteRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}