// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
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

internal class ScriptedBulkUpdateBody : BulkUpdateBody
{
	public Script Script { get; set; }

	protected override void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (Script is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, Script, options);
		}
	}
}

internal class ScriptedBulkUpdateBody<TDocument> : ScriptedBulkUpdateBody
{
	public TDocument Upsert { get; set; }

	protected override void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (Script is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, Script, options);
		}

		if (Upsert is not null)
		{
			writer.WritePropertyName("upsert");
			SourceSerialization.Serialize(Upsert, writer, settings.SourceSerializer);
		}
	}
}
