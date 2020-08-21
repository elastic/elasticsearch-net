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

namespace Tests.Aggregations.Pipeline.MaxBucket
{
	public class MaxBucketAggregationUsageTests : AggregationUsageTestBase
	{
		public MaxBucketAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
			max_commits_per_month = new
			{
				max_bucket = new
				{
					buckets_path = "projects_started_per_month>commits"
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
			.MaxBucket("max_commits_per_month", aaa => aaa
				.BucketsPath("projects_started_per_month>commits")
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				Interval = DateInterval.Month,
				Aggregations = new SumAggregation("commits", "numberOfCommits")
			}
			&& new MaxBucketAggregation("max_commits_per_month", "projects_started_per_month>commits");
#pragma warning restore 618, 612

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projectsPerMonth = response.Aggregations.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			var maxCommits = response.Aggregations.MaxBucket("max_commits_per_month");
			maxCommits.Should().NotBeNull();
			maxCommits.Value.Should().BeGreaterThan(0);
			maxCommits.Keys.Should().NotBeNull();
			maxCommits.Keys.Count.Should().BeGreaterOrEqualTo(1);
			foreach (var key in maxCommits.Keys)
				key.Should().NotBeNull();
		}
	}
}
