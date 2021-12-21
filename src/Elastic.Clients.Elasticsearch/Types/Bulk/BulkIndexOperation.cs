// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class BulkIndexOperation<T> : BulkOperationBase
	{
		private static byte _newline => (byte)'\n';

		public BulkIndexOperation(T document) => Document = document;

		[JsonPropertyName("pipeline")]
		public string? Pipeline { get; set; }

		[JsonPropertyName("require_alias")]
		public bool? RequireAlias { get; set; }

		[JsonPropertyName("dynamic_templates")]
		public Dictionary<string, string>? DynamicTemplates { get; set; }

		[JsonIgnore]
		public T Document { get; set; }

		protected override string Operation => "index";

		protected override Type ClrType => typeof(T);

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(Document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			await settings.SourceSerializer.SerializeAsync(Document, stream, formatting).ConfigureAwait(false);
		}

		protected override object GetBody() => Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}
}
