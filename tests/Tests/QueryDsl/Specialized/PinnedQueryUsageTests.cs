// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized;

public class PinnedQueryUsageTests : QueryDslUsageTestsBase
{
	public PinnedQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override Query QueryInitializer
	{
		get
		{
			var query = PinnedQuery.Ids(new Id[] { 1, 11, 22 });

			query.QueryName = "named_query";
			query.Boost = 1.25f;
			query.Organic = new MatchAllQuery { QueryName = "organic_query" };

			return query;
		}
	}

	protected override QueryDescriptor<Project> QueryFluent(QueryDescriptor<Project> queryDescriptor)
	{
		// The descriptor for the pinned query is not complete as it doesn't support setting the variant (ids/docs).
		// For now, we workaround this by passing in the PinnedQuery directly to the supported overload.

		var query = PinnedQuery.Ids(new Id[] { 1, 11, 22 });

		query.QueryName = "named_query";
		query.Boost = 1.25f;
		query.Organic = new MatchAllQuery { QueryName = "organic_query" };

		return queryDescriptor.Pinned(query);
	}
}
