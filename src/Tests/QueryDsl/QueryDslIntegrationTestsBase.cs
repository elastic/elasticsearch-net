using System;
using System.Collections.Generic;
using Elasticsearch.Net_5_2_0;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.QueryDsl
{
	public abstract class QueryDslIntegrationTestsBase : ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected QueryDslIntegrationTestsBase(ClusterBase cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(f),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_search";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;

		protected abstract object QueryJson { get; }

		protected abstract QueryContainer QueryInitializer { get; }
		protected abstract QueryContainer QueryFluent(QueryContainerDescriptor<Project> q);

		protected override object ExpectJson => new { query = this.QueryJson };

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(this.QueryFluent);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = this.QueryInitializer
			};
	}
}
