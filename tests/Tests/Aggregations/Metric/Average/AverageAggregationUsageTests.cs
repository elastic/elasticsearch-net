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
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Average
{
	public class AverageAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public AverageAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			average_commits = new
			{
				meta = new
				{
					foo = "bar"
				},
				avg = new
				{
					field = "numberOfCommits",
					missing = 10.0,
					script = new
					{
						source = "_value * 1.2",
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Average("average_commits", avg => avg
				.Meta(m => m
					.Add("foo", "bar")
				)
				.Field(p => p.NumberOfCommits)
				.Missing(10)
				.Script(ss => ss.Source("_value * 1.2"))
			);

		protected override AggregationDictionary InitializerAggs =>
			new AverageAggregation("average_commits", Field<Project>(p => p.NumberOfCommits))
			{
				Meta = new Dictionary<string, object>
				{
					{ "foo", "bar" }
				},
				Missing = 10,
				Script = new InlineScript("_value * 1.2")
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitsAvg = response.Aggregations.Average("average_commits");
			commitsAvg.Should().NotBeNull();
			commitsAvg.Value.Should().BeGreaterThan(0);
			commitsAvg.Meta.Should().NotBeNull().And.HaveCount(1);
			commitsAvg.Meta["foo"].Should().Be("bar");
		}
	}
}
