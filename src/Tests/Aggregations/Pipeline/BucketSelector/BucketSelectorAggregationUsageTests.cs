using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.BucketSelector
{
	public class BucketSelectorAggregationUsageTests : AggregationUsageTestBase
	{
		public BucketSelectorAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		
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
						},
						commits_bucket_filter = new
						{
							bucket_selector = new
							{
								buckets_path = new
								{
									totalCommits = "commits"
								},
								script = new
								{
									inline = "totalCommits >= 500"
								}
							}
						}
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
						.BucketSelector("commits_bucket_filter", bs => bs
							.BucketsPath(bp => bp
								.Add("totalCommits", "commits")
							)
							.Script("totalCommits >= 500")
						)
					)
				)
			);
		
		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			Size = 0,
			Aggregations = new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				Aggregations = 
					new SumAggregation("commits", "numberOfCommits") &&
					new BucketSelectorAggregation("commits_bucket_filter", new MultiBucketsPath
						{
							{ "totalCommits", "commits" },
						})
					{
						Script = (InlineScript)"totalCommits >= 500"
					}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();

			var projectsPerMonth = response.Aggs.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			foreach(var item in projectsPerMonth.Buckets)
			{
				var commits = item.Sum("commits");
				commits.Should().NotBeNull();
				commits.Value.Should().BeGreaterOrEqualTo(500);
			}
		}
	}
}
