// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public class BulkDeleteOperationDescriptor : BulkOperationDescriptor<BulkDeleteOperationDescriptor>
{
	public BulkDeleteOperationDescriptor() { }

	public BulkDeleteOperationDescriptor(Id id) => Id(id);

	protected override string Operation => "delete";

	protected override Type ClrType => null;

	protected override object GetBody() => null;

	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;
		var internalWriter = new Utf8JsonWriter(stream);
		internalWriter.WriteStartObject();
		internalWriter.WritePropertyName(Operation);
		requestResponseSerializer.Serialize(this, internalWriter, settings.MemoryStreamFactory, formatting);
		internalWriter.WriteEndObject();
		internalWriter.Flush();
	}

	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;
		var internalWriter = new Utf8JsonWriter(stream);
		internalWriter.WriteStartObject();
		internalWriter.WritePropertyName(Operation);
		requestResponseSerializer.Serialize(this, internalWriter, settings.MemoryStreamFactory, formatting);
		internalWriter.WriteEndObject();
		await internalWriter.FlushAsync(cancellationToken).ConfigureAwait(false);
	}

	protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

public sealed class BulkDeleteOperationDescriptor<TDocument> : BulkDeleteOperationDescriptor
{
	public BulkDeleteOperationDescriptor(TDocument documentToDelete) : base (new Id(documentToDelete))
	{
		RoutingValue = new Routing(documentToDelete);
		IndexNameValue = IndexName.From<TDocument>();
	}
}
