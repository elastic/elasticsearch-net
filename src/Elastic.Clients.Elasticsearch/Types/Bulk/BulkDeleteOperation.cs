// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch
{
	public class BulkDeleteOperation : BulkOperationBase
	{
		public BulkDeleteOperation(Id id) => Id = id;

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings)
		{
			SetValues(settings);
			using var writer = new Utf8JsonWriter(stream);
			SerializeInternal(settings, writer);
			writer.Flush();
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings)
		{
			SetValues(settings);
			await using var writer = new Utf8JsonWriter(stream);
			SerializeInternal(settings, writer);
			await writer.FlushAsync().ConfigureAwait(false);
		}

		protected virtual void SetValues(IElasticsearchClientSettings settings)
		{
		}

		private void SerializeInternal(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			writer.WriteStartObject();
			writer.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkDeleteOperation>(writer, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkDeleteOperation>(writer, this); // Unable to handle options if this were to ever be the case
			}

			writer.WriteEndObject();
		}

		protected override string Operation => "delete";

	}

	public sealed class BulkDeleteOperation<T> : BulkDeleteOperation
	{
		public BulkDeleteOperation(T document) : base (new Id(document))
			=> Document = document;

		public BulkDeleteOperation(Id id) : base(id) { }

		[JsonIgnore]
		public T Document { get; set; }

		protected override void SetValues(IElasticsearchClientSettings settings)
		{
			if (settings.ExperimentalEnableSerializeNullInferredValues)
			{
				Routing ??= new Routing(Document);
			}
			else if (Routing is null)
			{
				var routing = new Routing(Document);
				if (!string.IsNullOrEmpty(routing.GetString(settings)))
					Routing = routing;
			}

			Index ??= typeof(T);
		}		
	}
}
