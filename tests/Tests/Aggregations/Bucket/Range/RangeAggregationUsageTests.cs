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
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Range
{
	public class RangeAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public RangeAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commit_ranges = new
			{
				range = new
				{
					field = "numberOfCommits",
					ranges = new object[]
					{
						new { to = 100.0 },
						new { from = 100.0, to = 500.0 },
						new { from = 500.0 }
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Range("commit_ranges", ra => ra
				.Field(p => p.NumberOfCommits)
				.Ranges(
					r => r.To(100),
					r => r.From(100).To(500),
					r => r.From(500)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new RangeAggregation("commit_ranges")
			{
				Field = Field<Project>(p => p.NumberOfCommits),
				Ranges = new List<AggregationRange>
				{
					{ new AggregationRange { To = 100 } },
					{ new AggregationRange { From = 100, To = 500 } },
					{ new AggregationRange { From = 500 } }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitRanges = response.Aggregations.Range("commit_ranges");
			commitRanges.Should().NotBeNull();
			commitRanges.Buckets.Count.Should().Be(3);
			commitRanges.Buckets.FirstOrDefault(r => r.Key == "*-100.0").Should().NotBeNull();
			commitRanges.Buckets.FirstOrDefault(r => r.Key == "100.0-500.0").Should().NotBeNull();
			commitRanges.Buckets.FirstOrDefault(r => r.Key == "500.0-*").Should().NotBeNull();
		}
	}
}
