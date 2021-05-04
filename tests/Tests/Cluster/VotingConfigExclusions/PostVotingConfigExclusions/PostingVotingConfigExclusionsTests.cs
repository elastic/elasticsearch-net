// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.VotingConfigExclusions.PostVotingConfigExclusions
{
	public class PostingVotingConfigExclusionsTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/_cluster/voting_config_exclusions?node_names=node1%2Cnode2")
				.Fluent(c => c.Cluster.PostVotingConfigExclusions(f => f.NodeNames("node1,node2")))
				.Request(c => c.Cluster.PostVotingConfigExclusions(new PostVotingConfigExclusionsRequest{ NodeNames = "node1,node2" }))
				.FluentAsync(c => c.Cluster.PostVotingConfigExclusionsAsync(f => f.NodeNames("node1,node2")))
				.RequestAsync(c => c.Cluster.PostVotingConfigExclusionsAsync(new PostVotingConfigExclusionsRequest{ NodeNames = "node1,node2" }));
	}
}
