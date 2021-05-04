// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.VotingConfigExclusions.DeleteVotingConfigExclusions
{
	public class DeleteingVotingConfigExclusionsTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await DELETE("/_cluster/voting_config_exclusions")
				.Fluent(c => c.Cluster.DeleteVotingConfigExclusions())
				.Request(c => c.Cluster.DeleteVotingConfigExclusions(new DeleteVotingConfigExclusionsRequest()))
				.FluentAsync(c => c.Cluster.DeleteVotingConfigExclusionsAsync())
				.RequestAsync(c => c.Cluster.DeleteVotingConfigExclusionsAsync(new DeleteVotingConfigExclusionsRequest()));
	}
}
