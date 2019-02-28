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
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Search.MultiSearch
{
	[SkipVersion(">5.0.0-alpha1", "format of percolate query changed.")]
	public class MultiSearchApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IMultiSearchResponse, IMultiSearchRequest, MultiSearchDescriptor, MultiSearchRequest>
	{
		public MultiSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new object[]
		{
			new { },
			new { from = 0, size = 10, query = new { match_all = new { } } },
			new { search_type = "dfs_query_then_fetch" },
			new { },
			new { index = "devs" },
			new { from = 0, size = 5, query = new { match_all = new { } } },
			new { index = "devs" },
			new { from = 0, size = 5, query = new { match_all = new { } } },
			new { index = "queries" },
			new { query = new { percolate = new { document = Project.InstanceAnonymous, field = "query", routing = Project.First.Name } } },
			new { index = "queries" },
			new
			{
				query = new
				{
					percolate = new
						{ index = "project", id = Project.First.Name, version = 1, field = "query", routing = Project.First.Name }
				}
			},
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<MultiSearchDescriptor, IMultiSearchRequest> Fluent => ms => ms
			.Index(typeof(Project))
			.Search<Project>("10projects", s => s.Query(q => q.MatchAll()).From(0).Size(10))
			.Search<Project>("dfs_projects", s => s.SearchType(SearchType.DfsQueryThenFetch))
			.Search<Developer>("5developers", s => s.Query(q => q.MatchAll()).From(0).Size(5))
			.Search<Developer>("infer_type_name", s => s.Index("devs").From(0).Size(5).MatchAll())
			.Search<ProjectPercolation>("percolate_document", s => s
				.Index<ProjectPercolation>()
				.Query(q => q
					.Percolate(p => p
						.Document(Project.Instance)
						.Field(f => f.Query)
					)
				)
			)
			.Search<ProjectPercolation>("percolate_existing_document", s => s
				.Index<ProjectPercolation>()
				.Query(q => q
					.Percolate(p => p
						.Index<Project>()
						.Id(Project.First.Name)
						.Version(1)
						.Routing(Project.First.Name)
						.Field(f => f.Query)
					)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MultiSearchRequest Initializer => new MultiSearchRequest(typeof(Project))
		{
			Operations = new Dictionary<string, ISearchRequest>
			{
				{ "10projects", new SearchRequest<Project> { From = 0, Size = 10, Query = new QueryContainer(new MatchAllQuery()) } },
				{ "dfs_projects", new SearchRequest<Project> { SearchType = SearchType.DfsQueryThenFetch } },
				{ "5developers", new SearchRequest<Developer> { From = 0, Size = 5, Query = new QueryContainer(new MatchAllQuery()) } },
				{ "infer_type_name", new SearchRequest<Developer>("devs") { From = 0, Size = 5, Query = new QueryContainer(new MatchAllQuery()) } },
				{
					"percolate_document", new SearchRequest<ProjectPercolation>()
					{
						Query = new QueryContainer(new PercolateQuery
						{
							Document = Project.Instance,
							Field = Infer.Field<ProjectPercolation>(f => f.Query)
						})
					}
				},
				{
					"percolate_existing_document", new SearchRequest<ProjectPercolation>()
					{
						Query = new QueryContainer(new PercolateQuery
						{
							Index = typeof(Project),
							Id = Project.First.Name,
							Version = 1,
							Routing = Project.First.Name,
							Field = Infer.Field<ProjectPercolation>(f => f.Query)
						})
					}
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
			r.TotalResponses.Should().Be(6);

			var invalidResponses = r.GetInvalidResponses();
			invalidResponses.Should().BeEmpty();

			var allResponses = r.AllResponses.ToList();
			allResponses.Should().NotBeEmpty().And.HaveCount(6).And.OnlyContain(rr => rr.IsValid);

			var projects = r.GetResponse<Project>("10projects");
			projects.ShouldBeValid();
			projects.Documents.Should().HaveCount(10);

			var projectsCount = r.GetResponse<Project>("count_project");
			projectsCount.Should().BeNull();

			var developers = r.GetResponse<Developer>("5developers");
			developers.ShouldBeValid();
			developers.Documents.Should().HaveCount(5);

			var inferredTypeName = r.GetResponse<Developer>("infer_type_name");
			inferredTypeName.ShouldBeValid();
			inferredTypeName.Documents.Should().HaveCount(5);

			var percolateDocument = r.GetResponse<ProjectPercolation>("percolate_document");
			percolateDocument.ShouldBeValid();
			percolateDocument.Documents.Should().HaveCount(1);

			var percolateExistingDocument = r.GetResponse<ProjectPercolation>("percolate_existing_document");
			percolateExistingDocument.ShouldBeValid();
			percolateExistingDocument.Documents.Should().HaveCount(1);
		});
	}
}
