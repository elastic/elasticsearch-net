using System.Collections.Generic;
using NUnit.Framework;

namespace Nest.Tests.Integration.Cluster
{
	[TestFixture]
	public class StateTests : IntegrationTests
	{
		[Test]
		public void SimpleState()
		{
			var r = this._client.ClusterState();
			Assert.True(r.IsValid);
			Assert.NotNull(r.ClusterName);
			Assert.NotNull(r.MasterNode);
			Assert.NotNull(r.Metadata);
			Assert.NotNull(r.Metadata.Indices);
			Assert.True(r.Metadata.Indices.Count > 0);
			foreach (var index in r.Metadata.Indices)
			{
				if (!index.Key.StartsWith("nest"))
					continue;
				Assert.NotNull(index.Value.Mappings);
				Assert.True(index.Value.Mappings.Count > 0);
			}

			Assert.NotNull(r.Nodes);
			Assert.True(r.Nodes.Count > 0);
			Assert.NotNull(r.RoutingNodes);
			Assert.True(r.RoutingNodes.Nodes.Count > 0);
			Assert.NotNull(r.RoutingTable);
		}
		[Test]
		public void StateWithoutMetadata()
		{
			//TODO incorporate metrics
			//var r = this._client.ClusterState(cs=>cs.());
			//Assert.IsNull(r.Metadata);
		}
		[Test]
		public void StateWithoutNodes()
		{
			//var r = this._client.ClusterState(cs=>cs.FilterNodes());
			//Assert.IsNull(r.Nodes);
		}
		[Test]
		public void StateWithoutRoutingTable()
		{
			//var r = this._client.ClusterState(cs=>cs.FilterRoutingTable());
			//Assert.IsNull(r.RoutingTable);
		}
		[Test]
		public void StateWithoutBlocks()
		{
			////var r = this._client.ClusterState(ClusterStateInfo.ExcludeRoutingTable);
			//Assert.IsNull(r.Blocks);
		}
	}
}