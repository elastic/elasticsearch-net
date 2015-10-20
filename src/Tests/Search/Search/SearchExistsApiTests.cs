using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using Tests.Framework.MockData;

namespace Tests.Search.Search
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SearchApiTests 
		: ApiIntegrationTestBase<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		public SearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.Search(f),
			fluentAsync: (c, f) => c.SearchAsync(f),
			request: (c, r) => c.Search<Project>(r),
			requestAsync: (c, r) => c.SearchAsync<Project>(r)
		);
		
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/project/_search";

		protected override SearchDescriptor<Project> NewDescriptor() => new SearchDescriptor<Project>();

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(q => q
				.MatchAll()
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			Query = new QueryContainer(new MatchAllQuery())
		};
	}
}
