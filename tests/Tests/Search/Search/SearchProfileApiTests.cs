// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Search
{
	public class SearchProfileApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest,
			SearchDescriptor<Project>, SearchRequest<Project>>
	{
		public SearchProfileApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			profile = true,
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
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Profile()
			.Query(q => q
				.MatchAll()
			)
			.Aggregations(aggs => aggs
				.Terms("startDates", t => t
					.Field(p => p.StartedOn)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			Profile = true,
			Query = new QueryContainer(new MatchAllQuery()),
			Aggregations = new TermsAggregation("startDates")
			{
				Field = "startedOn"
			}
		};

		protected override string UrlPath => $"/project/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Search(f),
			(c, f) => c.SearchAsync(f),
			(c, r) => c.Search<Project>(r),
			(c, r) => c.SearchAsync<Project>(r)
		);

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.Hits.Count().Should().BeGreaterThan(0);
			var profile = response.Profile;
			profile.Should().NotBeNull();
			var shardProfiles = profile.Shards;
			shardProfiles.Should().NotBeNullOrEmpty();
			foreach (var shardProfile in shardProfiles)
			{
				shardProfile.Id.Should().NotBeNullOrEmpty();
				var searches = shardProfile.Searches;
				searches.Should().NotBeNullOrEmpty();
				foreach (var search in searches)
				{
					var queries = search.Query;
					queries.Should().NotBeNullOrEmpty();
					foreach (var query in queries)
					{
						query.Should().NotBeNull();
						query.Type.Should().NotBeNullOrEmpty();
						query.Description.Should().NotBeNullOrEmpty();
						query.TimeInNanoseconds.Should().BeGreaterThan(0);
						query.Breakdown.Should().NotBeNull();
					}
					search.RewriteTime.Should().BeGreaterThan(0);
					var collectors = search.Collector;
					foreach (var collector in collectors)
					{
						collector.Name.Should().NotBeNullOrEmpty();
						collector.Reason.Should().NotBeNullOrEmpty();
						collector.TimeInNanoseconds.Should().BeGreaterThan(0);
						var children = collector.Children;
						children.Should().NotBeNull();
						foreach (var child in children)
						{
							child.Should().NotBeNull();
							child.Name.Should().NotBeNullOrEmpty();
							child.Reason.Should().NotBeNullOrEmpty();
							child.TimeInNanoseconds.Should().BeGreaterThan(0);
							var grandchildren = child.Children;
							grandchildren.Should().NotBeNull();
							foreach (var grandchild in grandchildren)
							{
								grandchild.Name.Should().NotBeNullOrEmpty();
								grandchild.Reason.Should().NotBeNullOrEmpty();
								grandchild.TimeInNanoseconds.Should().BeGreaterThan(0);
							}
						}
					}
				}
				var aggregations = shardProfile.Aggregations;
				aggregations.Should().NotBeNull();
				foreach (var aggregation in aggregations)
				{
					aggregation.Should().NotBeNull();
					aggregation.Type.Should().NotBeNullOrEmpty();
					aggregation.Description.Should().NotBeNullOrEmpty();
					aggregation.TimeInNanoseconds.Should().BeGreaterThan(0);
					aggregation.Breakdown.Should().NotBeNull();
				}
			}
		}
	}
}
