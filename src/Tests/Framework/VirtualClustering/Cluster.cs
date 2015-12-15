using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

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