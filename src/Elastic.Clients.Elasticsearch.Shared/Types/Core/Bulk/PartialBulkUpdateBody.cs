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

internal class PartialBulkUpdateBody<TPartialUpdate> : BulkUpdateBody
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
			SourceSerialization.Serialize(PartialUpdate, writer, settings.SourceSerializer);
		}
	}
}
