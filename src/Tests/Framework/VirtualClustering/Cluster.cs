using Elasticsearch.Net.Connection;
using System;
using System.Linq;

namespace Tests.Framework
{
	public static class Cluster
	{
		public static VirtualCluster Nodes(int numberOfNodes) =>
			new VirtualCluster(
				Enumerable.Range(9200, numberOfNodes).Select(n => new Node(new Uri($"http://localhost:{n}")))
			);
	}
}