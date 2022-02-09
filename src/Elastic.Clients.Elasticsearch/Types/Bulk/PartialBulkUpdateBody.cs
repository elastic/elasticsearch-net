// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

namespace Elastic.Clients.Elasticsearch
{
	internal class PartialBulkUpdateBody<TPartialUpdate> : BulkUpdateBodyBase
	{
		public bool? DocAsUpsert { get; set; }

		public TPartialUpdate PartialUpdate { get; set; }

		protected override void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (DocAsUpsert.HasValue)
			{
				writer.WritePropertyName("doc_as_upsert");
				writer.WriteBooleanValue(DocAsUpsert.Value);
			}

			if (PartialUpdate is not null)
			{
				writer.WritePropertyName("doc");
				SourceSerialisation.Serialize(PartialUpdate, writer, settings.SourceSerializer);
			}
		}
	}
}
