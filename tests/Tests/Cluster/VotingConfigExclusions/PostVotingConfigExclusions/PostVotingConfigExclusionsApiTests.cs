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
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.VotingConfigExclusions.PostVotingConfigExclusions
{
	[SkipVersion("<7.8.0", "Introduced in 7.8.0")]
	public class PostVotingConfigExclusionsApiTests : ApiIntegrationTestBase<WritableCluster, PostVotingConfigExclusionsResponse, IPostVotingConfigExclusionsRequest, PostVotingConfigExclusionsDescriptor, PostVotingConfigExclusionsRequest>
	{
		public PostVotingConfigExclusionsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<PostVotingConfigExclusionsDescriptor, IPostVotingConfigExclusionsRequest> Fluent => s => s
			.NodeNames("node1,node2");
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override PostVotingConfigExclusionsRequest Initializer => new PostVotingConfigExclusionsRequest { NodeNames = "node1,node2" };
		protected override string UrlPath => $"/_cluster/voting_config_exclusions?node_names=node1%2Cnode2";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.PostVotingConfigExclusions(f),
			(client, f) => client.Cluster.PostVotingConfigExclusionsAsync(f),
			(client, r) => client.Cluster.PostVotingConfigExclusions(r),
			(client, r) => client.Cluster.PostVotingConfigExclusionsAsync(r)
		);

		protected override void ExpectResponse(PostVotingConfigExclusionsResponse response) => response.ShouldBeValid();
	}
}
