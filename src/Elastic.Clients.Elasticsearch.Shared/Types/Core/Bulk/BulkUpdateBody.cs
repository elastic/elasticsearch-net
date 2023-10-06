// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Core.Search;
#else
using Elastic.Clients.Elasticsearch.Core.Search;
#endif
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
using Elastic.Clients.Elasticsearch.Serialization;
#endif

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Core.Bulk;
#else
namespace Elastic.Clients.Elasticsearch.Core.Bulk;
#endif

internal abstract class BulkUpdateBody : ISelfSerializable
{
	public long? IfSequenceNumber { get; set; }

	public long? IfPrimaryTerm { get; set; }

	protected abstract void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

	void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
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

	public Script Script { get; set; }

	public bool? ScriptedUpsert { get; set; }

	public TDocument Upsert { get; set; }

	public Union<bool, SourceFilter> Source { get; set; }

	protected override void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (PartialUpdate is not null)
		{
			writer.WritePropertyName("doc");
			SourceSerialization.Serialize(PartialUpdate, writer, settings.SourceSerializer);
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
			SourceSerialization.Serialize(Upsert, writer, settings.SourceSerializer);
		}

		if (Source is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, Source, options);
		}
	}
}

