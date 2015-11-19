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

namespace Tests.Search.MultiSearch
{
    [Collection(IntegrationContext.ReadOnly)]
	public class MultiSearchApiTests
		: ApiIntegrationTestBase<IMultiSearchResponse, IMultiSearchRequest, MultiSearchDescriptor, MultiSearchRequest>
	{
		public MultiSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.MultiSearch(f),
			fluentAsync: (c, f) => c.MultiSearchAsync(f),
			request: (c, r) => c.MultiSearch(r),
			requestAsync: (c, r) => c.MultiSearchAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_msearch";

		protected override bool SupportsDeserialization => false;
	
		protected override object ExpectJson => new object[]
		{
			new { },
			new { query = new { match_all = new { } }, from = 0, size = 10 },
			new { index = "non-existent-index" },
			new { query = new { match = new { name = new { query = "nest" } } } },
			new { index = "devs", type = "developer", search_type = "count" },
			new { query = new { match_all = new { } } }
		};

        protected override void ExpectResponse(IMultiSearchResponse response)
        {
            response.Should().NotBeNull();
            response.ApiCall.Should().NotBeNull();
            response.ApiCall.Success.Should().BeTrue();

            var innerResponses = response.GetResponses<Project>();
            innerResponses.Should().NotBeNullOrEmpty();
            var innerResponsesList = innerResponses.ToList();
            innerResponsesList.Should().HaveCount(2);

            // should be proper response with 10 documents in it
            var first = innerResponsesList[0];
            first.IsValid.Should().BeTrue();
            first.Documents.Should().HaveCount(10);

            // should contain error response because of inexistent index
            var second = innerResponsesList[1];
            second.IsValid.Should().BeFalse();
            second.ServerError.Should().NotBeNull();

            // made also a search request to devs index 
            var devResponses = response.GetResponses<Developer>();
            devResponses.Should().NotBeNullOrEmpty();
            var devResponse = devResponses.First();
            devResponse.IsValid.Should().BeTrue();

            // non fluent request - new SearchRequest<Developer>("devs") - should infer Type and set "type"="developer" ?? but it isn't
            // which creates a wrong request and count = 0
            // delete above comment after fix or change request to new SearchRequest<Developer>("devs","developer") if it shouldn't infer type
            devResponse.Total.Should().BeGreaterThan(0);
        }

        protected override Func<MultiSearchDescriptor, IMultiSearchRequest> Fluent => ms => ms
			.Index(typeof(Project))
			.Type(typeof(Project))
			.Search<Project>(s => s.Query(q => q.MatchAll()).From(0).Size(10))
			.Search<Project>(s => s.Index("non-existent-index").Query(q => q.Match(m => m.Field(p => p.Name).Query("nest"))))
			.Search<Developer>(s => s.Index("devs").SearchType(SearchType.Count).MatchAll());

		protected override MultiSearchRequest Initializer => new MultiSearchRequest(typeof(Project), typeof(Project))
		{
			Operations = new Dictionary<string, ISearchRequest>
			{
				{ "s1", new SearchRequest<Project> { From = 0, Size = 10, Query = new QueryContainer(new MatchAllQuery()) } },
				{ "s2", new SearchRequest<Project>("non-existent-index", typeof(Project)) { Query = new QueryContainer(new MatchQuery { Field = "name", Query = "nest" }) } },
				{ "s3", new SearchRequest<Developer>("devs") { SearchType = SearchType.Count, Query = new QueryContainer(new MatchAllQuery()) } },
			}
		};
	}
}
