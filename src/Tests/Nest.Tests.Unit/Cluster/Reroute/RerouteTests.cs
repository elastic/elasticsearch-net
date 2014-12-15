using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Cluster.Reroute
{
	[TestFixture]
	public class RerouteTests : BaseJsonTests
	{
		[Test]
		public void ClusterReroute()
		{
			var r = this._client.ClusterReroute(cr => cr
				.Move(m => m
					.Index("test")
					.Shard(0)
					.FromNode("node1")
					.ToNode("node2")
				)
				.Allocate(a => a
					.Index("test")
					.Shard(1)
					.Node("node3")
				)
				.Cancel(c => c
					.Index("test")
					.Shard(2)
					.Node("node4")
				)
			);

			this.JsonEquals(r.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
