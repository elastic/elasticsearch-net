using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Pipeline.BucketScript
{
	public class BucketScriptAggregationUsageTests : AggregationUsageTestBase
	{
		public BucketScriptAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
					stable_state = new
					{
						filter = new
						{
							term = new
							{
								state = new
								{
									value = "Stable"
								}
							}
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
					stable_percentage = new
					{
						bucket_script = new
						{
							buckets_path = new
							{
								totalCommits = "commits",
								stableCommits = "stable_state>commits"
							},
							script = new
							{
								source = "params.stableCommits / params.totalCommits * 100",
							}
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
					.Filter("stable_state", f => f
						.Filter(ff => ff
							.Term(p => p.State, "Stable")
						)
						.Aggregations(aaa => aaa
							.Sum("commits", sm => sm
								.Field(p => p.NumberOfCommits)
							)
						)
					)
					.BucketScript("stable_percentage", bs => bs
						.BucketsPath(bp => bp
							.Add("totalCommits", "commits")
							.Add("stableCommits", "stable_state>commits")
						)
						.Script(ss => ss.Source("params.stableCommits / params.totalCommits * 100"))
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
					new FilterAggregation("stable_state")
					{
						Filter = new TermQuery
						{
							Field = "state",
							Value = "Stable"
						},
						Aggregations = new SumAggregation("commits", "numberOfCommits")
					}
					&& new BucketScriptAggregation("stable_percentage", new MultiBucketsPath
					{
						{"totalCommits", "commits"},
						{"stableCommits", "stable_state>commits"}
					})
					{
						Script = new InlineScript("params.stableCommits / params.totalCommits * 100")
					}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projectsPerMonth = response.Aggregations.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			foreach(var item in projectsPerMonth.Buckets)
			{
				var stablePercentage = item.BucketScript("stable_percentage");
				stablePercentage.Should().NotBeNull();
				stablePercentage.Value.Should().HaveValue();
			}
		}
	}
}
