// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Esql;
#else

namespace Elastic.Clients.Elasticsearch.Esql;
#endif

public sealed partial class EsqlQueryResponse
{
	public byte[] Data { get; init; }
}
