using Elasticsearch.Net.Connection;
using System;
using System.Linq;

namespace Tests.Framework
{
	public static class Cluster
	{
		public static VirtualCluster Nodes(int numberOfNodes, int startFrom = 9200) =>
			new VirtualCluster(
				Enumerable.Range(startFrom, numberOfNodes).Select(n => new Node(new Uri($"http://localhost:{n}")))
			);
	}
}