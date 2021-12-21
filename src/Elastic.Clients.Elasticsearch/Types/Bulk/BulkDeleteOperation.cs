// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class BulkDeleteOperation<T> : BulkOperationBase
	{
		public BulkDeleteOperation(T document) => Document = document;

		public BulkDeleteOperation(Id id) => Id = id;

		[JsonIgnore]
		public T Document { get; set; }

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			using var writer = new Utf8JsonWriter(stream);
			SerializeInternal(settings, writer);
			writer.Flush();
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			await using var writer = new Utf8JsonWriter(stream);
			SerializeInternal(settings, writer);
			await writer.FlushAsync().ConfigureAwait(false);
		}

		private void SerializeInternal(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			writer.WriteStartObject();
			writer.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(writer, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperation<T>>(writer, this); // Unable to handle options if this were to ever be the case
			}

			writer.WriteEndObject();
		}

		protected override string Operation => "delete";

		protected override Type ClrType => typeof(T);

		protected override object GetBody() => null;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}
}
