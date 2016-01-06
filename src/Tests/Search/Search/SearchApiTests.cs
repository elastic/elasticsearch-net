using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.Search
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SearchApiTests : ApiIntegrationTestBase<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
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

		protected override object ExpectJson => new
		{
			from = 10,
			size = 20,
			query = new
			{
				match_all = new { }
			},
			aggs = new
			{
				startDates = new
				{
					terms = new
					{
						field = "startedOn"
					}
				}
			},
			post_filter = new
			{
				term = new
				{
					state = new
					{
						value = "Stable"
					}

				}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.Hits.Count().Should().BeGreaterThan(0);
			response.Hits.First().Should().NotBeNull();
			response.Hits.First().Source.Should().NotBeNull();
			response.Aggregations.Count.Should().BeGreaterThan(0);
			response.Took.Should().BeGreaterThan(0);
			var startDates = response.Aggs.Terms("startDates");
			startDates.Should().NotBeNull();
		}

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.From(10)
			.Size(20)
			.Query(q => q
				.MatchAll()
			)
			.Aggregations(a => a
				.Terms("startDates", t => t
					.Field(p => p.StartedOn)
				)
			)
			.PostFilter(f => f
				.Term(p => p.State, StateOfBeing.Stable)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			From = 10,
			Size = 20,
			Query = new QueryContainer(new MatchAllQuery()),
			Aggregations = new TermsAggregation("startDates")
			{
				Field = "startedOn"
			},
			PostFilter = new QueryContainer(new TermQuery
			{
				Field = "state",
				Value = "Stable"
			})
		};
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class SearchApiFieldsTests : SearchApiTests
	{
		public SearchApiFieldsTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			from = 10,
			size = 20,
			query = new
			{
				match_all = new { }
			},
			aggs = new
			{
				startDates = new
				{
					terms = new
					{
						field = "startedOn"
					}
				}
			},
			post_filter = new
			{
				term = new
				{
					state = new
					{
						value = "Stable"
					}

				}
			},
			fields = new[] { "name", "numberOfCommits" }
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
		.From(10)
		.Size(20)
		.Query(q => q
			.MatchAll()
		)
		.Aggregations(a => a
			.Terms("startDates", t => t
				.Field(p => p.StartedOn)
			)
		)
		.PostFilter(f => f
			.Term(p => p.State, StateOfBeing.Stable)
		)
		.Fields(fs => fs
			.Field(p => p.Name)
			.Field(p => p.NumberOfCommits)
		);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			From = 10,
			Size = 20,
			Query = new QueryContainer(new MatchAllQuery()),
			Aggregations = new TermsAggregation("startDates")
			{
				Field = "startedOn"
			},
			PostFilter = new QueryContainer(new TermQuery
			{
				Field = "state",
				Value = "Stable"
			}),
			Fields = Infer.Fields<Project>(p => p.Name, p => p.NumberOfCommits)
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.Hits.Count().Should().BeGreaterThan(0);
			response.Hits.First().Should().NotBeNull();
			response.Hits.First().Fields.ValueOf<Project, string>(p => p.Name).Should().NotBeNullOrEmpty();
			response.Hits.First().Fields.ValueOf<Project, int>(p => p.NumberOfCommits).Should().BeGreaterThan(0);
			response.Aggregations.Count.Should().BeGreaterThan(0);
			var startDates = response.Aggs.Terms("startDates");
			startDates.Should().NotBeNull();
		}
	}
}
