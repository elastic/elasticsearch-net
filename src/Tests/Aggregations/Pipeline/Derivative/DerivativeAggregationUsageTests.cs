using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.Derivative
{
	public class DerivativeAggregationUsageTests : AggregationUsageTestBase
	{
		public DerivativeAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
				aggs = new
				{
					commits_derivative = new
					{
						derivative = new
						{
							buckets_path = "projects_started_per_month>commits"
						}
					}
				}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			base.ExpectResponse(response);
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
					)
				)
				.Derivative("commits_derivative", d => d
					.BucketsPath("projects_started_per_month>commits")
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>
		{
			Size = 0,
			Aggregations = new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				Aggregations = new SumAggregation("commits", "numberOfCommits")
			}
			&& new DerivativeAggregation("commits_derivative", "projects_started_per_month>commits")
		};
	}
}
