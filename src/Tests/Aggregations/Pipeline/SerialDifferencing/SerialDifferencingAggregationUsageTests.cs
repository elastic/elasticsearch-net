using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.SerialDifferencing
{
	public class SerialDifferencingAggregationUsageTests : AggregationUsageTestBase
	{
		public SerialDifferencingAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
						thirtieth_difference = new
						{
							serial_diff = new
							{
								buckets_path = "commits",
								lag = 30
							}
						}
					}
				}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();

			var projectsPerMonth = response.Aggs.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Items.Should().NotBeNull();
			projectsPerMonth.Items.Count.Should().BeGreaterThan(0);

			foreach (var item in projectsPerMonth.Items)
			{
				var commits = item.Sum("commits");
				commits.Should().NotBeNull();
				commits.Value.Should().NotBe(null);
			}
		}

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
						.SerialDifferencing("thirtieth_difference", d => d
							.BucketsPath("commits")
							.Lag(30)
						)
					)
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>
		{
			Size = 0,
			Aggregations = new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				Aggregations =
					new SumAggregation("commits", "numberOfCommits") &&
					new SerialDifferencingAggregation("thirtieth_difference", "commits")
					{
						Lag = 30
					}
			}
		};
	}
}
