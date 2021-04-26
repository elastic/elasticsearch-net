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
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClusterHealthResponse, IClusterHealthRequest, ClusterHealthDescriptor, ClusterHealthRequest>
	{
		public ClusterHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/health";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.Health(),
			(client, f) => client.Cluster.HealthAsync(),
			(client, r) => client.Cluster.Health(r),
			(client, r) => client.Cluster.HealthAsync(r)
		);

		protected override void ExpectResponse(ClusterHealthResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Status.Should().NotBe(Health.Red);
			response.TimedOut.Should().BeFalse();
			response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			response.ActiveShards.Should().BeGreaterOrEqualTo(1);
		}
	}

	public class ClusterHealthShardsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClusterHealthResponse, IClusterHealthRequest, ClusterHealthDescriptor, ClusterHealthRequest>
	{
		public ClusterHealthShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ClusterHealthDescriptor, IClusterHealthRequest> Fluent => c => c.Level(Level.Shards);
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override ClusterHealthRequest Initializer => new ClusterHealthRequest { Level = Level.Shards };
		protected override string UrlPath => "/_cluster/health?level=shards";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.Health(null, f),
			(client, f) => client.Cluster.HealthAsync(null, f),
			(client, r) => client.Cluster.Health(r),
			(client, r) => client.Cluster.HealthAsync(r)
		);

		protected override void ExpectResponse(ClusterHealthResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Status.Should().NotBe(Health.Red);
			response.TimedOut.Should().BeFalse();
			response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			response.ActiveShards.Should().BeGreaterOrEqualTo(1);
			response.ActiveShardsPercentAsNumber.Should().BePositive();
			response.DelayedUnassignedShards.Should().Be(0);
			response.NumberOfInFlightFetch.Should().BeGreaterOrEqualTo(0);
			response.TaskMaxWaitTimeInQueueInMilliseconds.Should().BeGreaterOrEqualTo(0);

			response.Indices.Should()
				.NotBeEmpty()
				.And.ContainKey(Index<Developer>());

			var indexHealth = response.Indices[Index<Developer>()];
			indexHealth.ActivePrimaryShards.Should().BeGreaterThan(0);
			indexHealth.ActiveShards.Should().BeGreaterThan(0);
			indexHealth.Shards["0"].Status.Should().Be(Health.Green);
		}
	}
}
