using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.PercentilesBucket
{
	public class PercentilesBucketAggregationUsageTests : AggregationUsageTestBase
	{
		public PercentilesBucketAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
				commits_outlier = new
				{
					percentiles_bucket = new
					{
						buckets_path = "projects_started_per_month>commits",
						percents = new[] { 95.0, 99.0, 99.9 }
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
				.PercentilesBucket("commits_outlier", aaa => aaa
					.BucketsPath("projects_started_per_month>commits")
					.Percents(95, 99, 99.9)
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
			&& new PercentilesBucketAggregation("commits_outlier", "projects_started_per_month>commits")
			{
				Percents = new[] { 95, 99, 99.9 }
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();

			var projectsPerMonth = response.Aggs.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			var commitsOutlier = response.Aggs.PercentilesBucket("commits_outlier");
			commitsOutlier.Should().NotBeNull();
			commitsOutlier.Items.Should().NotBeNullOrEmpty();
			foreach (var item in commitsOutlier.Items)
				item.Value.Should().BeGreaterThan(0);
		}
	}
}
