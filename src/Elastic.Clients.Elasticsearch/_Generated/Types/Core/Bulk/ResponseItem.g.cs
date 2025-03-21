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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public partial class ResponseItem
{
	/// <summary>
	/// <para>
	/// Additional information about the failed operation.
	/// The property is returned only for failed operations.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("error")]
	public Elastic.Clients.Elasticsearch.ErrorCause? Error { get; init; }
	[JsonInclude, JsonPropertyName("failure_store")]
	public Elastic.Clients.Elasticsearch.Core.Bulk.FailureStoreStatus? FailureStore { get; init; }
	[JsonInclude, JsonPropertyName("forced_refresh")]
	public bool? ForcedRefresh { get; init; }
	[JsonInclude, JsonPropertyName("get")]
	public Elastic.Clients.Elasticsearch.InlineGet<IReadOnlyDictionary<string, object>>? Get { get; init; }

	/// <summary>
	/// <para>
	/// The document ID associated with the operation.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_id")]
	public string? Id { get; init; }

	/// <summary>
	/// <para>
	/// The name of the index associated with the operation.
	/// If the operation targeted a data stream, this is the backing index into which the document was written.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_index")]
	public string Index { get; init; }

	/// <summary>
	/// <para>
	/// The primary term assigned to the document for the operation.
	/// This property is returned only for successful operations.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_primary_term")]
	public long? PrimaryTerm { get; init; }

	/// <summary>
	/// <para>
	/// The result of the operation.
	/// Successful values are <c>created</c>, <c>deleted</c>, and <c>updated</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("result")]
	public string? Result { get; init; }

	/// <summary>
	/// <para>
	/// The sequence number assigned to the document for the operation.
	/// Sequence numbers are used to ensure an older version of a document doesn't overwrite a newer version.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_seq_no")]
	public long? SeqNo { get; init; }

	/// <summary>
	/// <para>
	/// Shard information for the operation.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_shards")]
	public Elastic.Clients.Elasticsearch.ShardStatistics? Shards { get; init; }

	/// <summary>
	/// <para>
	/// The HTTP status code returned for the operation.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("status")]
	public int Status { get; init; }

	/// <summary>
	/// <para>
	/// The document version associated with the operation.
	/// The document version is incremented each time the document is updated.
	/// This property is returned only for successful actions.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_version")]
	public long? Version { get; init; }
}