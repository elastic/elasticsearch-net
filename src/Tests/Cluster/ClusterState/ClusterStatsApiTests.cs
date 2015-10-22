using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Cluster.ClusterState
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClusterStateApiTests : ApiIntegrationTestBase<IClusterStateResponse, IClusterStateRequest, ClusterStateDescriptor, ClusterStateRequest>
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

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.ClusterName.Should().NotBeNullOrWhiteSpace();
			r.MasterNode.Should().NotBeNullOrWhiteSpace();
			r.StateUUID.Should().NotBeNullOrWhiteSpace();
			r.Version.Should().BeGreaterThan(0);
		});

		[I] public async Task NodesResponse() => await this.AssertOnAllResponses(r =>
		{
			var nodes = r.Nodes;
			nodes.Should().NotBeEmpty().And.ContainKey(r.MasterNode);

			var node = nodes[r.MasterNode];

			node.Name.Should().NotBeNullOrWhiteSpace();
			node.TransportAddress.Should().NotBeNullOrWhiteSpace();
		});

		[I] public async Task MetadataResponse() => await this.AssertOnAllResponses(r =>
		{
			var meta = r.Metadata;
			meta.ClusterUUID.Should().NotBeNullOrWhiteSpace();
			meta.Templates.Should().NotBeEmpty().And.ContainKey("raw_fields");

			AssertMetadataTemplate(meta);
			AssertMetadataIndices(meta);
		});

		private void AssertMetadataIndices(MetadataState meta)
		{
			var i = this.Client.Infer.IndexName(Index<Project>());
			var t = this.Client.Infer.TypeName(Type<CommitActivity>());

			meta.Indices.Should().NotBeEmpty().And.ContainKey(i);

			var index = meta.Indices[i];
			index.Aliases.Should().NotBeEmpty().And.Contain("projects-alias");
			index.Mappings.Should().NotBeEmpty().And.ContainKey(t);

			var commitsMapping = index.Mappings[t];
			commitsMapping.ParentField.Should().NotBeNull();
			commitsMapping.ParentField.Type.Should().Be(i);
		}

		private static void AssertMetadataTemplate(MetadataState meta)
		{
			var rawFieldsTemplate = meta.Templates["raw_fields"];
			rawFieldsTemplate.Template.Should().NotBeNullOrWhiteSpace();
			rawFieldsTemplate.Settings.Should().NotBeNull();
			rawFieldsTemplate.Settings.NumberOfShards.Should().Be(2);

			var mapping = rawFieldsTemplate.Mappings["_default_"];
			mapping.Should().NotBeNull();
			mapping.DynamicTemplates.Should().NotBeEmpty().And.ContainKey("raw_fields");

			var rawFields = mapping.DynamicTemplates["raw_fields"];
			rawFields.MatchMappingType.Should().Be("string");
			rawFields.Match.Should().Be("*");
			rawFields.Mapping.Should().NotBeNull();
			rawFields.Mapping.Fields.Should().NotBeEmpty().And.ContainKey("raw");

			var rawField = rawFields.Mapping.Fields["raw"] as IStringProperty;
			rawField.Should().NotBeNull();
			rawField.Index.Should().Be(FieldIndexOption.NotAnalyzed);
		}

		[I] public async Task RoutingTableResponse() => await this.AssertOnAllResponses(r =>
		{
			var table = r.RoutingTable;
			table.Should().NotBeNull();

			table.Indices.Should().NotBeEmpty().And.ContainKey("project");
			var indices = table.Indices["project"];

			indices.Shards.Should().NotBeEmpty();
			var shards = indices.Shards.First().Value;
			shards.Should().NotBeEmpty();
			var shard = shards.First();
			shard.AllocationId.Should().NotBeNull();
			shard.AllocationId.Id.Should().NotBeNullOrWhiteSpace();
			shard.Index.Should().NotBeNullOrWhiteSpace();
			shard.Node.Should().NotBeNullOrWhiteSpace();
			shard.State.Should().NotBeNullOrWhiteSpace();
			shard.Version.Should().BeGreaterThan(0);
		});

		[I] public async Task RoutingNodesResponse() => await this.AssertOnAllResponses(r =>
		{
			var routing = r.RoutingNodes;
			routing.Should().NotBeNull();

			routing.Nodes.Should().NotBeEmpty().And.ContainKey(r.MasterNode);
			var nodes = routing.Nodes[r.MasterNode];

			nodes.Should().NotBeEmpty();
			var node = nodes.First();
			node.AllocationId.Should().NotBeNull();
			node.AllocationId.Id.Should().NotBeNullOrWhiteSpace();
			node.Index.Should().NotBeNullOrWhiteSpace();
			node.Node.Should().NotBeNullOrWhiteSpace();
			node.State.Should().NotBeNullOrWhiteSpace();
			node.Version.Should().BeGreaterThan(0);
		});
	}

}
