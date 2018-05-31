using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Framework;

namespace Tests.XPack.DeprecationInfo
{
	public class NodesUsageUnitTests
	{
		[U]
		public void ShouldDeserialize()
		{
			const string nodeId = "pQHNt5rXTTWNvUgOrdynKg";
			var fixedResponse = new
			{
				_nodes = new {
				total = 1,
				successful = 1,
				failed = 0
			},
			cluster_name = "my_cluster",
			nodes = new Dictionary<string, object>{{
				nodeId, new {
					timestamp = 1492553961812,
					since = 1492553906606,
					rest_actions = new Dictionary<string, object>
					{
						{ "org.elasticsearch.rest.action.admin.cluster.RestNodesUsageAction", 1},
						{ "org.elasticsearch.rest.action.admin.indices.RestCreateIndexAction", 1},
						{ "org.elasticsearch.rest.action.document.RestGetAction", 1},
						{ "org.elasticsearch.rest.action.search.RestSearchAction", 19},
						{ "org.elasticsearch.rest.action.admin.cluster.RestNodesInfoAction", 36}
					}
				}
				}
			}};

			var client = TestClient.GetFixedReturnClient(fixedResponse);

			//warmup
			var response = client.NodesUsage();
			response.ShouldBeValid();

			response.ClusterName.Should().Be("my_cluster");

			response.NodeStatistics.Should().NotBeNull();
			response.NodeStatistics.Total.Should().Be(1);
			response.NodeStatistics.Successful.Should().Be(1);
			response.NodeStatistics.Failed.Should().Be(0);

			response.Nodes.Should().NotBeNull();
			response.Nodes.Should().HaveCount(1);

			response.Nodes.Should().ContainKey(nodeId);

			var node = response.Nodes[nodeId];
			node.Timestamp.Should().Be(new DateTimeOffset(2017, 4, 18, 22, 19, 21, 812, TimeSpan.Zero));
			node.Since.Should().Be(new DateTimeOffset(2017, 4, 18, 22, 18, 26, 606, TimeSpan.Zero));
			node.RestActions.Should().NotBeNull();
			node.RestActions.Should().HaveCount(5);
			node.RestActions.Should().ContainKey("org.elasticsearch.rest.action.search.RestSearchAction");
			node.RestActions["org.elasticsearch.rest.action.search.RestSearchAction"].Should().Be(19);
		}
	}
}
