using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.MovingAverage
{
	[SkipVersion("5.0.0-alpha1", "https://github.com/elastic/elasticsearch/issues/17516")]
	public class MovingAverageHoltWintersUsageTests : AggregationUsageTestBase
	{
		public MovingAverageHoltWintersUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
						commits_moving_avg = new
						{
							moving_avg = new
							{
								buckets_path = "commits",
								window = 60,
								model = "holt_winters",
								settings = new
								{
									type = "mult",
									alpha = 0.5,
									beta = 0.5,
									gamma = 0.5,
									period = 30,
									pad = false
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
						.MovingAverage("commits_moving_avg", mv => mv
							.BucketsPath("commits")
							.Window(60)
							.Model(m => m
								.HoltWinters(hw => hw
									.Type(HoltWintersType.Multiplicative)
									.Alpha(0.5f)
									.Beta(0.5f)
									.Gamma(0.5f)
									.Period(30)
									.Pad(false)
								)
							)
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
					new MovingAverageAggregation("commits_moving_avg", "commits")
					{
						Window = 60,
						Model = new HoltWintersModel
						{
							Type = HoltWintersType.Multiplicative,
							Alpha = 0.5f,
							Beta = 0.5f,
							Gamma = 0.5f,
							Period = 30,
							Pad = false
						}
					}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
		}
	}
}
