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
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Stats
{
	public class StatsAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public StatsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commit_stats = new
			{
				stats = new
				{
					field = "numberOfCommits"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Stats("commit_stats", st => st
				.Field(p => p.NumberOfCommits)
			);

		protected override AggregationDictionary InitializerAggs =>
			new StatsAggregation("commit_stats", Field<Project>(p => p.NumberOfCommits));

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitStats = response.Aggregations.Stats("commit_stats");
			commitStats.Should().NotBeNull();
			commitStats.Average.Should().BeGreaterThan(0);
			commitStats.Max.Should().BeGreaterThan(0);
			commitStats.Min.Should().BeGreaterThan(0);
			commitStats.Count.Should().BeGreaterThan(0);
			commitStats.Sum.Should().BeGreaterThan(0);
		}
	}
}
