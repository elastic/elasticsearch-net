// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport.VirtualizedCluster.Products.Elasticsearch;

namespace Elastic.Transport.VirtualizedCluster
{
	public static class ElasticsearchVirtualCluster
	{
		public static VirtualCluster Nodes(int numberOfNodes, int startFrom = 9200) =>
			new VirtualCluster(
				Enumerable.Range(startFrom, numberOfNodes).Select(n => new Node(new Uri($"http://localhost:{n}"))),
				ElasticsearchMockProductRegistration.Default
			);

		public static VirtualCluster MasterOnlyNodes(int numberOfNodes, int startFrom = 9200) =>
			new VirtualCluster(
				Enumerable.Range(startFrom, numberOfNodes)
					.Select(n => new Node(new Uri($"http://localhost:{n}")) { HoldsData = false, MasterEligible = true }),
				ElasticsearchMockProductRegistration.Default
			);


		public static VirtualCluster Nodes(IEnumerable<Node> nodes) =>
			new VirtualCluster(nodes, ElasticsearchMockProductRegistration.Default);

	}
}
