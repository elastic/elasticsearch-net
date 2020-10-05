// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ClusterState
{
	public class ClusterStateApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClusterStateResponse, IClusterStateRequest, ClusterStateDescriptor, ClusterStateRequest>
	{
		public ClusterStateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/state";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.State(),
			(client, f) => client.Cluster.StateAsync(),
			(client, r) => client.Cluster.State(r),
			(client, r) => client.Cluster.StateAsync(r)
		);

		protected override void ExpectResponse(ClusterStateResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.MasterNode.Should().NotBeNullOrWhiteSpace();
			response.StateUUID.Should().NotBeNullOrWhiteSpace();
			response.Version.Should().BeGreaterThan(0);

			var masterNode = response.State["nodes"][response.MasterNode];
			var masterNodeName = masterNode["name"].Value as string;
			var transportAddress = masterNode["transport_address"].Value as string;
			masterNodeName.Should().NotBeNullOrWhiteSpace();
			transportAddress.Should().NotBeNullOrWhiteSpace();

			var getSyntax = response.Get<string>($"nodes.{response.MasterNode}.transport_address");

			getSyntax.Should().NotBeNullOrWhiteSpace().And.Be(transportAddress);

			var badPath = response.Get<string>($"this.is.not.a.path.into.the.response.structure");
			badPath.Should().BeNull();

			var dict = response.Get<DynamicDictionary>($"nodes");

			dict.Count.Should().BeGreaterThan(0);
			var node = dict[response.MasterNode].ToDictionary();
			node.Should().NotBeNull().And.ContainKey("name");

			object dictDoesNotExist = response.Get<DynamicDictionary>("nodes2");
			dictDoesNotExist.Should().BeNull();


			dynamic r = response.State;

			string lastCommittedConfig = r.metadata.cluster_coordination.last_committed_config[0];

			lastCommittedConfig.Should().NotBeNullOrWhiteSpace();




		}
	}
}
