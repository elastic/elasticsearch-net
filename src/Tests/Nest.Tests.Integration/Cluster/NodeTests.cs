using System.Linq;
using System.Runtime.Remoting.Channels;
using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Tests.Integration.Cluster
{
	[TestFixture]
	public class NodeTests : IntegrationTests
	{
		[Test]
		public void NodeInfo()
		{
			var r = this._client.NodesInfo(c=>c
				.Metrics(
					NodesInfoMetric.Transport, 
					NodesInfoMetric.Jvm,
					NodesInfoMetric.ThreadPool,
					NodesInfoMetric.Http,
					NodesInfoMetric.Network,
					NodesInfoMetric.Os,
					NodesInfoMetric.Process,
					NodesInfoMetric.Settings
				)
			);
			Assert.True(r.IsValid);
			Assert.IsNotNull(r.Nodes);
			var node = r.Nodes.Values.First();
			Assert.IsNotNull(node.Name);
			Assert.IsNotNull(node.TransportAddress);
			Assert.IsNotNull(node.Hostname);
			Assert.IsNotNull(node.JVM);
			Assert.IsNotNull(node.ThreadPool);
		}

		[Test]
		public void NodeStats()
		{
			var r = this._client.NodesStats();
			Assert.True(r.IsValid);
			Assert.IsNotNull(r.Nodes);
			var node = r.Nodes.Values.First();

			Assert.IsNotNull(node.Indices);
			Assert.IsNotNull(node.FileSystem);
			Assert.IsNotNull(node.OS);
			Assert.IsNotNull(node.Process);
			Assert.IsNotNull(node.JVM);
			Assert.IsNotNull(node.ThreadPool);
			Assert.IsNotNull(node.Network);
			Assert.IsNotNull(node.Transport);
			Assert.IsNotNull(node.HTTP);
		}
	}
}