// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

using Elastic.Transport.Extensions;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

public partial class GetSourceResponse<TDocument> : ISelfTwoWaySerializable
{
	public TDocument Body { get; set; }

	public void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) =>
		settings.SourceSerializer.Serialize(Body, writer);

	public void Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings) =>
		Body = settings.SourceSerializer.Deserialize<TDocument>(ref reader);
}
