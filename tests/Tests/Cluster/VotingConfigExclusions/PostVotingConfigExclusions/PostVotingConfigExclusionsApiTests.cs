// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
