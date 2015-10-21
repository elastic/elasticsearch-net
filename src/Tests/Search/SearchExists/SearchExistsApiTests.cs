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

namespace Tests.Search.SearchExists
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SearchExistsApiTests : ApiIntegrationTestBase<IExistsResponse, ISearchExistsRequest, SearchExistsDescriptor<Project>, SearchExistsRequest<Project>>
	{
		public SearchExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.SearchExists(f),
			fluentAsync: (c, f) => c.SearchExistsAsync(f),
			request: (c, r) => c.SearchExists(r),
			requestAsync: (c, r) => c.SearchExistsAsync(r)
		);
		
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/project/_search/exists";

		protected override object ExpectJson => new
		{
			query = new
			{
				match_all = new { }
			}
		};
		
		protected override Func<SearchExistsDescriptor<Project>, ISearchExistsRequest> Fluent => s => s
			.Query(q => q
				.MatchAll()
			);

		protected override SearchExistsRequest<Project> Initializer => new SearchExistsRequest<Project>()
		{
			Query = new QueryContainer(new MatchAllQuery())
		};
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class SearchDoesntExistApiTests 
		: ApiIntegrationTestBase<IExistsResponse, ISearchExistsRequest, SearchExistsDescriptor<Project>, SearchExistsRequest<Project>>
	{
		public SearchDoesntExistApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.SearchExists(f),
			fluentAsync: (c, f) => c.SearchExistsAsync(f),
			request: (c, r) => c.SearchExists(r),
			requestAsync: (c, r) => c.SearchExistsAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/project/_search/exists";

		private string _query = Guid.NewGuid().ToString();

		protected override object ExpectJson => new
		{
			query = new
			{
				match = new
				{
					name = new
					{
						query = _query
					}
				}
			}
		};
		
		protected override Func<SearchExistsDescriptor<Project>, ISearchExistsRequest> Fluent => s => s
			.Query(q => q
				.Match(m => m
					.OnField(p => p.Name)
					.Query(_query)
				)
			);

		protected override SearchExistsRequest<Project> Initializer => new SearchExistsRequest<Project>()
		{
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = _query
			})
		};

		[I] public async Task ExistsIsFalse() => await this.AssertOnAllResponses(r => r.Exists.Should().BeFalse());
	}
}
