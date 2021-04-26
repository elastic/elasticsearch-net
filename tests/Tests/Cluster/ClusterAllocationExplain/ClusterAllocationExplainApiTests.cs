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
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ClusterAllocationExplain
{
	public class ClusterAllocationExplainApiTests
		: ApiIntegrationTestBase<UnbalancedCluster, ClusterAllocationExplainResponse, IClusterAllocationExplainRequest,
			ClusterAllocationExplainDescriptor, ClusterAllocationExplainRequest>
	{
		public ClusterAllocationExplainApiTests(UnbalancedCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				index = "project",
				shard = 0,
				primary = true
			};

		protected override int ExpectStatusCode => 200;

		protected override Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> Fluent => s => s
			.Index<Project>()
			.Shard(0)
			.Primary()
			.IncludeYesDecisions();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClusterAllocationExplainRequest Initializer =>
			new ClusterAllocationExplainRequest
			{
				Index = typeof(Project),
				Shard = 0,
				Primary = true,
				IncludeYesDecisions = true
			};

		protected override string UrlPath => "/_cluster/allocation/explain?include_yes_decisions=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.AllocationExplain(f),
			(client, f) => client.Cluster.AllocationExplainAsync(f),
			(client, r) => client.Cluster.AllocationExplain(r),
			(client, r) => client.Cluster.AllocationExplainAsync(r)
		);

		protected override void ExpectResponse(ClusterAllocationExplainResponse response)
		{
			response.Primary.Should().BeTrue();
			response.Shard.Should().Be(0);
			response.Index.Should().NotBeNullOrEmpty();
			response.CurrentState.Should().NotBeNullOrEmpty();
			response.CurrentNode.Should().NotBeNull();
			response.CanRemainOnCurrentNode.Should().NotBeNull();
			response.CanRebalanceCluster.Should().NotBeNull();
			response.CanRebalanceClusterDecisions.Should().NotBeNullOrEmpty();

			foreach (var decision in response.CanRebalanceClusterDecisions)
			{
				decision.Decider.Should().NotBeNullOrEmpty();
				decision.Explanation.Should().NotBeNullOrEmpty();
			}

			response.CanRebalanceToOtherNode.Should().NotBeNull();
			response.RebalanceExplanation.Should().NotBeNullOrEmpty();
		}
	}
}
