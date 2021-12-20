// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class BulkCreateOperation<T> : BulkOperationBase
	{
		private static byte _newline => (byte)'\n';

		public BulkCreateOperation(T document) => Document = document;

		[JsonPropertyName("pipeline")]
		public string? Pipeline { get; set; }

		[JsonPropertyName("require_alias")]
		public bool? RequireAlias { get; set; }

		[JsonPropertyName("dynamic_templates")]
		public Dictionary<string, string>? DynamicTemplates { get; set; }

		[JsonIgnore]
		public T Document { get; set; }
		
		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			using var writer = new Utf8JsonWriter(stream);
			SerializeOperation(settings, writer);
			writer.Flush();
			stream.WriteByte(_newline);
			settings.SourceSerializer.Serialize(GetBody(), stream, formatting);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			await using var writer = new Utf8JsonWriter(stream);
			SerializeOperation(settings, writer);
			await writer.FlushAsync().ConfigureAwait(false);
			stream.WriteByte(_newline);
			await settings.SourceSerializer.SerializeAsync(GetBody(), stream, formatting).ConfigureAwait(false);
		}

		private void SerializeOperation(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			writer.WriteStartObject();
			writer.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkCreateOperation<T>>(writer, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkCreateOperation<T>>(writer, this); // Unable to handle options if this were to ever be the case
			}

			writer.WriteEndObject();
		}

		protected override string Operation => "create";

		protected override object GetBody() => Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}
}
