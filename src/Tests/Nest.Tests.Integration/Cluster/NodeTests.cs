using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Cluster
{
	[TestFixture]
	public class NodeTests : IntegrationTests
	{
		[Test]
		public void NodeInfo()
		{
			var r = this.Client.NodesInfo(c=>c
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
			var r = this.Client.NodesStats();
			Assert.True(r.IsValid);
			Assert.IsNotNull(r.Nodes);

			var node = r.Nodes.Values.First();
			Assert.IsNotNull(node.Hostname);
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

		[Test]
		public void NodesHotThreads()
		{
			var r = this.Client.NodesHotThreads(n => n
				.Interval("20s")
				.Snapshots(5)
				.Threads(5)
				.ThreadType(ThreadType.Cpu)
			);
			
			r.IsValid.Should().BeTrue();
			r.HotThreads.Count.Should().BeGreaterOrEqualTo(1);
			var hotThreadInfo = r.HotThreads.First();
			hotThreadInfo.Node.Should().NotBeNullOrEmpty();
			hotThreadInfo.Threads.Count.Should().BeGreaterThan(1);
		}
	}
}