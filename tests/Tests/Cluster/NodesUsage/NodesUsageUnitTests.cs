// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Client;
using Tests.Core.Extensions;

namespace Tests.Cluster.NodesUsage
{
	public class NodesUsageUnitTests
	{
		[U]
		public void ShouldDeserialize()
		{
			const string nodeId = "pQHNt5rXTTWNvUgOrdynKg";
			var fixedResponse = new
			{
				_nodes = new
				{
					total = 1,
					successful = 1,
					failed = 0,
					failures = new[]
					{
						new
						{
							type = "illegal_argument_exception",
							reason = "failed to execute script",
							caused_by = new
							{
								type = "script_exception",
								reason = "failed to run inline script [use(java.lang.Exception) {throw new Exception(\"Customized Exception\")}] using lang [groovy]",
								caused_by = new
								{
									type = "privileged_action_exception",
									reason = (string)null,
									caused_by = new
									{
										type="exception",
										reason= "Custom Exception"
									}
								}
							}
						}
					}
				},
				cluster_name = "my_cluster",
				nodes = new Dictionary<string, object>
				{
					{
						nodeId, new
						{
							timestamp = 1492553961812,
							since = 1492553906606,
							rest_actions = new Dictionary<string, object>
							{
								{ "org.elasticsearch.rest.action.admin.cluster.RestNodesUsageAction", 1 },
								{ "org.elasticsearch.rest.action.admin.indices.RestCreateIndexAction", 1 },
								{ "org.elasticsearch.rest.action.document.RestGetAction", 1 },
								{ "org.elasticsearch.rest.action.search.RestSearchAction", 19 },
								{ "org.elasticsearch.rest.action.admin.cluster.RestNodesInfoAction", 36 }
							}
						}
					}
				}
			};

			var client = FixedResponseClient.Create(fixedResponse);

			//warmup
			var response = client.Nodes.Usage();
			response.ShouldBeValid();

			response.ClusterName.Should().Be("my_cluster");

			response.NodeStatistics.Should().NotBeNull();
			response.NodeStatistics.Total.Should().Be(1);
			response.NodeStatistics.Successful.Should().Be(1);
			response.NodeStatistics.Failed.Should().Be(0);
			response.NodeStatistics.Failures.Should().HaveCount(1);
			var failure = response.NodeStatistics.Failures.First();
			failure.Type.Should().NotBeNull();
			failure.Reason.Should().NotBeNull();
			failure.CausedBy.Should().NotBeNull();

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
