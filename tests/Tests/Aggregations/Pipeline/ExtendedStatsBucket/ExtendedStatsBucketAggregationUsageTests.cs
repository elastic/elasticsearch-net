// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Pipeline.ExtendedStatsBucket
{
	public class ExtendedStatsBucketAggregationUsageTests : AggregationUsageTestBase
	{
		public ExtendedStatsBucketAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
					}
				}
			},
			extended_stats_commits_per_month = new
			{
				extended_stats_bucket = new
				{
					buckets_path = "projects_started_per_month>commits",
					sigma = 2.0
				}
			}
		};

#pragma warning disable 618, 612
		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("projects_started_per_month", dh => dh
				.Field(p => p.StartedOn)
				.Interval(DateInterval.Month)
				.Aggregations(aa => aa
					.Sum("commits", sm => sm
						.Field(p => p.NumberOfCommits)
					)
				)
			)
			.ExtendedStatsBucket("extended_stats_commits_per_month", aaa => aaa
				.BucketsPath("projects_started_per_month>commits")
				.Sigma(2.0)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				Aggregations = new SumAggregation("commits", "numberOfCommits")
			}
			&& new ExtendedStatsBucketAggregation("extended_stats_commits_per_month", "projects_started_per_month>commits")
			{
				Sigma = 2.0
			};
#pragma warning restore 618, 612

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projectsPerMonth = response.Aggregations.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			var commitsStats = response.Aggregations.ExtendedStatsBucket("extended_stats_commits_per_month");
			commitsStats.Should().NotBeNull();
			commitsStats.Average.Should().BeGreaterThan(0);
			commitsStats.Max.Should().BeGreaterThan(0);
			commitsStats.Min.Should().BeGreaterThan(0);
			commitsStats.Count.Should().BeGreaterThan(0);
			commitsStats.Sum.Should().BeGreaterThan(0);
			commitsStats.SumOfSquares.Should().BeGreaterThan(0);
			commitsStats.StdDeviation.Should().BeGreaterThan(0);
			commitsStats.StdDeviationBounds.Should().NotBeNull();
			commitsStats.StdDeviationBounds.Upper.Should().BeGreaterThan(0);
			commitsStats.StdDeviationBounds.Lower.Should().NotBe(0);
		}
	}
}
