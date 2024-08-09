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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class IndexRequestParameters : RequestParameters
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
	/// Set to create to only index the document if it does not already exist (put if absent).
	/// If a document with the specified <c>_id</c> already exists, the indexing operation will fail.
	/// Same as using the <c>&lt;index>/_create</c> endpoint.
	/// Valid values: <c>index</c>, <c>create</c>.
	/// If document id is specified, it defaults to <c>index</c>.
	/// Otherwise, it defaults to <c>create</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpType? OpType { get => Q<Elastic.Clients.Elasticsearch.OpType?>("op_type"); set => Q("op_type", value); }

	/// <summary>
	/// <para>
	/// ID of the pipeline to use to preprocess incoming documents.
	/// If the index has a default ingest pipeline specified, then setting the value to <c>_none</c> disables the default ingest pipeline for this request.
	/// If a final pipeline is configured it will always run, regardless of the value of this parameter.
	/// </para>
	/// </summary>
	public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, Elasticsearch refreshes the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> do nothing with refreshes.
	/// Valid values: <c>true</c>, <c>false</c>, <c>wait_for</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the destination must be an index alias.
	/// </para>
	/// </summary>
	public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// Period the request waits for the following operations: automatic index creation, dynamic mapping updates, waiting for active shards.
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
	/// Set to all or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

/// <summary>
/// <para>
/// Index a document.
/// Adds a JSON document to the specified data stream or index and makes it searchable.
/// If the target is an index and the document already exists, the request updates the document and increments its version.
/// </para>
/// </summary>
public sealed partial class IndexRequest<TDocument> : PlainRequest<IndexRequestParameters>, ISelfSerializable
{
	public IndexRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Required("index", index).Optional("id", id))
	{
	}

	public IndexRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceIndex;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "index";

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
	/// Set to create to only index the document if it does not already exist (put if absent).
	/// If a document with the specified <c>_id</c> already exists, the indexing operation will fail.
	/// Same as using the <c>&lt;index>/_create</c> endpoint.
	/// Valid values: <c>index</c>, <c>create</c>.
	/// If document id is specified, it defaults to <c>index</c>.
	/// Otherwise, it defaults to <c>create</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.OpType? OpType { get => Q<Elastic.Clients.Elasticsearch.OpType?>("op_type"); set => Q("op_type", value); }

	/// <summary>
	/// <para>
	/// ID of the pipeline to use to preprocess incoming documents.
	/// If the index has a default ingest pipeline specified, then setting the value to <c>_none</c> disables the default ingest pipeline for this request.
	/// If a final pipeline is configured it will always run, regardless of the value of this parameter.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

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
	/// If <c>true</c>, the destination must be an index alias.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// Period the request waits for the following operations: automatic index creation, dynamic mapping updates, waiting for active shards.
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
	/// Set to all or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
	[JsonIgnore]
	public TDocument Document { get; set; }

	void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		SourceSerialization.Serialize(Document, writer, settings.SourceSerializer);
	}
}

/// <summary>
/// <para>
/// Index a document.
/// Adds a JSON document to the specified data stream or index and makes it searchable.
/// If the target is an index and the document already exists, the request updates the document and increments its version.
/// </para>
/// </summary>
public sealed partial class IndexRequestDescriptor<TDocument> : RequestDescriptor<IndexRequestDescriptor<TDocument>, IndexRequestParameters>
{
	internal IndexRequestDescriptor(Action<IndexRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
	public IndexRequestDescriptor(TDocument document, Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Required("index", index).Optional("id", id)) => DocumentValue = document;

	public IndexRequestDescriptor(TDocument document) : this(document, typeof(TDocument), Elastic.Clients.Elasticsearch.Id.From(document))
	{
	}

	public IndexRequestDescriptor(TDocument document, Elastic.Clients.Elasticsearch.IndexName index) : this(document, index, Elastic.Clients.Elasticsearch.Id.From(document))
	{
	}

	public IndexRequestDescriptor(TDocument document, Elastic.Clients.Elasticsearch.Id? id) : this(document, typeof(TDocument), id)
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceIndex;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "index";

	public IndexRequestDescriptor<TDocument> IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
	public IndexRequestDescriptor<TDocument> IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
	public IndexRequestDescriptor<TDocument> OpType(Elastic.Clients.Elasticsearch.OpType? opType) => Qs("op_type", opType);
	public IndexRequestDescriptor<TDocument> Pipeline(string? pipeline) => Qs("pipeline", pipeline);
	public IndexRequestDescriptor<TDocument> Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);
	public IndexRequestDescriptor<TDocument> RequireAlias(bool? requireAlias = true) => Qs("require_alias", requireAlias);
	public IndexRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
	public IndexRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public IndexRequestDescriptor<TDocument> Version(long? version) => Qs("version", version);
	public IndexRequestDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? versionType) => Qs("version_type", versionType);
	public IndexRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public IndexRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id? id)
	{
		RouteValues.Optional("id", id);
		return Self;
	}

	public IndexRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private TDocument DocumentValue { get; set; }

	public IndexRequestDescriptor<TDocument> Document(TDocument document)
	{
		DocumentValue = document;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		SourceSerialization.Serialize(DocumentValue, writer, settings.SourceSerializer);
	}
}