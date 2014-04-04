using System;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Cluster.State
{
	[TestFixture]
	public class StateTests : BaseJsonTests
	{
		[Test]
		public void ClusterState()
		{
			var r = this._client.ClusterState();
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/state");
		}

		[Test]
		public void ClusterStatePerIndex()
		{
			var r = this._client.ClusterState(h=>h.Index(Test.Default.DefaultIndex).FlatSettings());
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/state");
			u.Query.Should().Contain("flat_settings=true");
		}

		[Test]
		public void CanMapClusterStateReturn()
		{
			var client = this.GetFixedReturnClient(MethodInfo.GetCurrentMethod(), "ClusterState");
			var state = client.ClusterState(s=>s.Metrics(ClusterStateMetric.All));

			state.ClusterName.Should().Be("elasticsearch-test");
			state.Nodes.Count.Should().Be(1);

			var indices = state.Metadata.Indices;
			indices.Count.Should().Be(1);


		}
	}
}