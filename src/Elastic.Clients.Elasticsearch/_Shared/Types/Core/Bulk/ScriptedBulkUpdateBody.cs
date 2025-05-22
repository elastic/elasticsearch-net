// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

internal class ScriptedBulkUpdateBody : BulkUpdateBody
{
	public Script Script { get; set; }

	protected override void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteProperty(options, "script", Script);
	}
}

internal class ScriptedBulkUpdateBody<TDocument> : ScriptedBulkUpdateBody
{
	public TDocument Upsert { get; set; }

	protected override void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteProperty(options, "script", Script);

		if (Upsert is not null)
		{
			writer.WritePropertyName("upsert");
			settings.SourceSerializer.Serialize(Upsert, writer, settings.MemoryStreamFactory);
		}
	}
}
