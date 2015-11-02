using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.QueryDsl
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class QueryDslUsageTestsBase : ApiTestBase<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected QueryDslUsageTestsBase(IIntegrationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(f ),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_search";

		protected abstract object QueryJson { get; }

		protected abstract QueryContainer QueryInitializer { get; }
		protected abstract QueryContainer QueryFluent(QueryContainerDescriptor<Project> q);

		protected override object ExpectJson => new { query = this.QueryJson };

		[U] public void FluentIsNotConditionless() => ((IQueryContainer)this.QueryFluent(new QueryContainerDescriptor<Project>())).IsConditionless.Should().BeFalse();
		[U] public void InitializerIsNotConditionless() => ((IQueryContainer)this.QueryInitializer).IsConditionless.Should().BeFalse();

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(this.QueryFluent);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = this.QueryInitializer
			};

	}
}
