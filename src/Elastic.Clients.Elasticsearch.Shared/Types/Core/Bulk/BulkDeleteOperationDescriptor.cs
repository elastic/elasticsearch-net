// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
using Elastic.Clients.Elasticsearch.Serialization;
#endif
using Elastic.Transport;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Core.Bulk;
#else
namespace Elastic.Clients.Elasticsearch.Core.Bulk;
#endif

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
		requestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		JsonSerializer.Serialize<BulkDeleteOperationDescriptor>(internalWriter, this, options);
		internalWriter.WriteEndObject();
		internalWriter.Flush();
	}

	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;
		var internalWriter = new Utf8JsonWriter(stream);
		internalWriter.WriteStartObject();
		internalWriter.WritePropertyName(Operation);
		requestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		JsonSerializer.Serialize<BulkDeleteOperationDescriptor>(internalWriter, this, options);
		internalWriter.WriteEndObject();
		await internalWriter.FlushAsync().ConfigureAwait(false);
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
