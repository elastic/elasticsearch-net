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

namespace Tests.Aggregations.Pipeline.SerialDifferencing
{
	public class SerialDifferencingAggregationUsageTests : AggregationUsageTestBase
	{
		public SerialDifferencingAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
					second_difference = new
					{
						serial_diff = new
						{
							buckets_path = "commits",
							lag = 2
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
					.SerialDifferencing("second_difference", d => d
						.BucketsPath("commits")
						.Lag(2)
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
					&& new SerialDifferencingAggregation("second_difference", "commits")
					{
						Lag = 2
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

			var differenceCount = 0;

			foreach (var item in projectsPerMonth.Buckets)
			{
				differenceCount++;
				var commits = item.Sum("commits");
				commits.Should().NotBeNull();
				commits.Value.Should().NotBe(null);

				var secondDifference = item.SerialDifferencing("second_difference");

				// serial differencing specified a lag of 2, so
				// only expect values from the 3rd bucket onwards
				if (differenceCount <= 2)
					secondDifference.Should().BeNull();
				else
				{
					secondDifference.Should().NotBeNull();
					secondDifference.Value.Should().NotBe(null);
				}
			}
		}
	}
}
