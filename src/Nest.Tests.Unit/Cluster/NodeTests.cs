using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Cluster
{
	[TestFixture]
	public class NodeTests : BaseJsonTests
	{
		[Test]
		public void NodeInfoSimple()
		{
			var r = this._client.NodeInfo(NodesInfo.All);
			var status = r.ConnectionStatus;
			var url = new Uri(status.RequestUrl);
			url.Query.Should().Contain("all=true");
			url.AbsolutePath.Should().StartWith("/_nodes");
		}

		[Test]
		public void NodeInfoFlags()
		{
			var r = this._client.NodeInfo(NodesInfo.Process | NodesInfo.OS);
			var status = r.ConnectionStatus;
			var url = new Uri(status.RequestUrl);
			url.Query.Should().Contain("os=true");
			url.Query.Should().Contain("process=true");
			url.AbsolutePath.Should().StartWith("/_nodes");
		}
		[Test]
		public void NodeInfoFlagsAndNodes()
		{
			var r = this._client.NodeInfo(new [] { "127.0.0.1"}, NodesInfo.Process | NodesInfo.OS);
			var status = r.ConnectionStatus;
			var url = new Uri(status.RequestUrl);
			url.Query.Should().Contain("os=true");
			url.Query.Should().Contain("process=true");
			url.AbsolutePath.Should().StartWith("/_nodes");
			url.AbsolutePath.Should().Contain("127.0.0.1");
		}

		[Test]
		public void NodeStats()
		{
			var r = this._client.NodeStats(NodeInfoStats.All);
			var status = r.ConnectionStatus;
			var url = new Uri(status.RequestUrl);
			url.AbsolutePath.Should().StartWith("/_nodes/stats");
			url.Query.Should().Contain("all=true");
			
		}
		[Test]
		public void NodeStatsSpecificNodeWithFlags()
		{
			var r = this._client.NodeStats(new [] { "127.0.0.1"}, NodeInfoStats.Network | NodeInfoStats.HTTP);
			var status = r.ConnectionStatus;
			var url = new Uri(status.RequestUrl);
			url.AbsolutePath.Should().StartWith("/_nodes/127.0.0.1/stats");
			url.Query.Should().Contain("network=true");
			url.Query.Should().Contain("http=true");
		}

	}
}