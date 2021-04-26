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
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Bucket.Children
{
	/**
	 * A special single bucket aggregation that enables aggregating from buckets on parent document types to
	 * buckets on child documents.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-children-aggregation.html[Children Aggregation]
	 */
	public class ChildrenAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public ChildrenAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			name_of_child_agg = new
			{
				children = new { type = "commits" },
				aggs = new
				{
					average_per_child = new
					{
						avg = new { field = "confidenceFactor" }
					},
					max_per_child = new
					{
						max = new { field = "confidenceFactor" }
					},
					min_per_child = new
					{
						min = new { field = "confidenceFactor" }
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Children<CommitActivity>("name_of_child_agg", child => child
				.Aggregations(childAggs => childAggs
					.Average("average_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					.Max("max_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					.Min("min_per_child", avg => avg.Field(p => p.ConfidenceFactor))
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new ChildrenAggregation("name_of_child_agg", typeof(CommitActivity))
			{
				Aggregations =
					new AverageAggregation("average_per_child", "confidenceFactor")
					&& new MaxAggregation("max_per_child", "confidenceFactor")
					&& new MinAggregation("min_per_child", "confidenceFactor")
			};
	}
}
