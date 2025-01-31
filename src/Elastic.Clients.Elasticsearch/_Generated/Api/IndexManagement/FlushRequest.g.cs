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

public sealed partial class FlushRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request forces a flush even if there are no changes to commit to the index.
	/// </para>
	/// </summary>
	public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the flush operation blocks until execution when another flush operation is running.
	/// If <c>false</c>, Elasticsearch returns an error if you request a flush when another flush operation is running.
	/// </para>
	/// </summary>
	public bool? WaitIfOngoing { get => Q<bool?>("wait_if_ongoing"); set => Q("wait_if_ongoing", value); }
}

/// <summary>
/// <para>
/// Flush data streams or indices.
/// Flushing a data stream or index is the process of making sure that any data that is currently only stored in the transaction log is also permanently stored in the Lucene index.
/// When restarting, Elasticsearch replays any unflushed operations from the transaction log into the Lucene index to bring it back into the state that it was in before the restart.
/// Elasticsearch automatically triggers flushes as needed, using heuristics that trade off the size of the unflushed transaction log against the cost of performing each flush.
/// </para>
/// <para>
/// After each operation has been flushed it is permanently stored in the Lucene index.
/// This may mean that there is no need to maintain an additional copy of it in the transaction log.
/// The transaction log is made up of multiple files, called generations, and Elasticsearch will delete any generation files when they are no longer needed, freeing up disk space.
/// </para>
/// <para>
/// It is also possible to trigger a flush on one or more indices using the flush API, although it is rare for users to need to call this API directly.
/// If you call the flush API after indexing some documents then a successful response indicates that Elasticsearch has flushed all the documents that were indexed before the flush API was called.
/// </para>
/// </summary>
public sealed partial class FlushRequest : PlainRequest<FlushRequestParameters>
{
	[JsonConstructor]
	public FlushRequest()
	{
	}

	public FlushRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementFlush;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.flush";

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases to flush.
	/// Supports wildcards (<c>*</c>).
	/// To flush all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request forces a flush even if there are no changes to commit to the index.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the flush operation blocks until execution when another flush operation is running.
	/// If <c>false</c>, Elasticsearch returns an error if you request a flush when another flush operation is running.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? WaitIfOngoing { get => Q<bool?>("wait_if_ongoing"); set => Q("wait_if_ongoing", value); }
}

/// <summary>
/// <para>
/// Flush data streams or indices.
/// Flushing a data stream or index is the process of making sure that any data that is currently only stored in the transaction log is also permanently stored in the Lucene index.
/// When restarting, Elasticsearch replays any unflushed operations from the transaction log into the Lucene index to bring it back into the state that it was in before the restart.
/// Elasticsearch automatically triggers flushes as needed, using heuristics that trade off the size of the unflushed transaction log against the cost of performing each flush.
/// </para>
/// <para>
/// After each operation has been flushed it is permanently stored in the Lucene index.
/// This may mean that there is no need to maintain an additional copy of it in the transaction log.
/// The transaction log is made up of multiple files, called generations, and Elasticsearch will delete any generation files when they are no longer needed, freeing up disk space.
/// </para>
/// <para>
/// It is also possible to trigger a flush on one or more indices using the flush API, although it is rare for users to need to call this API directly.
/// If you call the flush API after indexing some documents then a successful response indicates that Elasticsearch has flushed all the documents that were indexed before the flush API was called.
/// </para>
/// </summary>
public sealed partial class FlushRequestDescriptor<TDocument> : RequestDescriptor<FlushRequestDescriptor<TDocument>, FlushRequestParameters>
{
	internal FlushRequestDescriptor(Action<FlushRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public FlushRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public FlushRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementFlush;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.flush";

	public FlushRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public FlushRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public FlushRequestDescriptor<TDocument> Force(bool? force = true) => Qs("force", force);
	public FlushRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public FlushRequestDescriptor<TDocument> WaitIfOngoing(bool? waitIfOngoing = true) => Qs("wait_if_ongoing", waitIfOngoing);

	public FlushRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
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
/// Flush data streams or indices.
/// Flushing a data stream or index is the process of making sure that any data that is currently only stored in the transaction log is also permanently stored in the Lucene index.
/// When restarting, Elasticsearch replays any unflushed operations from the transaction log into the Lucene index to bring it back into the state that it was in before the restart.
/// Elasticsearch automatically triggers flushes as needed, using heuristics that trade off the size of the unflushed transaction log against the cost of performing each flush.
/// </para>
/// <para>
/// After each operation has been flushed it is permanently stored in the Lucene index.
/// This may mean that there is no need to maintain an additional copy of it in the transaction log.
/// The transaction log is made up of multiple files, called generations, and Elasticsearch will delete any generation files when they are no longer needed, freeing up disk space.
/// </para>
/// <para>
/// It is also possible to trigger a flush on one or more indices using the flush API, although it is rare for users to need to call this API directly.
/// If you call the flush API after indexing some documents then a successful response indicates that Elasticsearch has flushed all the documents that were indexed before the flush API was called.
/// </para>
/// </summary>
public sealed partial class FlushRequestDescriptor : RequestDescriptor<FlushRequestDescriptor, FlushRequestParameters>
{
	internal FlushRequestDescriptor(Action<FlushRequestDescriptor> configure) => configure.Invoke(this);

	public FlushRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public FlushRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementFlush;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.flush";

	public FlushRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public FlushRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public FlushRequestDescriptor Force(bool? force = true) => Qs("force", force);
	public FlushRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public FlushRequestDescriptor WaitIfOngoing(bool? waitIfOngoing = true) => Qs("wait_if_ongoing", waitIfOngoing);

	public FlushRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}