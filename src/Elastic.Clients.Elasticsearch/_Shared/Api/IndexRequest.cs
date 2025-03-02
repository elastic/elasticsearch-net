// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public partial class IndexRequest<TDocument>
{
	public IndexRequest(TDocument document, Id id) : this(typeof(TDocument), id) => Document = document;

	protected override HttpMethod? DynamicHttpMethod => GetHttpMethod(this);

	public IndexRequest(TDocument document, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(document)) => Document = document;

	internal Request<IndexRequestParameters> Self => this;

	internal static HttpMethod GetHttpMethod(IndexRequest<TDocument> request) =>
		request.Id?.StringOrLongValue != null || request.RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
}

public sealed partial class IndexRequestDescriptor<TDocument>
{
	protected override HttpMethod? DynamicHttpMethod => RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
}
