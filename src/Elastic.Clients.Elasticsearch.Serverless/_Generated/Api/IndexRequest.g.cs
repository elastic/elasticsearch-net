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
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless;

public sealed partial class IndexRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Only perform the operation if the document has this primary term.</para>
	/// </summary>
	public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

	/// <summary>
	/// <para>Only perform the operation if the document has this sequence number.</para>
	/// </summary>
	public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

	/// <summary>
	/// <para>Set to create to only index the document if it does not already exist (put if absent).<br/>If a document with the specified `_id` already exists, the indexing operation will fail.<br/>Same as using the `<index>/_create` endpoint.<br/>Valid values: `index`, `create`.<br/>If document id is specified, it defaults to `index`.<br/>Otherwise, it defaults to `create`.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.OpType? OpType { get => Q<Elastic.Clients.Elasticsearch.Serverless.OpType?>("op_type"); set => Q("op_type", value); }

	/// <summary>
	/// <para>ID of the pipeline to use to preprocess incoming documents.<br/>If the index has a default ingest pipeline specified, then setting the value to `_none` disables the default ingest pipeline for this request.<br/>If a final pipeline is configured it will always run, regardless of the value of this parameter.</para>
	/// </summary>
	public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

	/// <summary>
	/// <para>If `true`, Elasticsearch refreshes the affected shards to make this operation visible to search, if `wait_for` then wait for a refresh to make this operation visible to search, if `false` do nothing with refreshes.<br/>Valid values: `true`, `false`, `wait_for`.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Serverless.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>If `true`, the destination must be an index alias.</para>
	/// </summary>
	public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }

	/// <summary>
	/// <para>Custom value used to route operations to a specific shard.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Serverless.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>Period the request waits for the following operations: automatic index creation, dynamic mapping updates, waiting for active shards.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>Explicit version number for concurrency control.<br/>The specified version must match the current version of the document for the request to succeed.</para>
	/// </summary>
	public long? Version { get => Q<long?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>Specific version type: `external`, `external_gte`.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.Serverless.VersionType?>("version_type"); set => Q("version_type", value); }

	/// <summary>
	/// <para>The number of shard copies that must be active before proceeding with the operation.<br/>Set to all or any positive integer up to the total number of shards in the index (`number_of_replicas+1`).</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

/// <summary>
/// <para>Adds a JSON document to the specified data stream or index and makes it searchable.<br/>If the target is an index and the document already exists, the request updates the document and increments its version.</para>
/// </summary>
public sealed partial class IndexRequest<TDocument> : PlainRequest<IndexRequestParameters>
{
	public IndexRequest(Elastic.Clients.Elasticsearch.Serverless.IndexName index, Elastic.Clients.Elasticsearch.Serverless.Id? id) : base(r => r.Required("index", index).Optional("id", id))
	{
	}

	public IndexRequest(Elastic.Clients.Elasticsearch.Serverless.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceIndex;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "index";

	[JsonIgnore]
	public TDocument Document { get; set; }

	/// <summary>
	/// <para>Only perform the operation if the document has this primary term.</para>
	/// </summary>
	[JsonIgnore]
	public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

	/// <summary>
	/// <para>Only perform the operation if the document has this sequence number.</para>
	/// </summary>
	[JsonIgnore]
	public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

	/// <summary>
	/// <para>Set to create to only index the document if it does not already exist (put if absent).<br/>If a document with the specified `_id` already exists, the indexing operation will fail.<br/>Same as using the `<index>/_create` endpoint.<br/>Valid values: `index`, `create`.<br/>If document id is specified, it defaults to `index`.<br/>Otherwise, it defaults to `create`.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.OpType? OpType { get => Q<Elastic.Clients.Elasticsearch.Serverless.OpType?>("op_type"); set => Q("op_type", value); }

	/// <summary>
	/// <para>ID of the pipeline to use to preprocess incoming documents.<br/>If the index has a default ingest pipeline specified, then setting the value to `_none` disables the default ingest pipeline for this request.<br/>If a final pipeline is configured it will always run, regardless of the value of this parameter.</para>
	/// </summary>
	[JsonIgnore]
	public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

	/// <summary>
	/// <para>If `true`, Elasticsearch refreshes the affected shards to make this operation visible to search, if `wait_for` then wait for a refresh to make this operation visible to search, if `false` do nothing with refreshes.<br/>Valid values: `true`, `false`, `wait_for`.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Serverless.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>If `true`, the destination must be an index alias.</para>
	/// </summary>
	[JsonIgnore]
	public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }

