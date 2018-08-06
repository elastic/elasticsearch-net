using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.StatsBucket
{
	public class StatsBucketAggregationUsageTests : AggregationUsageTestBase
	{
		public StatsBucketAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object AggregationJson => new
		{
			projects_started_per_month = new
			{
				date_histogram = new
				{
					field = "startedOn",
					interval = "month",
				},
				aggs = new
				{
					commits = new
					{
						sum = new
						{
							field = "numberOfCommits"
						}
					}
				}
			},
			stats_commits_per_month = new
			{
				stats_bucket = new
				{
					buckets_path = "projects_started_per_month>commits"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("projects_started_per_month", dh => dh
				.Field(p => p.StartedOn)
				.Interval(DateInterval.Month)
				.Aggregations(aa => aa
					.Sum("commits", sm => sm
						.Field(p => p.NumberOfCommits)
					)
				)
			)
			.StatsBucket("stats_commits_per_month", aaa => aaa
				.BucketsPath("projects_started_per_month>commits")
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				Aggregations = new SumAggregation("commits", "numberOfCommits")
			}
			&& new StatsBucketAggregation("stats_commits_per_month", "projects_started_per_month>commits");

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projectsPerMonth = response.Aggregations.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			var commitsStats = response.Aggregations.StatsBucket("stats_commits_per_month");
			commitsStats.Should().NotBeNull();
			commitsStats.Average.Should().BeGreaterThan(0);
			commitsStats.Max.Should().BeGreaterThan(0);
			commitsStats.Min.Should().BeGreaterThan(0);
			commitsStats.Count.Should().BeGreaterThan(0);
			commitsStats.Sum.Should().BeGreaterThan(0);
		}
	}
}
