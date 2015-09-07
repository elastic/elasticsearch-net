using Elasticsearch.Net.Connection;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Tests.Framework
{
	public static class Cluster
	{
		public static VirtualCluster Nodes(int numberOfNodes, int startFrom = 9200) =>
			new VirtualCluster(
				Enumerable.Range(startFrom, numberOfNodes).Select(n => new Node(new Uri($"http://localhost:{n}")))
			);

		public static VirtualCluster Nodes(IEnumerable<Node> nodes) =>
			new VirtualCluster(nodes);
	}
}