// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;
#else
namespace Elastic.Clients.Elasticsearch.QueryDsl;
#endif

public partial class BoolQuery
{
	internal bool Locked => !QueryName.IsNullOrEmpty() || Boost.HasValue || MinimumShouldMatch is not null;
}
