// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Pipeline.CumulativeCardinality
{
	[SkipVersion("<7.4.0", "Introduced in 7.4")]
	public class CumulativeCardinalityAggregationUsageTests : AggregationUsageTestBase
	{
		public CumulativeCardinalityAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object AggregationJson => new
		{
			projects_started_per_month = new
			{
				date_histogram = new
				{
					field = "startedOn",
					calendar_interval = "month",
				},
				aggs = new
				{
					commits = new
					{
						cardinality = new
						{
							field = "numberOfCommits"
						}
					},
					cumulative_commits = new
					{
						cumulative_cardinality = new
						{
							buckets_path = "commits"
						}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("projects_started_per_month", dh => dh
				.Field(p => p.StartedOn)
				.CalendarInterval(DateInterval.Month)
				.Aggregations(aa => aa
					.Cardinality("commits", sm => sm
						.Field(p => p.NumberOfCommits)
					)
					.CumulativeCardinality("cumulative_commits", d => d
						.BucketsPath("commits")
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("projects_started_per_month")
			{
				Field = "startedOn",
				CalendarInterval = DateInterval.Month,
				Aggregations =
					new CardinalityAggregation("commits", "numberOfCommits") &&
					new CumulativeCardinalityAggregation("cumulative_commits", "commits")
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projectsPerMonth = response.Aggregations.DateHistogram("projects_started_per_month");
			projectsPerMonth.Should().NotBeNull();
			projectsPerMonth.Buckets.Should().NotBeNull();
			projectsPerMonth.Buckets.Count.Should().BeGreaterThan(0);

			foreach (var item in projectsPerMonth.Buckets)
			{
				var commitsDerivative = item.CumulativeCardinality("cumulative_commits");
				commitsDerivative.Should().NotBeNull();
				commitsDerivative.Value.Should().NotBe(null);
			}
		}
	}
}
