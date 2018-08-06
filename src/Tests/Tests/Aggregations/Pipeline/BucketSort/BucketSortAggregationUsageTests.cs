using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.BucketSort
{
	[SkipVersion("<6.1.0", "Only valid in Elasticsearch 6.1.0+")]
	public class BucketSortAggregationUsageTests : AggregationUsageTestBase
	{
		public BucketSortAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
					},
					commits_bucket_sort = new
					{
						bucket_sort = new
						{
							sort = new[]
							{
								new { commits = new { order = "desc" } }
							},
							from = 0,
							size = 3,
							gap_policy = "insert_zeros"
						}
					}
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
					.BucketSort("commits_bucket_sort", bs => bs
						.Sort(s => s
							.Descending("commits")
						)
						.From(0)
						.Size(3)
						.GapPolicy(GapPolicy.InsertZeros)
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				Aggregations =
					new SumAggregation("commits", "numberOfCommits") &&
					new BucketSortAggregation("commits_bucket_sort")
					{
						Sort = new List<ISort>
						{
							new SortField { Field = "commits", Order = SortOrder.Descending }
						},
						From = 0,
						Size = 3,
						GapPolicy = GapPolicy.InsertZeros
					}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projectsPerMonth = response.Aggregations.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().Be(3);

			double previousCommits = -1;

			// sum of commits should descend over buckets
			foreach(var item in projectsPerMonth.Buckets)
			{
				var numberOfCommits = item.Sum("commits").Value.Value;
				if (previousCommits != -1)
					numberOfCommits.Should().BeLessOrEqualTo(previousCommits);

				previousCommits = numberOfCommits;
			}
		}
	}
}
