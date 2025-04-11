// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public partial class IndexRequest<TDocument>
{
	protected override HttpMethod? DynamicHttpMethod => GetHttpMethod(this);

	internal static HttpMethod GetHttpMethod(IndexRequest<TDocument> request) =>
		request.Id?.StringOrLongValue != null || request.RouteValues.ContainsId ? HttpMethod.PUT : HttpMethod.POST;
}
