// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.Extensions;
using Xunit;

namespace Tests.Aggregations.Pipeline;

public class CumulativeSumAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public CumulativeSumAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override object AggregationJson => new
	{
		projects_started_per_month = new
		{
			date_histogram = new
			{
				field = "startedOn",
				calendar_interval = "month",
			},
			aggregations = new
			{
				commits = new
				{
					sum = new
					{
						field = "numberOfCommits"
					}
				},
				cumulative_commits = new
				{
					cumulative_sum = new
					{
						buckets_path = "commits"
					}
				}
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.DateHistogram("projects_started_per_month", dh => dh
			.Field(p => p.StartedOn)
			.CalendarInterval(CalendarInterval.Month)
			.Aggregations(aa => aa
				.Sum("commits", sm => sm
					.Field(p => p.NumberOfCommits)
				)
				.CumulativeSum("cumulative_commits", d => d
					.BucketsPath("commits")
				)
			)
		);

	protected override AggregationDictionary InitializerAggs =>
		new DateHistogramAggregation("projects_started_per_month")
		{
			Field = "startedOn",
			CalendarInterval = CalendarInterval.Month,
			Aggregations =
				new SumAggregation("commits", "numberOfCommits") &&
				new CumulativeSumAggregation("cumulative_commits") { BucketsPath = "commits" }
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();

		var projectsPerMonth = response.Aggregations.GetDateHistogram("projects_started_per_month");
		projectsPerMonth.Should().NotBeNull();
		projectsPerMonth.Buckets.Should().NotBeNull();
		projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

		foreach (var item in projectsPerMonth.Buckets)
		{
			item.TryGetValue("cumulative_commits", out var cumulative).Should().BeTrue();

			if (cumulative is not SimpleValueAggregate simpleValue)
			{
				Assert.Fail("Expected cumulative_commits to be SimpleValueAggregate");
				return;
			}

			simpleValue.Should().NotBeNull();
			simpleValue.Value.Should().NotBe(null);
		}
	}
}