	/// <summary>
	/// <para>Custom value used to route operations to a specific shard.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Serverless.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>Period the request waits for the following operations: automatic index creation, dynamic mapping updates, waiting for active shards.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>Explicit version number for concurrency control.<br/>The specified version must match the current version of the document for the request to succeed.</para>
	/// </summary>
	[JsonIgnore]
	public long? Version { get => Q<long?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>Specific version type: `external`, `external_gte`.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.Serverless.VersionType?>("version_type"); set => Q("version_type", value); }

	/// <summary>
	/// <para>The number of shard copies that must be active before proceeding with the operation.<br/>Set to all or any positive integer up to the total number of shards in the index (`number_of_replicas+1`).</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

/// <summary>
/// <para>Adds a JSON document to the specified data stream or index and makes it searchable.<br/>If the target is an index and the document already exists, the request updates the document and increments its version.</para>
/// </summary>
public sealed partial class IndexRequestDescriptor<TDocument> : RequestDescriptor<IndexRequestDescriptor<TDocument>, IndexRequestParameters>
{
	internal IndexRequestDescriptor(Action<IndexRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	internal IndexRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexName index, Elastic.Clients.Elasticsearch.Serverless.Id? id) : base(r => r.Required("index", index).Optional("id", id))
	{
	}

	public IndexRequestDescriptor(TDocument document) : this(typeof(TDocument), Serverless.Id.From(document)) => DocumentValue = document;
	public IndexRequestDescriptor(TDocument document, IndexName index, Id id) : this(index, id) => DocumentValue = document;
	public IndexRequestDescriptor(TDocument document, IndexName index) : this(index, Serverless.Id.From(document)) => DocumentValue = document;
	public IndexRequestDescriptor(TDocument document, Id id) : this(typeof(TDocument), id) => DocumentValue = document;

	public IndexRequestDescriptor(Id id) : this(typeof(TDocument), id)
	{
	}

	public IndexRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal IndexRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceIndex;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "index";

	public IndexRequestDescriptor<TDocument> IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
	public IndexRequestDescriptor<TDocument> IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
	public IndexRequestDescriptor<TDocument> OpType(Elastic.Clients.Elasticsearch.Serverless.OpType? opType) => Qs("op_type", opType);
	public IndexRequestDescriptor<TDocument> Pipeline(string? pipeline) => Qs("pipeline", pipeline);
	public IndexRequestDescriptor<TDocument> Refresh(Elastic.Clients.Elasticsearch.Serverless.Refresh? refresh) => Qs("refresh", refresh);
	public IndexRequestDescriptor<TDocument> RequireAlias(bool? requireAlias = true) => Qs("require_alias", requireAlias);
	public IndexRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Serverless.Routing? routing) => Qs("routing", routing);
	public IndexRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);
	public IndexRequestDescriptor<TDocument> Version(long? version) => Qs("version", version);
	public IndexRequestDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.Serverless.VersionType? versionType) => Qs("version_type", versionType);
	public IndexRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public IndexRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Serverless.Id? id)
	{
		RouteValues.Optional("id", id);
		return Self;
	}

	public IndexRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.Serverless.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private TDocument DocumentValue { get; set; }

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		SourceSerialization.Serialize(DocumentValue, writer, settings.SourceSerializer);
	}
}