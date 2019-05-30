using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

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

			var i = Client.Infer.IndexName(Index<Project>());

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

	[SkipVersion("<6.5.0", "Validated against 6.5.0")]
	public class ClusterStateStoredScriptApiTests
		: ApiIntegrationTestBase<WritableCluster, ClusterStateResponse, IClusterStateRequest, ClusterStateDescriptor, ClusterStateRequest>
	{
		public ClusterStateStoredScriptApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			client.PutScript("my-script-id", s => s.Painless("return 0"));
			client.PutScript("my-other-script-id", s => s.Painless("return 1"));
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/state/metadata";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.State(null, s => s.Metric(ClusterStateMetric.Metadata)),
			(client, f) => client.Cluster.StateAsync(null, s => s.Metric(ClusterStateMetric.Metadata)),
			(client, r) => client.Cluster.State(new ClusterStateRequest(ClusterStateMetric.Metadata)),
			(client, r) => client.Cluster.StateAsync(new ClusterStateRequest(ClusterStateMetric.Metadata))
		);

		protected override void ExpectResponse(ClusterStateResponse response)
		{
			response.Metadata.Should().NotBeNull();
			response.Metadata.StoredScripts.Should().NotBeNull();
			response.Metadata.StoredScripts.Count.Should().Be(2);

			response.Metadata.StoredScripts["my-script-id"].Language.Should().Be("painless");
			response.Metadata.StoredScripts["my-script-id"].Source.Should().Be("return 0");
			response.Metadata.StoredScripts["my-script-id"].Options.Should().BeEmpty();

			response.Metadata.StoredScripts["my-other-script-id"].Language.Should().Be("painless");
			response.Metadata.StoredScripts["my-other-script-id"].Source.Should().Be("return 1");
			response.Metadata.StoredScripts["my-other-script-id"].Options.Should().BeEmpty();
		}
	}
}
