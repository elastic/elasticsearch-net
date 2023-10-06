// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
using Elastic.Clients.Elasticsearch.Serialization;
#endif
using Elastic.Transport;
using System.Text.Json;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

public sealed partial class CreateRequest<TDocument> : ICustomJsonWriter
{

	public CreateRequest(Id id) : this(typeof(TDocument), id)
	{
	}

	public CreateRequest(TDocument documentWithId, IndexName index = null, Id id = null)
		: this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) =>
			Document = documentWithId;

	public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialization.Serialize(Document, writer, sourceSerializer);
}
