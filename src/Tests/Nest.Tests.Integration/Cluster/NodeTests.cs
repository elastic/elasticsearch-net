using System.Linq;
using NUnit.Framework;

namespace Nest.Tests.Integration.Cluster
{
	[TestFixture]
	public class NodeTests : IntegrationTests
	{
		[Test]
		public void NodeInfo()
		{

			var r = this._client.NodesInfo();

			Assert.True(r.IsValid);
			Assert.IsNotNull(r.Nodes);
			var node = r.Nodes.Values.First();
			Assert.IsNotNull(node.Name);
			Assert.IsNotNull(node.TransportAddress);
			Assert.IsNotNull(node.Hostname);
			Assert.IsNull(node.JVM);
			Assert.IsNull(node.ThreadPool);
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