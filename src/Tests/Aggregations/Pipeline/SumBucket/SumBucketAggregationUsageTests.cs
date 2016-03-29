using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.SumBucket
{
	public class SumBucketAggregationUsageTests : AggregationUsageTestBase
	{
		public SumBucketAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
				sum_of_commits = new
				{
					sum_bucket = new
					{
						buckets_path = "projects_started_per_month>commits"
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
				.SumBucket("sum_of_commits", aaa => aaa
					.BucketsPath("projects_started_per_month>commits")
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
			&& new SumBucketAggregation("sum_of_commits", "projects_started_per_month>commits")
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();

			var projectsPerMonth = response.Aggs.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			var commitsSum = response.Aggs.SumBucket("sum_of_commits");
			commitsSum.Should().NotBeNull();
			commitsSum.Value.Should().BeGreaterThan(0);
		}
	}
}
