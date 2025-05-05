// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

/// <summary>
/// Provides the base class from which the classes that represent bulk operations are derived.
/// </summary>
/// <remarks>
/// This is an abstract class.
/// </remarks>
public abstract class BulkOperation : IBulkOperation, IStreamSerializable
{
	internal BulkOperation() { }

	/// <summary>
	/// The document ID.
	/// <para>Required for update and delete operations.</para>
	/// <para>Optional for create and index operations. If no ID is specified, a document ID is automatically generated.</para>
	/// </summary>
	[JsonPropertyName("_id")]
	public Id? Id { get; set; }

	/// <summary>
	/// May be set as part of optimistic concurrency control to specify the operation applies only if the primary term matches.
	/// </summary>
	[JsonPropertyName("if_primary_term")]
	public long? IfPrimaryTerm { get; set; }

	/// <summary>
	///  May be set as part of optimistic concurrency control to specify the operation applies only if the sequence number matches.
	/// </summary>
	[JsonPropertyName("if_seq_no")]
	public long? IfSequenceNumber { get; set; }

	/// <summary>
	/// Name of the data stream, index, or index alias to perform the action on.
	/// This parameter is required if a target index is not specified in the request path.
	/// </summary>
	[JsonPropertyName("_index")]
	public IndexName? Index { get; set; }

	/// <summary>
	/// If <c>true</c>, the operation action must target an index alias. Defaults to <c>false</c>.
	/// </summary>
	[JsonPropertyName("require_alias")]
	public bool? RequireAlias { get; set; }

	/// <summary>
	/// May be set to specify a custom routing value for the document.
	/// </summary>
	/// <remarks>It automatically follows the behavior of the index/delete operation based on the _routing mapping</remarks>
	[JsonPropertyName("routing")]
	public Routing? Routing { get; set; }

	/// <summary>
	/// Each bulk operation can include the version value for the document.
	/// </summary>
	/// <remarks>This automatically follows the behavior of the index/delete operation based on the _version mapping.</remarks>
	[JsonPropertyName("version")]
	public long? Version { get; set; }

	/// <summary>
	/// The version type for the document in the current operation.
	/// </summary>
	/// <remarks>This automatically follows the behavior of the index/delete operation based on the _version mapping.</remarks>
	[JsonPropertyName("version_type")]
	public VersionType? VersionType { get; set; }

	/// <summary>
	/// The name for the bulk operation action.
	/// </summary>
	protected abstract string Operation { get; }

	protected abstract Type ClrType { get; }

	/// <summary>
	/// Derived operations should override this control how the operation and its payload will be serialised into the HTTP request content <see cref="Stream"/>.
	/// This supports newline delimited JSON data.
	/// </summary>
	/// <param name="stream">The writable stream for the HTTP request.</param>
	/// <param name="settings">The <see cref="IElasticsearchClientSettings"/> for the current client instance.</param>
	protected abstract void Serialize(Stream stream, IElasticsearchClientSettings settings);

	/// <summary>
	/// Derived operations should override this control how the operation and its payload will be asynchronously serialised into the HTTP request content <see cref="Stream"/>.
	/// This supports newline delimited JSON data.
	/// </summary>
	/// <param name="stream">The writable stream for the HTTP request.</param>
	/// <param name="settings">The <see cref="IElasticsearchClientSettings"/> for the current client instance.</param>
	protected abstract Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings);

	void IBulkOperation.PrepareIndex(IndexName bulkRequestIndex)
	{
		Index ??= bulkRequestIndex ?? ClrType;

		if (bulkRequestIndex is not null && Index.Equals(bulkRequestIndex))
			Index = null;
	}

	/// <inheritdoc />
	void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => Serialize(stream, settings);

	/// <inheritdoc />
	Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => SerializeAsync(stream, settings);
}
