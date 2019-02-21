using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.QueryDsl
{
	public abstract class QueryDslIntegrationTestsBase
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected QueryDslIntegrationTestsBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new { query = QueryJson };
		protected override int ExpectStatusCode => 200;

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(QueryFluent);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = QueryInitializer
			};

		protected abstract QueryContainer QueryInitializer { get; }

		protected abstract object QueryJson { get; }
		protected override string UrlPath => "/project/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Search<Project>(f),
			(client, f) => client.SearchAsync<Project>(f),
			(client, r) => client.Search<Project>(r),
			(client, r) => client.SearchAsync<Project>(r)
		);

		protected abstract QueryContainer QueryFluent(QueryContainerDescriptor<Project> q);
	}
}
