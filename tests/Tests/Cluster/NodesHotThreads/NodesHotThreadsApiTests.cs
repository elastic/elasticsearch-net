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

using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.NodesHotThreads
{
	public class NodesHotThreadsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, NodesHotThreadsResponse, INodesHotThreadsRequest, NodesHotThreadsDescriptor, NodesHotThreadsRequest
		>
	{
		public NodesHotThreadsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes/hot_threads";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Nodes.HotThreads(),
			(client, f) => client.Nodes.HotThreadsAsync(),
			(client, r) => client.Nodes.HotThreads(r),
			(client, r) => client.Nodes.HotThreadsAsync(r)
		);

		protected override void ExpectResponse(NodesHotThreadsResponse response)
		{
			response.HotThreads.Should().NotBeEmpty();
			var t = response.HotThreads.First();
			t.NodeId.Should().NotBeNullOrWhiteSpace();
			t.NodeName.Should().NotBeNullOrWhiteSpace();
			t.Hosts.Should().NotBeEmpty();
		}
	}
}
