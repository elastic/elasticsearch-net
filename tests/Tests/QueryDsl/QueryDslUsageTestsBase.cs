// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Tests.Core.Extensions;

namespace Tests.QueryDsl;

public abstract class QueryDslUsageTestsBase : ApiTestBase<ReadOnlyCluster, SearchResponse<Project>, SearchRequestDescriptor<Project>, SearchRequest<Project>>
{
	protected QueryDslUsageTestsBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool VerifyJson => true;

	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Query(q => QueryFluent(q));

	protected override HttpMethod ExpectHttpMethod => HttpMethod.POST;

	protected override SearchRequest<Project> Initializer =>
		new()
		{
			Query = QueryInitializer
		};

	protected abstract Query QueryInitializer { get; }

	protected override string ExpectedUrlPathAndQuery => "/project/_search";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Search(f),
		(client, f) => client.SearchAsync(f),
		(client, r) => client.Search<Project>(r),
		(client, r) => client.SearchAsync<Project>(r)
	);

	protected abstract QueryDescriptor<Project> QueryFluent(QueryDescriptor<Project> queryDescriptor);

	[I]
	protected async Task AssertQueryResponse() => await AssertOnAllResponses(r =>
	{
		r.ShouldBeValid();
	});
}
