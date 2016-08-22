using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.MultiSearch
{
	public class MultiSearchApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IMultiSearchResponse, IMultiSearchRequest, MultiSearchDescriptor, MultiSearchRequest>
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
			new {},
			new { from = 0, size = 10, query = new { match_all = new {} } },
			new { search_type = "count" },
			new {},
			new { index = "devs", type = "developer" },
			new { from = 0, size = 5, query = new { match_all = new {} } },
			new { index = "devs", type = "developer" },
			new { from = 0, size = 5, query = new { match_all = new {} } }
		};

		protected override Func<MultiSearchDescriptor, IMultiSearchRequest> Fluent => ms => ms
			.Index(typeof(Project))
			.Type(typeof(Project))
			.Search<Project>("10projects",s => s.Query(q => q.MatchAll()).From(0).Size(10))
			.Search<Project>("count_project", s => s.SearchType(SearchType.Count))
			.Search<Developer>("5developers", s => s.Query(q => q.MatchAll()).From(0).Size(5))
			.Search<Developer>("infer_type_name", s => s.Index("devs").From(0).Size(5).MatchAll());

		protected override MultiSearchRequest Initializer => new MultiSearchRequest(typeof(Project), typeof(Project))
		{
			Operations = new Dictionary<string, ISearchRequest>
			{
				{ "10projects", new SearchRequest<Project> { From = 0, Size = 10, Query = new QueryContainer(new MatchAllQuery()) } },
				{ "count_project", new SearchRequest<Project> { SearchType = SearchType.Count } },
				{ "5developers", new SearchRequest<Developer> { From = 0, Size = 5, Query = new QueryContainer(new MatchAllQuery()) } },
				{ "infer_type_name", new SearchRequest<Developer>("devs") { From = 0, Size = 5, Query = new QueryContainer(new MatchAllQuery()) } },
			}
		};

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.TotalResponses.Should().Be(4);

			var invalidResponses = r.GetInvalidResponses();
			invalidResponses.Should().BeEmpty();

			var allResponses = r.AllResponses.ToList();
			allResponses.Should().NotBeEmpty().And.HaveCount(4).And.OnlyContain(rr => rr.IsValid);

			var projects= r.GetResponse<Project>("10projects");
			projects.ShouldBeValid();
			projects.Documents.Should().HaveCount(10);

			var projectsCount = r.GetResponse<Project>("count_project");
			projectsCount.ShouldBeValid();
			projectsCount.Documents.Should().HaveCount(0);

			var developers = r.GetResponse<Developer>("5developers");
			developers.ShouldBeValid();
			developers.Documents.Should().HaveCount(5);

			var inferredTypeName = r.GetResponse<Developer>("infer_type_name");
			inferredTypeName.ShouldBeValid();
			inferredTypeName.Documents.Should().HaveCount(5);
		});
	}
}
