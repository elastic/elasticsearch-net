// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.Aggregations;
using System.Collections.Generic;
using System;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using Tests.Core.Extensions;

namespace Tests.Aggregations.Pipeline;

public class BucketSortAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public BucketSortAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
				commits_bucket_sort = new
				{
					bucket_sort = new
					{
						sort = new { commits = new { order = "desc" } },
						from = 0,
						size = 3,
						gap_policy = "insert_zeros"
					}
				}
			}
		}
	};

#pragma warning disable 618, 612
	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.DateHistogram("projects_started_per_month", dh => dh
			.Field(p => p.StartedOn)
			.CalendarInterval(CalendarInterval.Month)
			.Aggregations(aa => aa
				.Sum("commits", sm => sm
					.Field(p => p.NumberOfCommits)
				)
				.BucketSort("commits_bucket_sort", bs => bs
					.Sort(s => s
						.Field("commits", f => f.Order(SortOrder.Desc))
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
			CalendarInterval = CalendarInterval.Month,
			Aggregations =
				new SumAggregation("commits", "numberOfCommits") &&
				new BucketSortAggregation("commits_bucket_sort")
				{
					Sort = new List<SortOptions>
					{
						SortOptions.Field("commits", new FieldSort { Order = SortOrder.Desc })
					},
					From = 0,
					Size = 3,
					GapPolicy = GapPolicy.InsertZeros
				}
		};
#pragma warning restore 618, 612

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();

		var projectsPerMonth = response.Aggregations.GetDateHistogram("projects_started_per_month");
		projectsPerMonth.Should().NotBeNull();
		projectsPerMonth.Buckets.Should().NotBeNull();
		projectsPerMonth.Buckets.Count.Should().Be(3);

		double previousCommits = -1;

		// sum of commits should descend over buckets
		foreach (var item in projectsPerMonth.Buckets)
		{
			var value = item.GetSum("commits").Value;
			if (value == null)
				continue;

			var numberOfCommits = value.Value;
			if (Math.Abs(previousCommits - (-1)) > double.Epsilon)
				numberOfCommits.Should().BeLessOrEqualTo(previousCommits);

			previousCommits = numberOfCommits;
		}
	}
}
