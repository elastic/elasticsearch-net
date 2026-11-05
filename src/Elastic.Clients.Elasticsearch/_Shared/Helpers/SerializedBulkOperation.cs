// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Threading.Tasks;

using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal sealed class SerializedBulkOperation :
	IBulkOperation,
	IStreamSerializable
{
	private readonly byte[] _bytes;

	private SerializedBulkOperation(byte[] bytes) => _bytes = bytes;

	// Includes the newline BulkRequest writes after each operation.
	public long RequestBytes => _bytes.Length + 1;

	public static SerializedBulkOperation Create(IBulkOperation operation, IndexName? bulkRequestIndex, IElasticsearchClientSettings settings)
	{
		if (operation is not IStreamSerializable serializable)
		{
			ThrowHelper.ThrowInvalidOperationForBulkWhenNotIStreamSerializable();
			return null!;
		}

		// Same steps as BulkRequest.Serialize.
		operation.PrepareIndex(bulkRequestIndex);

		using var stream = settings.MemoryStreamFactory.Create();
		serializable.Serialize(stream, settings, SerializationFormatting.None);
		return new SerializedBulkOperation(stream.ToArray());
	}

	// The serialized bytes already contain the resolved index.
	void IBulkOperation.PrepareIndex(IndexName? bulkRequestIndex)
	{ }

	void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) =>
		stream.Write(_bytes, 0, _bytes.Length);

	Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) =>
		stream.WriteAsync(_bytes, 0, _bytes.Length);
}
