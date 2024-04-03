// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Requests;
#else
using Elastic.Clients.Elasticsearch.Requests;
#endif
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
using Elastic.Clients.Elasticsearch.Serialization;
#endif
using Elastic.Transport;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

public partial class IndexRequest<TDocument> : ICustomJsonWriter
{
	public IndexRequest() : this(typeof(TDocument)) { }

	public IndexRequest(TDocument document, Id id) : this(typeof(TDocument), id) => Document = document;

	protected override HttpMethod? DynamicHttpMethod => GetHttpMethod(this);

	public IndexRequest(TDocument document, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(document)) => Document = document;

	internal Request<IndexRequestParameters> Self => this;

	[JsonIgnore] private Id? Id => RouteValues.Get<Id>("id");

	void ICustomJsonWriter.WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialization.Serialize(Document, writer, sourceSerializer);

	internal static HttpMethod GetHttpMethod(IndexRequest<TDocument> request) =>
		request.Id?.StringOrLongValue != null || request.RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
}

public sealed partial class IndexRequestDescriptor<TDocument> : ICustomJsonWriter
{
	internal Id _id;

	public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialization.Serialize(DocumentValue, writer, sourceSerializer);

	protected override HttpMethod? DynamicHttpMethod => _id is not null || RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
}
