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
	public sealed class BulkDeleteOperationDescriptor : BulkOperationDescriptorBase<BulkDeleteOperationDescriptor>
	{
		public BulkDeleteOperationDescriptor() { }

		public BulkDeleteOperationDescriptor(Id id) => Id(id);

		protected override string Operation => "delete";

		protected override object GetBody() => null;

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperationDescriptor>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperationDescriptor>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperationDescriptor>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperationDescriptor>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);
		}

		protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}
