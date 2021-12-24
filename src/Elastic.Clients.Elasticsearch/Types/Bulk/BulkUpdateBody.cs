// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.Json;

namespace Elastic.Clients.Elasticsearch
{
	internal class BulkUpdateBody<TDocument, TPartialUpdate> : BulkUpdateBodyBase
	{
		public bool? DocAsUpsert { get; set; }
				
		public TPartialUpdate PartialUpdate { get; set; }

		public ScriptBase Script { get; set; }

		public bool? ScriptedUpsert { get; set; }

		public TDocument Upsert { get; set; }

		public Union<bool, SourceFilter> Source { get; set; }

		protected override void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (PartialUpdate is not null)
			{
				writer.WritePropertyName("doc");
				SourceSerialisation.Serialize(PartialUpdate, writer, settings.SourceSerializer);
			}

			if (Script is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, Script, options);
			}

			if (ScriptedUpsert.HasValue)
			{
				writer.WritePropertyName("scripted_upsert");
				JsonSerializer.Serialize(writer, ScriptedUpsert.Value, options);
			}

			if (DocAsUpsert.HasValue)
			{
				writer.WritePropertyName("doc_as_upsert");
				JsonSerializer.Serialize(writer, DocAsUpsert.Value, options);
			}

			if (Upsert is not null)
			{
				writer.WritePropertyName("upsert");
				SourceSerialisation.Serialize(Upsert, writer, settings.SourceSerializer);
			}

			if (Source is not null)
			{
				writer.WritePropertyName("_source");
				JsonSerializer.Serialize(writer, Source, options);
			}
		}
	}
}
