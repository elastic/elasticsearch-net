/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
	public class CumulativeCardinalityAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
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
