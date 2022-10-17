// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public abstract class BulkOperationDescriptor<TDescriptor> : SerializableDescriptor<TDescriptor>, IBulkOperation, IStreamSerializable where TDescriptor : BulkOperationDescriptor<TDescriptor>
{
	private long? _version;
	private VersionType? _versionType;
	private bool? _requireAlias;

	internal BulkOperationDescriptor() { }

	protected IndexName IndexNameValue { get; set; }
	protected Id IdValue { get; set; }
	protected Routing RoutingValue { get; set; }
	protected long? IfSequenceNoValue { get; set; }
	protected long? IfPrimaryTermValue { get; set; }
	protected abstract string Operation { get; }
	protected abstract Type ClrType { get; }

	public TDescriptor Id(Id id) => Assign(id, (a, v) => a.IdValue = v);

	public TDescriptor IfSequenceNumber(long? ifSequenceNumber) => Assign(ifSequenceNumber, (a, v) => a.IfSequenceNoValue = v);

	public TDescriptor IfPrimaryTerm(long? ifPrimaryTerm) => Assign(ifPrimaryTerm, (a, v) => a.IfPrimaryTermValue = v);

	public TDescriptor Index(IndexName index) => Assign(index, (a, v) => a.IndexNameValue = v);

	public TDescriptor Index<T>() => Assign(typeof(T), (a, v) => a.IndexNameValue = v);
	public TDescriptor RequireAlias(bool? requireAlias = true) => Assign(requireAlias, (a, v) => a._requireAlias = v);

	public TDescriptor Routing(Routing routing) => Assign(routing, (a, v) => a.RoutingValue = v);

	public TDescriptor Version(long version) => Assign(version, (a, v) => a._version = v);

	public TDescriptor VersionType(VersionType? versionType) => Assign(versionType, (a, v) => a._versionType = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		IdValue = GetIdForOperation(settings.Inferrer);
		RoutingValue = GetRoutingForOperation(settings.Inferrer);

		writer.WriteStartObject();

		if (IdValue is not null)
		{
			// TODO - This flow is a bit inefficient and annoying just to get "clean" JSON
			var id = IdValue.GetString(settings);

			if (!string.IsNullOrEmpty(id))
			{
				writer.WritePropertyName("_id");
				writer.WriteStringValue(id);
			}
		}

		if (IfPrimaryTermValue.HasValue)
		{
			writer.WritePropertyName("if_primary_term");
			JsonSerializer.Serialize(writer, IfPrimaryTermValue.Value, options);
		}

		if (IfSequenceNoValue.HasValue)
		{
			writer.WritePropertyName("if_seq_no");
			JsonSerializer.Serialize(writer, IfSequenceNoValue.Value, options);
		}

		if (IndexNameValue is not null)
		{
			writer.WritePropertyName("_index");
			JsonSerializer.Serialize(writer, IndexNameValue, options);
		}

		if (RoutingValue is not null)
		{
			// TODO - This flow is a bit inefficient and annoying just to get "clean" JSON
			var value = RoutingValue.GetString(settings);

			if (!string.IsNullOrEmpty(value))
			{
				writer.WritePropertyName("routing");
				writer.WriteStringValue(value);
			}
		}

		if (_requireAlias.HasValue)
		{
			writer.WritePropertyName("require_alias");
			JsonSerializer.Serialize(writer, _requireAlias.Value, options);
		}

		if (_version.HasValue)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, _version.Value, options);
		}

		if (_versionType.HasValue)
		{
			writer.WritePropertyName("version_type");
			JsonSerializer.Serialize(writer, _versionType.Value, options);
		}

		SerializeInternal(writer, options, settings);

		writer.WriteEndObject();
	}

	/// <summary>
	/// Must be overridden in derived operations to write their own properties to the <see cref="Utf8JsonWriter"/>.
	/// </summary>
	protected abstract void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

	protected abstract void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting);

	protected abstract Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default);

	void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => Serialize(stream, settings, formatting);

	Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => SerializeAsync(stream, settings, formatting);

	protected abstract object GetBody();

	protected virtual Id GetIdForOperation(Inferrer inferrer) => IdValue ?? new Id(GetBody());

	protected virtual Routing GetRoutingForOperation(Inferrer inferrer) => RoutingValue ?? new Routing(GetBody());

	void IBulkOperation.PrepareIndex(IndexName bulkRequestIndex)
	{
		IndexNameValue ??= bulkRequestIndex ?? ClrType;

		if (bulkRequestIndex is not null && IndexNameValue.Equals(bulkRequestIndex))
			IndexNameValue = null;
	}
}
