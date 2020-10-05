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

namespace Tests.Cluster.VotingConfigExclusions.DeleteVotingConfigExclusions
{
	[SkipVersion("<7.8.0", "Introduced in 7.8.0")]
	public class DeleteVotingConfigExclusionsApiTests : ApiIntegrationTestBase<WritableCluster, DeleteVotingConfigExclusionsResponse, IDeleteVotingConfigExclusionsRequest, DeleteVotingConfigExclusionsDescriptor, DeleteVotingConfigExclusionsRequest>
	{
		public DeleteVotingConfigExclusionsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteVotingConfigExclusionsDescriptor, IDeleteVotingConfigExclusionsRequest> Fluent => s => s
			.WaitForRemoval();

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteVotingConfigExclusionsRequest Initializer => new DeleteVotingConfigExclusionsRequest { WaitForRemoval = true };
		protected override string UrlPath => $"/_cluster/voting_config_exclusions?wait_for_removal=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.DeleteVotingConfigExclusions(f),
			(client, f) => client.Cluster.DeleteVotingConfigExclusionsAsync(f),
			(client, r) => client.Cluster.DeleteVotingConfigExclusions(r),
			(client, r) => client.Cluster.DeleteVotingConfigExclusionsAsync(r)
		);

		protected override void ExpectResponse(DeleteVotingConfigExclusionsResponse response) => response.ShouldBeValid();
	}
}
