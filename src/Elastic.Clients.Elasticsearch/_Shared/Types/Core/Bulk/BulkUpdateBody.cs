// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using Elastic.Transport.Extensions;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

internal abstract class BulkUpdateBody
{
	public long? IfSequenceNumber { get; set; }

	public long? IfPrimaryTerm { get; set; }

	protected abstract void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

	internal void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();

		if (IfSequenceNumber.HasValue)
		{
			writer.WritePropertyName("if_seq_no");
			writer.WriteNumberValue(IfSequenceNumber.Value);
		}

		if (IfPrimaryTerm.HasValue)
		{
			writer.WritePropertyName("if_primary_term");
			writer.WriteNumberValue(IfPrimaryTerm.Value);
		}

		SerializeProperties(writer, options, settings);

		writer.WriteEndObject();
	}
}

internal class BulkUpdateBody<TDocument, TPartialUpdate> : BulkUpdateBody
{
	public bool? DocAsUpsert { get; set; }

	public TPartialUpdate PartialUpdate { get; set; }

	public Script? Script { get; set; }

	public bool? ScriptedUpsert { get; set; }

	public TDocument Upsert { get; set; }

	public Union<bool, SourceFilter>? Source { get; set; }

	protected override void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (PartialUpdate is not null)
		{
			writer.WritePropertyName("doc");
			settings.SourceSerializer.Serialize(PartialUpdate, writer);
		}

		writer.WriteProperty(options, "script", Script);
		writer.WriteProperty(options, "scripted_upsert", ScriptedUpsert);
		writer.WriteProperty(options, "doc_as_upsert", DocAsUpsert);

		if (Upsert is not null)
		{
			writer.WritePropertyName("upsert");
			settings.SourceSerializer.Serialize(Upsert, writer, settings.MemoryStreamFactory);
		}

		if (Source is not null)
		{
			writer.WritePropertyName("_source");
			switch (Source.Tag)
			{
				case UnionTag.T1:
					writer.WriteValue(options, Source.Value1);
					break;

				case UnionTag.T2:
					writer.WriteValue(options, Source.Value2);
					break;

				default:
					throw new InvalidOperationException();
			}
		}
	}
}
