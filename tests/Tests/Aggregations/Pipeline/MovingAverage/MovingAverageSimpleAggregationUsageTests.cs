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
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Pipeline.MovingAverage
{
	[SkipVersion("5.0.0-alpha1", "https://github.com/elastic/elasticsearch/issues/17516")]
	public class MovingAverageSimpleAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public MovingAverageSimpleAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
						moving_avg = new
						{
							buckets_path = "commits",
							model = "simple",
							window = 30,
							predict = 10,
							gap_policy = "insert_zeros",
							settings = new { }
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
					.MovingAverage("commits_moving_avg", mv => mv
						.BucketsPath("commits")
						.Window(30)
						.Predict(10)
						.GapPolicy(GapPolicy.InsertZeros)
						.Model(m => m
							.Simple()
						)
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
					&& new MovingAverageAggregation("commits_moving_avg", "commits")
					{
						Window = 30,
						Predict = 10,
						GapPolicy = GapPolicy.InsertZeros,
						Model = new SimpleModel()
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

			// average not calculated for the first bucket so movingAvg.Value is expected to be null there
			foreach (var item in projectsPerMonth.Buckets.Skip(1))
			{
				var movingAvg = item.Sum("commits_moving_avg");
				movingAvg.Should().NotBeNull();
				movingAvg.Value.Should().BeGreaterThan(0);
			}
		}
	}
}
