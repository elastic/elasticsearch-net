using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Cluster.ClusterState
{
	public class ClusterStateApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IClusterStateResponse, IClusterStateRequest, ClusterStateDescriptor, ClusterStateRequest>
	{
		public ClusterStateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterState(),
			fluentAsync: (client, f) => client.ClusterStateAsync(),
			request: (client, r) => client.ClusterState(r),
			requestAsync: (client, r) => client.ClusterStateAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/state";

		protected override void ExpectResponse(IClusterStateResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.MasterNode.Should().NotBeNullOrWhiteSpace();
			response.StateUUID.Should().NotBeNullOrWhiteSpace();
			response.Version.Should().BeGreaterThan(0);
			Assert(response.Nodes, response.MasterNode);
			Assert(response.Metadata);
			Assert(response.RoutingTable);
			Assert(response.RoutingNodes, response.MasterNode);
		}

		private void Assert(IReadOnlyDictionary<string, NodeState> nodes, string master)
		{
			nodes.Should().NotBeEmpty().And.ContainKey(master);
			var node = nodes[master];
			node.Name.Should().NotBeNullOrWhiteSpace();
			node.TransportAddress.Should().NotBeNullOrWhiteSpace();
		}

		private void Assert(MetadataState meta)
		{
			meta.ClusterUUID.Should().NotBeNullOrWhiteSpace();
			meta.Templates.Should().NotBeEmpty().And.ContainKey("nest_tests");

			var rawFieldsTemplate = meta.Templates["nest_tests"];
			rawFieldsTemplate.IndexPatterns.Should().NotBeNullOrEmpty();
			rawFieldsTemplate.Settings.Should().NotBeNull();
			rawFieldsTemplate.Settings.NumberOfShards.Should().Be(2);

			var i = this.Client.Infer.IndexName(Index<Project>());

			meta.Indices.Should().NotBeEmpty().And.ContainKey(i);

			var index = meta.Indices[i];
			index.Aliases.Should().NotBeEmpty().And.Contain(DefaultSeeder.ProjectsAliasName);

		}

		protected void Assert(RoutingTableState routingTable)
		{
			routingTable.Should().NotBeNull();

			routingTable.Indices.Should().NotBeEmpty().And.ContainKey("project");
			var indices = routingTable.Indices["project"];

			indices.Shards.Should().NotBeEmpty();
			var shards = indices.Shards.First().Value;
			shards.Should().NotBeEmpty();
			var shard = shards.First();
			shard.AllocationId.Should().NotBeNull();
			shard.AllocationId.Id.Should().NotBeNullOrWhiteSpace();
			shard.Index.Should().NotBeNullOrWhiteSpace();
			shard.Node.Should().NotBeNullOrWhiteSpace();
			shard.State.Should().NotBeNullOrWhiteSpace();
		}

		protected void Assert(RoutingNodesState routingNodes, string master)
		{
			routingNodes.Should().NotBeNull();

			routingNodes.Nodes.Should().NotBeEmpty().And.ContainKey(master);
			var nodes = routingNodes.Nodes[master];

			nodes.Should().NotBeEmpty();
			var node = nodes.First();
			node.AllocationId.Should().NotBeNull();
			node.AllocationId.Id.Should().NotBeNullOrWhiteSpace();
			node.Index.Should().NotBeNullOrWhiteSpace();
			node.Node.Should().NotBeNullOrWhiteSpace();
			node.State.Should().NotBeNullOrWhiteSpace();
		}
	}
}
