// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.IO;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
using Elastic.Clients.Elasticsearch.Serialization;
#endif

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

public partial class GetSourceResponse<TDocument> : ISelfDeserializable
{
	public TDocument Body { get; set; }

	public void Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		using var jsonDoc = JsonSerializer.Deserialize<JsonDocument>(ref reader);

		using var stream = new MemoryStream();

		var writer = new Utf8JsonWriter(stream);
		jsonDoc.WriteTo(writer);
		writer.Flush();
		stream.Position = 0;

		var body = settings.SourceSerializer.Deserialize<TDocument>(stream);

		Body = body;
	}
}
