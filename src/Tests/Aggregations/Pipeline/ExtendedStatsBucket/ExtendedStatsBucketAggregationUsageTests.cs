using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.ExtendedStatsBucket
{
	public class ExtendedStatsBucketAggregationUsageTests : AggregationUsageTestBase
	{
		public ExtendedStatsBucketAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			size = 0,
			aggs = new
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
				extended_stats_commits_per_month = new
				{
					extended_stats_bucket = new
					{
						buckets_path = "projects_started_per_month>commits",
						sigma = 2.0
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Aggregations(a => a
				.DateHistogram("projects_started_per_month", dh => dh
					.Field(p => p.StartedOn)
					.Interval(DateInterval.Month)
					.Aggregations(aa => aa
						.Sum("commits", sm => sm
							.Field(p => p.NumberOfCommits)
						)
					)
				)
				.ExtendedStatsBucket("extended_stats_commits_per_month", aaa => aaa
					.BucketsPath("projects_started_per_month>commits")
					.Sigma(2.0)
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			Size = 0,
			Aggregations = new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				Aggregations = new SumAggregation("commits", "numberOfCommits")
			}
			&& new ExtendedStatsBucketAggregation("extended_stats_commits_per_month", "projects_started_per_month>commits")
			{
				Sigma = 2.0
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();

			var projectsPerMonth = response.Aggs.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			var commitsStats = response.Aggs.ExtendedStatsBucket("extended_stats_commits_per_month");
			commitsStats.Should().NotBeNull();
			commitsStats.Average.Should().BeGreaterThan(0);
			commitsStats.Max.Should().BeGreaterThan(0);
			commitsStats.Min.Should().BeGreaterThan(0);
			commitsStats.Count.Should().BeGreaterThan(0);
			commitsStats.Sum.Should().BeGreaterThan(0);
			commitsStats.SumOfSquares.Should().BeGreaterThan(0);
			commitsStats.StdDeviation.Should().BeGreaterThan(0);
			commitsStats.StdDeviationBounds.Should().NotBeNull();
			commitsStats.StdDeviationBounds.Upper.Should().BeGreaterThan(0);
			commitsStats.StdDeviationBounds.Lower.Should().NotBe(0);
		}
	}
}
