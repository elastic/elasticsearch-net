using Elasticsearch.Net;
using Nest;
using System;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Aggregations
{
	[Collection(TypeOfCluster.ReadOnly)]
	public abstract class AggregationUsageTestBase
		: ApiIntegrationTestBase<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected AggregationUsageTestBase(IIntegrationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(FluentOverrides(f)),
			fluentAsync: (client, f) => client.SearchAsync<Project>(FluentOverrides(f)),
			request: (client, r) => client.Search<Project>(InitializerOverrides(r)),
			requestAsync: (client, r) => client.SearchAsync<Project>(InitializerOverrides(r))
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_search";

		protected virtual ISearchRequest FluentOverrides(Func<SearchDescriptor<Project>, ISearchRequest> f) =>
			f(new SearchDescriptor<Project>().Size(0));

		protected virtual ISearchRequest InitializerOverrides(SearchRequest<Project> r)
		{
			r.Size = 0;
			return r;
		}
	}
}
