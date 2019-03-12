using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.MultiSearch
{
	public class MultiSearchInvalidApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IMultiSearchResponse, IMultiSearchRequest, MultiSearchDescriptor, MultiSearchRequest>
	{
		public MultiSearchInvalidApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false; //2 out of the three searches are not valid

		protected override object ExpectJson => new object[]
		{
			new { },
			new { query = new { match_all = new { } }, from = 0, size = 10 },
			new { index = "otherindex" },
			new { query = new { match = new { name = new { query = "nest" } } } },
			new { index = "otherindex", search_type = "dfs_query_then_fetch" },
			new { query = new { match_all = new { } } }
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<MultiSearchDescriptor, IMultiSearchRequest> Fluent => ms => ms
			.Index(typeof(Project))
			.Search<Project>(s => s.Query(q => q.MatchAll()).From(0).Size(10))
			.Search<Project>(s => s.Index("otherindex").Query(q => q.Match(m => m.Field(p => p.Name).Query("nest"))))
			.Search<Project>(s => s.Index("otherindex").SearchType(SearchType.DfsQueryThenFetch).Query(q => q.MatchAll()));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MultiSearchRequest Initializer => new MultiSearchRequest(typeof(Project))
		{
			Operations = new Dictionary<string, ISearchRequest>
			{
				{ "s1", new SearchRequest<Project> { From = 0, Size = 10, Query = new QueryContainer(new MatchAllQuery()) } },
				{
					"s2",
					new SearchRequest<Project>("otherindex")
						{ Query = new QueryContainer(new MatchQuery { Field = "name", Query = "nest" }) }
				},
				{
					"s3",
					new SearchRequest<Project>("otherindex")
						{ SearchType = SearchType.DfsQueryThenFetch, Query = new QueryContainer(new MatchAllQuery()) }
				},
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => "/project/_msearch";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.MultiSearch(f),
			(c, f) => c.MultiSearchAsync(f),
			(c, r) => c.MultiSearch(r),
			(c, r) => c.MultiSearchAsync(r)
		);

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.TotalResponses.Should().Be(3);

			/** GetResponses also returns invalid requests **/
			var responses = r.GetResponses<Project>().ToList();
			responses.First().ShouldBeValid();
			AssertInvalidResponse(responses[1]);
			AssertInvalidResponse(responses[2]);

			/** GetInvalidResponses returns all the invalid responses as IResponse **/
			var invalidResponses = r.GetInvalidResponses();
			invalidResponses.Should().NotBeNull().And.HaveCount(2);
			foreach (var response in invalidResponses)
				AssertInvalidResponse(response);
		});

		private void AssertInvalidResponse(IResponse searchResponse)
		{
			searchResponse.ShouldNotBeValid();

			searchResponse.ServerError.Should().NotBeNull();
			searchResponse.ServerError.Status.Should().Be(404);
			searchResponse.ServerError.Error.Should().NotBeNull();
			searchResponse.ServerError.Error.Type.Should().Be("index_not_found_exception");
			searchResponse.ServerError.Error.Reason.Should().StartWith("no such index");
		}
	}
}
