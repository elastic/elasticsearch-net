// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Pipeline.MovingFunction
{
	/**
	 * Given an ordered series of data, the Moving Function aggregation will slide a window across the data and allow
	 * the user to specify a custom script that is executed on each window of data. For convenience, a number of
	 * common functions are predefined such as min/max, moving averages, etc.
	 *
     * This is conceptually very similar to the Moving Average pipeline aggregation, except it provides more functionality.
	 *
	 * NOTE: Only available in Elasticsearch 6.4.0+
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-pipeline-movfn-aggregation.html[Moving Function Aggregation]
	 */
	[SkipVersion("<7.4.0", "Shift option introduced in 7.4.0+")]
	public class MovingFunctionAggregationUsageTests : AggregationUsageTestBase
	{
		public MovingFunctionAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object AggregationJson => new
		{
			projects_started_per_month = new
			{
				date_histogram = new
				{
					field = "startedOn",
					interval = "month",
					min_doc_count = 0
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
						moving_fn = new
						{
							buckets_path = "commits",
							window = 30,
							shift = 0,
							script = "MovingFunctions.unweightedAvg(values)"
						}
					}
				}
			}
		};

#pragma warning disable 618, 612
		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("projects_started_per_month", dh => dh
				.Field(p => p.StartedOn)
				.Interval(DateInterval.Month)
				.MinimumDocumentCount(0)
				.Aggregations(aa => aa
					.Sum("commits", sm => sm
						.Field(p => p.NumberOfCommits)
					)
					.MovingFunction("commits_moving_avg", mv => mv
						.BucketsPath("commits")
						.Window(30)
						.Shift(0)
						.Script("MovingFunctions.unweightedAvg(values)")
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				MinimumDocumentCount = 0,
				Aggregations =
					new SumAggregation("commits", "numberOfCommits")
					&& new MovingFunctionAggregation("commits_moving_avg", "commits")
					{
						Window = 30,
						Shift = 0,
						Script = "MovingFunctions.unweightedAvg(values)"
					}
			};
#pragma warning restore 618, 612

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projectsPerMonth = response.Aggregations.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			// average not calculated for the first bucket
			foreach (var item in projectsPerMonth.Buckets.Skip(1))
			{
				var movingAvg = item.Sum("commits_moving_avg");
				movingAvg.Should().NotBeNull();
				movingAvg.Value.Should().BeGreaterThan(0);
			}
		}
	}
}
