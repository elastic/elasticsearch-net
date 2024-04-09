// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport.Products.Elasticsearch;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.IndexManagement.Serverless;
#else
namespace Elastic.Clients.Elasticsearch.IndexManagement;
#endif

public sealed partial class ExistsAliasResponse : ElasticsearchResponse
{
	public bool Exists => ApiCallDetails is
	{
		HasSuccessfulStatusCode: true, HttpStatusCode: 200
	};
}
