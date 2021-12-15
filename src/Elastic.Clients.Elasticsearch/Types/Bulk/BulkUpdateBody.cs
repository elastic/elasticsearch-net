// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	internal class BulkUpdateBody<TDocument, TPartialUpdate> : BulkUpdateBodyBase
	{
		[JsonPropertyName("doc_as_upsert")]
		public bool? DocAsUpsert { get; set; }

		[JsonPropertyName("doc")]
		//[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		public TPartialUpdate PartialUpdate { get; set; }

		[JsonPropertyName("script")]
		public ScriptBase Script { get; set; }

		[JsonPropertyName("scripted_upsert")]
		public bool? ScriptedUpsert { get; set; }

		[JsonPropertyName("upsert")]
		//[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		public TDocument Upsert { get; set; }

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (PartialUpdate is not null)
			{
				writer.WritePropertyName("doc");
				SourceSerialisation.Serialize(PartialUpdate, writer, settings.SourceSerializer);
				return;
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

			// TODO - Finish this! Upsert
		}

		// TODO
		//[DataMember(Name = "_source")]
		//internal Union<bool, ISourceFilter> Source { get; set; }
	}
}
