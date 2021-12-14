// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class BulkDeleteOperation<T> : BulkOperationBase
	{
		protected override string Operation => "delete";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			//stream.WriteByte(_newline);

			//settings.SourceSerializer.Serialize(Document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			//stream.WriteByte(_newline);

			//await settings.SourceSerializer.SerializeAsync(Document, stream, formatting).ConfigureAwait(false);
		}
	}
}
