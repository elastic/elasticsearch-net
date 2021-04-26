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
using static Nest.Infer;

namespace Tests.Aggregations.Metric.StringStats
{
	[SkipVersion("<7.6.0", "Available in 7.6.0 with at least basic license level")]
	public class StringStatsAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public StringStatsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			name_stats = new
			{
				string_stats = new
				{
					field = "name"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.StringStats("name_stats", st => st
				.Field(p => p.Name)
			);

		protected override AggregationDictionary InitializerAggs =>
			new StringStatsAggregation("name_stats", Field<Project>(p => p.Name));

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitStats = response.Aggregations.StringStats("name_stats");
			commitStats.Should().NotBeNull();
			commitStats.AverageLength.Should().BeGreaterThan(0);
			commitStats.MaxLength.Should().BeGreaterThan(0);
			commitStats.MinLength.Should().BeGreaterThan(0);
			commitStats.Count.Should().BeGreaterThan(0);
			commitStats.Distribution.Should().NotBeNull().And.BeEmpty();
		}
	}

	// hide
	[SkipVersion("<7.6.0", "Available in 7.6.0 with at least basic license level")]
	public class StringStatsWithDistributionAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public StringStatsWithDistributionAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			name_stats = new
			{
				string_stats = new
				{
					field = "name",
					show_distribution = true
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.StringStats("name_stats", st => st
				.Field(p => p.Name)
				.ShowDistribution()
			);

		protected override AggregationDictionary InitializerAggs =>
			new StringStatsAggregation("name_stats", Field<Project>(p => p.Name))
			{
				ShowDistribution = true
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitStats = response.Aggregations.StringStats("name_stats");
			commitStats.Should().NotBeNull();
			commitStats.AverageLength.Should().BeGreaterThan(0);
			commitStats.MaxLength.Should().BeGreaterThan(0);
			commitStats.MinLength.Should().BeGreaterThan(0);
			commitStats.Count.Should().BeGreaterThan(0);
			commitStats.Distribution.Should().NotBeNull().And.NotBeEmpty();
		}
	}
}
