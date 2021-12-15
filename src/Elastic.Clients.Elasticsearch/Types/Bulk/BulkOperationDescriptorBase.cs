// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public abstract class BulkOperationDescriptorBase<TDescriptor> : DescriptorBase<TDescriptor>, IBulkOperation, IStreamSerializable where TDescriptor : BulkOperationDescriptorBase<TDescriptor>
	{
		private Id _id;
		private long? _version;
		private IndexName _index;
		private Routing _routing;
		private VersionType? _versionType;
		private long? _ifSequenceNo;
		private long? _ifPrimaryTerm;

		protected abstract string Operation { get; }

		public TDescriptor Id(Id id) => Assign(id, (a, v) => a._id = v);

		public TDescriptor IfSequenceNumber(long? ifSequenceNumber) => Assign(ifSequenceNumber, (a, v) => a._ifSequenceNo = v);

		public TDescriptor IfPrimaryTerm(long? ifPrimaryTerm) => Assign(ifPrimaryTerm, (a, v) => a._ifPrimaryTerm = v);

		public TDescriptor Index(IndexName index) => Assign(index, (a, v) => a._index = v);

		public TDescriptor Index<T>() => Assign(typeof(T), (a, v) => a._index = v);

		public TDescriptor Routing(Routing routing) => Assign(routing, (a, v) => a._routing = v);

		public TDescriptor Version(long version) => Assign(version, (a, v) => a._version = v);

		public TDescriptor VersionType(VersionType? versionType) => Assign(versionType, (a, v) => a._versionType = v);

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();

			SerializeInternal(writer, options, settings);

			if (_id is not null)
			{
				writer.WritePropertyName("_id");
				JsonSerializer.Serialize(writer, _id, options);
			}

			if (_ifPrimaryTerm.HasValue)
			{
				writer.WritePropertyName("if_primary_term");
				JsonSerializer.Serialize(writer, _ifPrimaryTerm.Value, options);
			}

			if (_ifSequenceNo.HasValue)
			{
				writer.WritePropertyName("if_seq_no");
				JsonSerializer.Serialize(writer, _ifSequenceNo.Value, options);
			}

			if (_index is not null)
			{
				writer.WritePropertyName("_index");
				JsonSerializer.Serialize(writer, _index, options);
			}

			if (_routing is not null)
			{
				writer.WritePropertyName("routing");
				JsonSerializer.Serialize(writer, _routing, options);
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

			writer.WriteEndObject();
		}

		protected abstract void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

		protected abstract void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting);

		protected abstract Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default);

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => Serialize(stream, settings, formatting);

		Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => SerializeAsync(stream, settings, formatting);

		protected abstract object GetBody();

		protected virtual Id GetIdForOperation(Inferrer inferrer) => _id ?? new Id(GetBody());

		protected virtual Routing GetRoutingForOperation(Inferrer inferrer) => _routing ?? new Routing(GetBody());
	}
}
