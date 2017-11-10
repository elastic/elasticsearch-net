using System;
using FluentAssertions;
using Tests.Framework;

namespace Tests.XPack.DeprecationInfo
{
	public class NodesUsageUnitTests
	{
		[U]
		public void ShouldDeserialize()
		{
			const string fixedResponse = "{\"_nodes\": {\"total\": 1,\"successful\": 1,\"failed\": 0},\"cluster_name\": \"my_cluster\",\"nodes\": {\"pQHNt5rXTTWNvUgOrdynKg\": {\"timestamp\": 1492553961812,\"since\": 1492553906606,\"rest_actions\": {\"org.elasticsearch.rest.action.admin.cluster.RestNodesUsageAction\": 1,\"org.elasticsearch.rest.action.admin.indices.RestCreateIndexAction\": 1,\"org.elasticsearch.rest.action.document.RestGetAction\": 1,\"org.elasticsearch.rest.action.search.RestSearchAction\": 19,\"org.elasticsearch.rest.action.admin.cluster.RestNodesInfoAction\": 36}}}}";
			var client = TestClient.GetFixedReturnClient(fixedResponse);

			//warmup
			var response = client.NodesUsage();
			response.ShouldBeValid();

			response.ClusterName.Should().Be("my_cluster");

			response.NodeMetadata.Should().NotBeNull();
			response.NodeMetadata.Total.Should().Be(1);
			response.NodeMetadata.Successful.Should().Be(1);
			response.NodeMetadata.Failed.Should().Be(0);

			response.Nodes.Should().NotBeNull();
			response.Nodes.Should().HaveCount(1);

			const string nodeId = "pQHNt5rXTTWNvUgOrdynKg";

			response.Nodes.Should().ContainKey(nodeId);
			response.Nodes[nodeId].Timestamp.Should().Be(new DateTimeOffset(2017, 4, 18, 22, 19, 21, 812, TimeSpan.Zero));
			response.Nodes[nodeId].Since.Should().Be(new DateTimeOffset(2017, 4, 18, 22, 18, 26, 606, TimeSpan.Zero));
			response.Nodes[nodeId].RestActions.Should().NotBeNull();
			response.Nodes[nodeId].RestActions.Should().HaveCount(5);
			response.Nodes[nodeId].RestActions.Should().ContainKey("org.elasticsearch.rest.action.search.RestSearchAction");
			response.Nodes[nodeId].RestActions["org.elasticsearch.rest.action.search.RestSearchAction"].Should().Be(19);
		}
	}
}
