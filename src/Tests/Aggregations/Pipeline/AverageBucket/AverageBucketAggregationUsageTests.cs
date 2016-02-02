using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.AverageBucket
{
	public class AverageBucketAggregationUsageTests : AggregationUsageTestBase
	{
		public AverageBucketAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
				average_commits_per_month = new
				{
					avg_bucket = new
					{
						buckets_path = "projects_started_per_month>commits",
						gap_policy = "insert_zeros"
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
				.AverageBucket("average_commits_per_month", aaa => aaa
					.BucketsPath("projects_started_per_month>commits")
					.GapPolicy(GapPolicy.InsertZeros)
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
			&& new AverageBucketAggregation("average_commits_per_month", "projects_started_per_month>commits")
			{
				GapPolicy = GapPolicy.InsertZeros
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();

			var projectsPerMonth = response.Aggs.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			var averageCommits = response.Aggs.AverageBucket("average_commits_per_month");
			averageCommits.Should().NotBeNull();
			averageCommits.Value.Should().BeGreaterThan(0);
		}
	}
}
